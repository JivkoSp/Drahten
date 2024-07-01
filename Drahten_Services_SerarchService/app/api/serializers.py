from rest_framework import serializers
from app import models, dtos

class DocumentSerializer(serializers.Serializer):
    ArticlePrevTitle = serializers.CharField(allow_null=False, allow_blank=False)
    ArticleTitle = serializers.CharField(allow_null=False, allow_blank=False)
    ArticleData = serializers.CharField(allow_null=False, allow_blank=False)
    ArticlePublishedDate = serializers.CharField(allow_null=True, allow_blank=True)
    ArticleAuthor = serializers.CharField(allow_null=True, allow_blank=True)
    ArticleLink = serializers.CharField(allow_null=True, allow_blank=True)

    def to_representation(self, instance):
        return {
            'ArticlePrevTitle': instance.ArticlePrevTitle,
            'ArticleTitle': instance.ArticleTitle,
            'ArticleData': instance.ArticleData,
            'ArticlePublishedDate': instance.ArticlePublishedDate,
            'ArticleAuthor': instance.ArticleAuthor,
            'ArticleLink': instance.ArticleLink,
        }

    def to_internal_value(self, data):
        return dtos.DocumentDto(
            articlePrevTitle=data.get('ArticlePrevTitle'),
            articleTitle=data.get('ArticleTitle'),
            articleData=data.get('ArticleData'),
            articlePublishedDate=data.get('ArticlePublishedDate'),
            articleAuthor=data.get('ArticleAuthor'),
            articleLink=data.get('ArticleLink')
        )
        

class NLPQueryAnswerSerializer(serializers.Serializer):
    DocumentId = serializers.CharField(max_length=1000)
    Answer = serializers.CharField(max_length=100000)
    AnswerType = serializers.CharField(max_length=100)
    Score = serializers.FloatField(min_value=0, max_value=1)
    Context = serializers.CharField(max_length=1000000)
    Document = DocumentSerializer()
    
    def create(self, validated_data):
        document_data = validated_data.pop('Document')
        document_instance = dtos.DocumentDto(**document_data)
        return models.NLPQueryAnswer({
            'document_ids': [validated_data['DocumentId']],
            'answer': validated_data['Answer'],
            'type': validated_data['AnswerType'],
            'score': validated_data['Score'],
            'context': validated_data['Context'],
            'meta': document_instance.to_dict(),
            'offsets_in_document': [],
            'offsets_in_context': [],
        })

    def update(self, instance, validated_data):
        instance.DocumentId = validated_data.get('DocumentId', instance.DocumentId)
        instance.Answer = validated_data.get('Answer', instance.Answer)
        instance.AnswerType = validated_data.get('AnswerType', instance.AnswerType)
        instance.Score = validated_data.get('Score', instance.Score)
        instance.Context = validated_data.get('Context', instance.Context)
        
        document_data = validated_data.get('Document')
        instance.Document.ArticlePrevTitle = document_data.get('ArticlePrevTitle', instance.Document.ArticlePrevTitle)
        instance.Document.ArticleTitle = document_data.get('ArticleTitle', instance.Document.ArticleTitle)
        instance.Document.ArticleData = document_data.get('ArticleData', instance.Document.ArticleData)
        instance.Document.ArticlePublishedDate = document_data.get('ArticlePublishedDate', instance.Document.ArticlePublishedDate)
        instance.Document.ArticleAuthor = document_data.get('ArticleAuthor', instance.Document.ArticleAuthor)
        instance.Document.ArticleLink = document_data.get('ArticleLink', instance.Document.ArticleLink)

        return instance
        


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
