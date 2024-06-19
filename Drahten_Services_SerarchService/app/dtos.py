from django.db import models

class ResponseDto(models.Model):
    def __init__(self, IsSuccess, Result, ErrorMessages=[]):
        self.IsSuccess = IsSuccess
        self.Result = Result
        self.ErrorMessages = ErrorMessages

    IsSuccess = False
    Result = None
    ErrorMessages = []


class SearchedArticleDataDto:
    def __init__(self, articleId, userId, searchedData, searchedDataAnswer, searchedDataAnswerContext, dateTime):
        self.ArticleId = articleId
        self.UserId = userId
        self.SearchedData = searchedData
        self.SearchedDataAnswer = searchedDataAnswer
        self.SearchedDataAnswerContext = searchedDataAnswerContext
        self.DateTime = dateTime
        self.Event = "SearchedArticleData"


class SummarizedDocumentDto(models.Model):
    def __init__(self, DocumentId, DocumentSummary):
        self.DocumentId = DocumentId
        self.DocumentSummary = DocumentSummary

    DocumentId = ""
    DocumentSummary = ""