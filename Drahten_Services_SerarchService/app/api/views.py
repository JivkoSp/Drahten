from rest_framework.response import Response
from rest_framework import status
from rest_framework import viewsets
from haystack.nodes import EmbeddingRetriever
from sentence_transformers import SentenceTransformer
from haystack.schema import Document as HaystackDocument
from app import models, dtos
from app.asyncDataServices import messageBusClient
from . import serializers
from app.security import authorization, ratelimiting
import requests
from ratelimit import RateLimitException
import json
import re
from datetime import datetime
import pytz


class CybersecurityNewsEuropeSummarizationViewSet(viewsets.ViewSet):
    
    #FSC implementation
    def fixed_size_chunking(self, document_text, chunk_size=1024):
        """
            Splits a document into fixed-size chunks.
            Args:
                document_text (str): The input document text.
                chunk_size (int): Size of each chunk.

            Returns:
                list: List of chunks.
        """
        if chunk_size <= 0:
            raise ValueError("Chunk size must be a positive integer.")

        chunks = [document_text[i:i + chunk_size] for i in range(0, len(document_text), chunk_size)]
        return chunks

   
    def list(self, request):
        return Response(status=status.HTTP_200_OK)


    #Retrieves summarization of document, that is stored in cybersecurity_news_europe index.
    #Input: String, representing valid id of document, stored in cybersecurity_news_europe index.
    #Output: ResponseDto object where the Result property has the following format: SummarizedDocumentDto object.
    #ProducesResponseType(HTTP_200_OK, ResponseDto)
    #ProducesResponseType(HTTP_404_NOT_FOUND, ResponseDto)
    #ProducesResponseType(HTTP_400_BAD_REQUEST, ResponseDto)
    def retrieve(self, request, pk=None):
        try:
            query_response = models.search_engine_cybersecurity_news_europe.document_store.get_document_by_id(id=pk)

            if not query_response:
                return Response(status=status.HTTP_404_NOT_FOUND)
            
            document_chunks = self.fixed_size_chunking(query_response.content)

            # Create Document objects for each chunk
            documents = [HaystackDocument(content=chunk) for chunk in document_chunks]

            document_summary = ""

            for document in documents:
                response = models.search_engine_cybersecurity_news_europe.summarizer_pipeline.run(
                    documents=[document]
                )
                document_summary += response['documents'][0].meta['summary']


            summarized_documentDto = dtos.SummarizedDocumentDto(pk, document_summary)

            serialized_summarized_documentDto = serializers.SummarizedDocumentDtoSerializer(summarized_documentDto)

            responseDto = dtos.ResponseDto(IsSuccess=True, Result=serialized_summarized_documentDto.data)

            serialized_responseDto = serializers.ResponseDtoSerializer(responseDto)

            return Response(status=status.HTTP_200_OK, data=serialized_responseDto.data)
        
        except Exception as ex:
            print(f"Exception {ex}")
            #TODO: Log the exception to logging service.
            return Response(status=status.HTTP_400_BAD_REQUEST)
        

class CybersecurityNewsEuropeQuestionViewSet(viewsets.ViewSet):
    
    def list(self, request):
        
        return Response(status=status.HTTP_200_OK)
    

    #Retrieves questions, based on document, that is stored in cybersecurity_news_europe index.
    #Input: String, representing valid id of document, stored in cybersecurity_news_europe index.
    #Output: ResponseDto object where the Result property has the following format: [str]
    #ProducesResponseType(HTTP_200_OK, ResponseDto)
    #ProducesResponseType(HTTP_404_NOT_FOUND, ResponseDto)
    #ProducesResponseType(HTTP_400_BAD_REQUEST, ResponseDto)
    def retrieve(self, request, pk=None):
        try:
            query_response = models.search_engine_cybersecurity_news_europe.document_store.get_document_by_id(id=pk)

            if not query_response:
                return Response(status=status.HTTP_404_NOT_FOUND)

            query_response = models.search_engine_cybersecurity_news_europe.question_generator.generate(query_response.content)

            responseDto = dtos.ResponseDto(IsSuccess=True, Result=query_response)

            serialized_responseDto = serializers.ResponseDtoSerializer(responseDto)

            return Response(status=status.HTTP_200_OK, data=serialized_responseDto.data)
        
        except Exception as ex:
            print(f"Exception {ex}")
            #TODO: Log the exception to logging service.
            return Response(status=status.HTTP_400_BAD_REQUEST)


