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


class DocumentDto:
    def __init__(self, article_prev_title, article_title, article_data, article_published_date, article_author, article_link):
        self.ArticlePrevTitle = article_prev_title
        self.ArticleTitle = article_title
        self.ArticleData = article_data
        self.ArticlePublishedDate = article_published_date
        self.ArticleAuthor = article_author
        self.ArticleLink = article_link
    
    def to_dict(self):
        return {
            'ArticlePrevTitle': self.ArticlePrevTitle,
            'ArticleTitle': self.ArticleTitle,
            'ArticleData': self.ArticleData,
            'ArticlePublishedDate': self.ArticlePublishedDate,
            'ArticleAuthor': self.ArticleAuthor,
            'ArticleLink': self.ArticleLink,
        }