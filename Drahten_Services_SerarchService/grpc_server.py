from concurrent import futures
import grpc
import greeter_pb2
import greeter_pb2_grpc
from app import models
from haystack.schema import Document as HaystackDocument

class DocumentFinderService(greeter_pb2_grpc.DocumentFinder):
    
    def FindDoucments(self, request, context):
       
        documents = models.search_engine_cybersecurity_news_europe.document_store.get_all_documents()

        for document in documents:
          
          documentAsDict = document.to_dict()
          
          documentMeta = documentAsDict['meta']

          documentDto = greeter_pb2.Document(
              articleId = documentAsDict['id'],
              prevTitle = documentMeta['article_prev_title'],
              title = documentMeta['article_title'],
              content = documentMeta['article_data'],
              publishingDate = documentMeta['article_published_date'],
              author = documentMeta['article_author'],
              link = documentMeta['article_link']
          )

          # The ElasticSearch index name is the name of the document/s Topic.
          documentResponseDto = greeter_pb2.DocumentResponse(documentTopic="CybersecurityNewsEurope", document=documentDto)

          yield documentResponseDto


class DocumentSimilarityCheckService(greeter_pb2_grpc.DocumentSimilarityCheck):
    
    def CheckDocumentSimilarity(self, request, context):
    
        print(f"\n\n--> RECEIVED DOCUMENT WITH CONTENT: {request.content}\n\n")

        query_response = models.search_engine_cybersecurity_news_europe.query_pipeline.run(
                query = request.content,
                params={
                    "retriever": {"top_k": 1}
                }
        )
    
        print(f"\n\n--> QUERY RESPONSE: {query_response}\n\n")

        if query_response is None:
            similarityScoreDto = greeter_pb2.SimilarityScoreResponse(similarityScore=0)
            yield similarityScoreDto

        if query_response['documents']:
            print(f"\n\nTHERE IS DOCUMENTS RETURNED\n\n")
            # The response is always a list of Haystack documents, but there will be only one returned document (top_k == 1).
            document = query_response['documents'][0]
        else:
            print(f"\n\nTHERE IS NO DOCUMENTS RETURNED\n\n")

        # Check if the document has less than 90% similarity with existing documents in ElasticSearch.
        if not query_response['documents'] or document.score < 0.9:
            print(f"\n\nDOCUMENT HAS *** LESS *** THAN 90 PERCENT SIMILARITY WITH ALREADY EXISTING DOCUMENTS.\n\n")
            new_document = HaystackDocument(id= request.articleId,
                                                content=request.content, 
                                                meta={"article_prev_title": request.prevTitle,
                                                    "article_title": request.title,
                                                    "article_data": request.content,
                                                    "article_published_date": request.publishingDate,
                                                    "article_author": request.author,
                                                    "article_link": request.link})
            print(f"\n\nWRITING THE DOCUMENT: {new_document}\n\n")
            models.search_engine_cybersecurity_news_europe.WriteDocuments([new_document])
        else:
            print(f"\n\nDOCUMENT HAS 90 OR MORE PERCENT SIMILARITY WITH DOCUMENT/S THAT ALREADY EXIST!\n\n")

        similarityScoreDto = greeter_pb2.SimilarityScoreResponse(similarityScore=document.score)
        
        return similarityScoreDto


def InitializeGrpcServer():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    greeter_pb2_grpc.add_DocumentFinderServicer_to_server(DocumentFinderService(), server)
    greeter_pb2_grpc.add_DocumentSimilarityCheckServicer_to_server(DocumentSimilarityCheckService(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    server.wait_for_termination()