class CybersecurityNewsEuropeSemanticSearchDataViewSet(viewsets.ViewSet):
    
     def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.messageBusPublisher = messageBusClient.MessageBusPublisher()
      
    #Retrieves information with SEMANTIC query from document stored in cybersecurity_news_europe index.
    #Input: The request body must have json with the following format: {"document_id": "", "query": ""} where document_id is 
    #       valid id of document stored in cybersecurity_news_europe index.
    #Output: ResponseDto object where the Result property has the following format: NLPQueryAnswer object.
    #ProducesResponseType(HTTP_200_OK, ResponseDto)
    #ProducesResponseType(HTTP_404_NOT_FOUND, ResponseDto)
    #ProducesResponseType(HTTP_400_BAD_REQUEST, ResponseDto)
     def create(self, request):
        try:
            # Apply request rate limiting.
            ratelimiting.rate_limiter()

            # Get the access token from the request headers
            authorization_header = request.headers.get("Authorization")

            if authorization_header:
                # Extract the token part from the Authorization header.
                token = authorization_header.split(" ")[1]  
                
                # Call the authorize function and initialize the variable decoded_token
                # with the result. The decoded_token will contain the decoded information from token if the token is valid.
                # Possible values for decoded_token: None, dict[str, str].
                decoded_token = authorization.authorize(token)

                if decoded_token is None:
                     #### TODO
                     #### Throw custom exception.
                    pass

                questionDto = request.data

                query_response = models.search_engine_cybersecurity_news_europe.document_store.get_document_by_id(id=questionDto['document_id'])

                if not query_response:
                    return Response(status=status.HTTP_404_NOT_FOUND)

                searchedArticleDataDto = dtos.SearchedArticleDataDto(articleId=questionDto['document_id'], 
                                                                     userId=decoded_token['sub'], 
                                                                     searchedData=questionDto['query'],
                                                                     dateTime=datetime.now(pytz.utc))

                # Post message to the message broker about searching information about document with ID: document_id by user with ID: userId.
                self.messageBusPublisher.PublishSearchedDocumentData(searchedArticleDataDto)

                query_response = models.search_engine_cybersecurity_news_europe.query_pipeline.run(
                    query = questionDto['query'],
                    documents=[query_response]
                )

                document = None

                for answer in query_response['answers']:
                    answer = answer.to_dict()
                    if(answer['document_ids'][0] == questionDto['document_id']):
                        document = answer
                        break
                
                query_answer = models.NLPQueryAnswer(document)

                serialized_query_answer = serializers.NLPQueryAnswerSerializer(query_answer)

                responseDto = dtos.ResponseDto(IsSuccess=True, Result=serialized_query_answer.data)

                serialized_responseDto = serializers.ResponseDtoSerializer(responseDto)

                return Response(status=status.HTTP_200_OK, data=serialized_responseDto.data)
            else:
                return Response("Unauthorized", status=status.HTTP_401_UNAUTHORIZED)
        
        except Exception as ex:
            print(f"Exception {ex}")
            #TODO: Log the exception to logging service.
            return Response(status=status.HTTP_400_BAD_REQUEST)


