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
        

class SummarizedDocumentDtoSerializer(serializers.ModelSerializer):
    class Meta:
        model = dtos.SummarizedDocumentDto
        fields = ['DocumentId',
                  'DocumentSummary']


class ResponseDtoSerializer(serializers.ModelSerializer):
    class Meta:
        model = dtos.ResponseDto
        fields = ['IsSuccess',
                  'Result',
                  'ErrorMessages']
        


class SearchedArticleDataDtoSerializer(serializers.Serializer):
    ArticleId = serializers.CharField(max_length=1000)
    UserId = serializers.CharField(max_length=1000)
    SearchedData = serializers.CharField(max_length=10000)
    SearchedDataAnswer = serializers.CharField(max_length=100000)
    SearchedDataAnswerContext = serializers.CharField(max_length=10000000)
    DateTime = serializers.DateTimeField()
    Event = serializers.CharField(max_length=100)

    def create(self, validated_data):
        return dtos.SearchedArticleDataDto(**validated_data)

    def update(self, instance, validated_data):
        instance.ArticleId = validated_data.get('ArticleId', instance.ArticleId)
        instance.UserId = validated_data.get('UserId', instance.UserId)
        instance.SearchedData = validated_data.get('SearchedData', instance.SearchedData)
        instance.SearchedDataAnswer = validated_data.get('SearchedDataAnswer', instance.SearchedDataAnswer)
        instance.SearchedDataAnswerContext = validated_data.get('SearchedDataAnswerContext', instance.SearchedDataAnswerContext)
        instance.DateTime = validated_data.get('DateTime', instance.DateTime)
        instance.Event = validated_data.get('Event', instance.Event)
        return instance
