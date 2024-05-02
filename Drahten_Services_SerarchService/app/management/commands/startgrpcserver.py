from django.core.management.base import BaseCommand
import grpc_server

class Command(BaseCommand):
    help = 'Start the gRPC server'

    def handle(self, *args, **options):
        self.stdout.write(self.style.SUCCESS('\n\n#### GRPC SERVER - STARTED. ####\n\n'))
        grpc_server.InitializeGrpcServer()