class CybersecurityNewsEuropeViewSet(viewsets.ViewSet):

    def FilterDuplicates(self, documents):
        document_dict = {}    

        for document in documents:
            doc_as_dict = document.to_dict()    
            document_dict[doc_as_dict['document_ids'][0]] = document
        
        return document_dict


    #Retrieves ALL documents from cybersecurity_news_europe index.
    #Input: None
    #Output: ResponseDto object where the Result property has the following format: QueryAnswer object.
    #ProducesResponseType(HTTP_200_OK, ResponseDto)
    #ProducesResponseType(HTTP_404_NOT_FOUND, ResponseDto)
    #ProducesResponseType(HTTP_400_BAD_REQUEST, ResponseDto)
    def list(self, request):
        try:
            # Apply request rate limiting.
            ratelimiting.rate_limiter()

            # Get the access token from the request headers
            authorization_header = request.headers.get("Authorization")

            if authorization_header:
                # Extract the token part from the Authorization header.
                token = authorization_header.split(" ")[1]  
                
                # Call the authorize function and initialize the variable decoded_token
                # with the result. The decoded_token will contain the decoded information from token if the token is valid.
                # Possible values for decoded_token: None, dict[str, str].
                decoded_token = authorization.authorize(token)

                if decoded_token is None:
                     #### TODO
                     #### Throw custom exception.
                    pass

                query_response = models.search_engine_cybersecurity_news_europe.document_store.get_all_documents()

                if not query_response:
                    return Response(status=status.HTTP_404_NOT_FOUND)
                
                answers = []

                for answer in query_response:
                    query_answer = models.QueryAnswer(answer.to_dict())
                    answers.append(query_answer)

                serialized_query_answer = serializers.QueryAnswerSerializer(answers, many=True)

                responseDto = dtos.ResponseDto(IsSuccess=True, Result=serialized_query_answer.data)
            
                serialized_responseDto = serializers.ResponseDtoSerializer(responseDto)

                return Response(status=status.HTTP_200_OK, data=serialized_responseDto.data)
            else:
                return Response("Unauthorized", status=status.HTTP_401_UNAUTHORIZED)

        except RateLimitException as e:
            # Handle rate limit exception
            return Response("Rate limit exceeded", status=status.HTTP_429_TOO_MANY_REQUESTS)
        except requests.RequestException as e:
            print(f"Error fetching JWKS: {e}")

            #### TODO
            #### Throw custom exception.

            return None  # Unable to fetch JWKS, token validation failed
        except Exception as ex:
            print(f"Exception {ex}")
            #TODO: Log the exception to logging service.
            return Response(status=status.HTTP_400_BAD_REQUEST)
    
    
    #Retrieves ALL documents from cybersecurity_news_europe index, that are mathing the query, specified by the pk argument.
    #Input: String (text data) - represents query, that will be run against ALL documents in cybersecurity_news_europe index.
    #Output: ResponseDto object where the Result property has the following format: NLPQueryAnswer object.
    #ProducesResponseType(HTTP_200_OK, ResponseDto)
    #ProducesResponseType(HTTP_404_NOT_FOUND, ResponseDto)
    #ProducesResponseType(HTTP_400_BAD_REQUEST, ResponseDto)
    def retrieve(self, request, pk=None):
        try:
            if(pk is None or not pk):
                raise Exception("No query value was found for search_service/news/cybersecurity/europe/ endpoint!")


            #Query example: "give me all documents that mention attacks"
            query_response = models.search_engine_cybersecurity_news_europe.query_pipeline.run(
                query = pk,
                params={
                    "retriever": {"top_k": 5}
                }
            )

            if query_response is None:
                return Response(status=status.HTTP_404_NOT_FOUND)
            
            document_dict = self.FilterDuplicates(query_response['answers'])

            answers = []

            for document_key in document_dict:
                query_answer = models.NLPQueryAnswer(document_dict[document_key].to_dict())
                answers.append(query_answer)

            serialized_query_answer = serializers.NLPQueryAnswerSerializer(answers, many=True)

            responseDto = dtos.ResponseDto(IsSuccess=True, Result=serialized_query_answer.data)

            serialized_responseDto = serializers.ResponseDtoSerializer(responseDto)

            return Response(status=status.HTTP_200_OK, data=serialized_responseDto.data)
        
        except Exception as ex:
            print(f"Exception {ex}")
            #TODO: Log the exception to logging service.
            return Response(status=status.HTTP_400_BAD_REQUEST)