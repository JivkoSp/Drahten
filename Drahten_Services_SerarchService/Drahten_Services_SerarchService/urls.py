"""
Definition of urls for Drahten_Services_SerarchService.
"""

from django.urls import path, include


urlpatterns = [
   path('search_service/', include('app.api.urls'))
]
