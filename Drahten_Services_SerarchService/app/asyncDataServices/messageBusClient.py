import pika
import json

connection = pika.BlockingConnection(pika.ConnectionParameters('rabbitmq_servicebus'))

channel = connection.channel()

channel.exchange_declare(
    exchange='search_service',
    exchange_type='direct'
)

def publishSearchEvent():
    search_info = {
    'Data': 'some search data...',
    'Event': 'Search_Information_Published'
    }
    channel.basic_publish(
    exchange='search_service',
    routing_key='search_service.event',
    body=json.dumps({
                    'Data': search_info['Data'],
                    'Event': search_info['Event']
                    })
    )

    print("\n\n#### SEND MESSAGE TO RABBITMQ ####\n\n")


