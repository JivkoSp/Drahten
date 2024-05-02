from concurrent import futures
import grpc
import greeter_pb2
import greeter_pb2_grpc
from app import models

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


def InitializeGrpcServer():
    server = grpc.server(futures.ThreadPoolExecutor(max_workers=10))
    greeter_pb2_grpc.add_DocumentFinderServicer_to_server(DocumentFinderService(), server)
    server.add_insecure_port('[::]:50051')
    server.start()
    server.wait_for_termination()