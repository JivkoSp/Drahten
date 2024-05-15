from django.core.management.base import BaseCommand
from app.asyncDataServices import messageBusClient

class Command(BaseCommand):
    help = 'Start the RabbitMQ consumer server'

    def handle(self, *args, **options):
        self.stdout.write(self.style.SUCCESS('\n\n#### RABBITMQ CONSUMER SERVER - STARTED. ####\n\n'))
        messageBusSubscriber = messageBusClient.MessageBusSubscriber()
        messageBusSubscriber.StartConsumer()