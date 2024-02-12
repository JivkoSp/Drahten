"""
Definition of urls for Drahten_Services_SerarchService.
"""

from rest_framework.routers import DefaultRouter
from . import views

router = DefaultRouter()
router.register('news/cybersecurity/europe', views.CybersecurityNewsEuropeViewSet, basename='cybersec-news-europe')
router.register('news/cybersecurity/europe/summarization/documents', views.CybersecurityNewsEuropeSummarizationViewSet, basename='cybersec-news-europe-summarization')
router.register('news/cybersecurity/europe/guestions/documents', views.CybersecurityNewsEuropeQuestionViewSet, basename='cybersec-news-europe-questions')
router.register('news/cybersecurity/europe/semantic-search/documents/data', views.CybersecurityNewsEuropeSemanticSearchDataViewSet, basename='cybersec-news-europe-questions-semanticsearch-data')

#Telling django, that the router is now taking over the urlpatterns.
urlpatterns = router.urls