import pika
import json
from app import models
from haystack.schema import Document as HaystackDocument
from sentence_transformers import SentenceTransformer
import numpy as np
from loguru import logger
from app.logging import handler


class MessageClient:
    def __init__(self):
        self.connection = pika.BlockingConnection(pika.ConnectionParameters('rabbitmq_servicebus'))
        print(f"\n\n*** SearchService INITIALIZED THE MESSAGE CLIENT ***\n\n")
        logger.info("*** SearchService INITIALIZED THE MESSAGE CLIENT ***")
    def GetConnection(self):
        return self.connection

messageClient = MessageClient()


class MessageBusPublisher:
    def __init__(self):
        self.connection = messageClient.GetConnection()
        self.channel = self.connection.channel()
        self.channel.exchange_declare(exchange='search_service', exchange_type='direct')
        print("\n\n*** SearchService INITIALIZED THE MESSAGE PUBLISHER ***\n\n")
        logger.info("*** SearchService INITIALIZED THE MESSAGE PUBLISHER ***")

    def PublishNewDocument(self, document, topicName):
        document = document.to_dict()
        self.channel.basic_publish(
            exchange='search_service',
            routing_key='search_service.newdocument',
            body=json.dumps({'DocumentId' : document['id'],
                             'PrevTitle': document['meta']['article_prev_title'],
                             'Title' : document['meta']['article_title'],
                             'Content' : document['meta']['article_data'],
                             'PublishingDate' : document['meta']['article_published_date'],
                             'Author' : document['meta']['article_author'],
                             'Link' : document['meta']['article_link'],
                             'TopicName' : topicName,
                             'Event': 'NewDocument'})
        )

    def PublishDocumentForSimilarityCheck(self, document, spiderName):
        document = document.to_dict()
        self.channel.basic_publish(
            exchange='search_service',
            routing_key='search_service.similaritycheck',
            body=json.dumps({'DocumentId' : document['id'],
                             'PrevTitle': document['meta']['article_prev_title'],
                             'Title' : document['meta']['article_title'],
                             'Content' : document['meta']['article_data'],
                             'PublishingDate' : document['meta']['article_published_date'],
                             'Author' : document['meta']['article_author'],
                             'Link' : document['meta']['article_link'],
                             'TopicName' : spiderName,
                             'Event': 'DocumentSimilarityCheck'})
        )

        print("\n\n*** PUBLISHED MESSAGE TO RABBITMQ ***\n\n")

    def PublishSearchEvent(self):
        search_info = {
            'Data': 'some search data...',
            'Event': 'Search_Information_Published'
        }
        self.channel.basic_publish(
            exchange='search_service',
            routing_key='search_service.event',
            body=json.dumps({'Data': search_info['Data'], 'Event': search_info['Event']}))



class MessageBusSubscriber:
    def __init__(self):
        self.connection = messageClient.GetConnection()
        self.channel = self.connection.channel()
        self.channel.exchange_declare(exchange='search_service', exchange_type='direct')
        self.queue_name = self.channel.queue_declare('test_queue').method.queue
        self.channel.queue_bind(self.queue_name, 'search_service', 'search_service.smc')
        self.model = SentenceTransformer("sentence-transformers/multi-qa-mpnet-base-dot-v1")
        self.messageBusPublisher = MessageBusPublisher()
        print("\n\n*** SearchService INITIALIZED THE MESSAGE SUBSCRIBER ***\n\n")
        logger.info("*** SearchService INITIALIZED THE MESSAGE SUBSCRIBER ***")

    # TODO: Remove this to appropriate class (This should NOT be a part of the message subscriber).
    def _checkSimilarity(self, document_content, threshold=0.9):
        try:
            documents = models.search_engine_cybersecurity_news_europe.document_store.get_all_documents()
            
            provided_document_embedding = self.model.encode(document_content)

            document_embeddings = [self.model.encode(document.content) for document in documents]

            # Calculate cosine similarity between provided document and each retrieved document.
            similarity_scores = []
            for doc_embedding in document_embeddings:
                dot_product = np.dot(provided_document_embedding, doc_embedding)
                norm_provided = np.linalg.norm(provided_document_embedding)
                norm_doc = np.linalg.norm(doc_embedding)
                similarity_score = dot_product / (norm_provided * norm_doc)
                similarity_scores.append(similarity_score)

            similar_documents = []
            # Iterate through each document and its similarity score
            for document, similarity_score in zip(documents, similarity_scores):
                # Check if the similarity score is above the threshold
                if similarity_score >= threshold:
                    # The similarity score is above the threshold, append the document to the similar_documents list.
                    similar_documents.append(document)

            return similar_documents
        except Exception as ex:
            print(f"\nEXCEPTION: {ex}\n")

    def _consume(self, queue_name, callback):
        print(f"\n\nCONSUME WAITING...\n\n")
        self.channel.basic_consume(queue=queue_name, on_message_callback=callback, auto_ack=True)
        self.channel.start_consuming()
        print(f"\n\nCONSUME DONE WAITING\n\n")
    
    def _processMessage(self, ch, method, properties, body):
         try:
            jsonMessage = body.decode()
            message = json.loads(jsonMessage)  # Parse the JSON string to dictionary

            print(f"\n\n--> MESSAGE: {message}\n\n")

            similar_documents = self._checkSimilarity(message['Content'])

            print(f"\nRETURNED FROM _checkSimilarity\n")

            if not similar_documents:
                print(f"\n\nDOCUMENT HAS *** LESS *** THAN 90 PERCENT SIMILARITY WITH ALREADY EXISTING DOCUMENTS.\n\n")
                new_document = HaystackDocument(id= message['DocumentId'],
                                                    content=message['Content'], 
                                                    meta={"article_prev_title": message['PrevTitle'],
                                                        "article_title": message['Title'],
                                                        "article_data": message['Content'],
                                                        "article_published_date": message['PublishingDate'],
                                                        "article_author": message['Author'],
                                                        "article_link": message['Link']})
                print(f"\n\nWRITING THE DOCUMENT: {new_document}\n\n")
                models.search_engine_cybersecurity_news_europe.WriteDocuments([new_document])
                self.messageBusPublisher.PublishNewDocument(new_document, message['TopicName'])
            else:
                print(f"\n\nDOCUMENT HAS 90 OR MORE PERCENT SIMILARITY WITH DOCUMENT/S THAT ALREADY EXIST!\n\n")
         except Exception as ex:
            print(f"\nEXCEPTION: {ex}\n")


    def StartConsumer(self):
        print("\n\n*** SearchService STARTED CONSUMER ***\n\n")
        self._consume(self.queue_name, self._processMessage)
        print("\n\n*** SearchService STOPED CONSUMER ***\n\n")

    