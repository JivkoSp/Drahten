import os
from celery import Celery

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'Drahten_Services_SerarchService.settings')

app = Celery('Drahten_Services_SerarchService')

app.config_from_object('django.conf:settings', namespace='CELERY')

app.autodiscover_tasks()