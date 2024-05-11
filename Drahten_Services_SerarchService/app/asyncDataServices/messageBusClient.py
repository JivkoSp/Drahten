import pika
import json

class MessageBusPublisher:
    def __init__(self):
        self.connection = pika.BlockingConnection(pika.ConnectionParameters('rabbitmq_servicebus'))
        self.channel = self.connection.channel()
        self.channel.exchange_declare(exchange='search_service', exchange_type='direct')
        print("\n\n*** SearchService INITIALIZED THE MESSAGE PUBLISHER ***\n\n")

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