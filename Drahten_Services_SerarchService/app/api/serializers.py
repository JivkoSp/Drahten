from rest_framework import serializers
from app import models, dtos

class NLPQueryAnswerSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.NLPQueryAnswer
        fields = ['document_id',
                  'document',
                  'answer', 
                  'answer_type',
                  'score',
                  'context',
                  'offsets_in_document']
        


class QueryAnswerSerializer(serializers.ModelSerializer):
    class Meta:
        model = models.QueryAnswer
        fields = ['document_id',
                  'score',
                  'document']
        


class ResponseDtoSerializer(serializers.ModelSerializer):
    class Meta:
        model = dtos.ResponseDto
        fields = ['IsSuccess',
                  'Result',
                  'ErrorMessages']
        

class SummarizedDocumentDtoSerializer(serializers.ModelSerializer):
    class Meta:
        model = dtos.SummarizedDocumentDto
        fields = ['DocumentId',
                  'DocumentSummary']