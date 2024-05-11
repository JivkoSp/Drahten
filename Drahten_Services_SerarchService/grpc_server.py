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
    
        query_response = models.search_engine_cybersecurity_news_europe.query_pipeline.run(
                query = request.content,
                params={
                    "retriever": {"top_k": 1}
                }
        )
    
        if query_response is None:
            similarityScoreDto = greeter_pb2.SimilarityScoreResponse(similarityScore=0)
            yield similarityScoreDto

        # The response is always a list of Haystack documents, but there will be only one returned document (top_k == 1).
        document = query_response['documents'][0]

        # Check if the document has less than 90% similarity with existing documents in ElasticSearch.
        if document.score < 0.9:
            new_document = HaystackDocument(id= request.articleId,
                                                content=request.content, 
                                                meta={"article_prev_title": request.prevTitle,
                                                    "article_title": request.title,
                                                    "article_data": request.content,
                                                    "article_published_date": request.publishingDate,
                                                    "article_author": request.author,
                                                    "article_link": request.link})
            
            models.search_engine_cybersecurity_news_europe.WriteDocuments([new_document])

        similarityScoreDto = greeter_pb2.SimilarityScoreResponse(similarityScore=document.score)
        
        yield similarityScoreDto


def InitializeGrpcServer():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    greeter_pb2_grpc.add_DocumentFinderServicer_to_server(DocumentFinderService(), server)
    greeter_pb2_grpc.add_DocumentSimilarityCheckServicer_to_server(DocumentSimilarityCheckService(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    server.wait_for_termination()