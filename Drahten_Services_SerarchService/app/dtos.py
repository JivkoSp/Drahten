from django.db import models

class ResponseDto(models.Model):
    def __init__(self, IsSuccess, Result, ErrorMessages=[]):
        self.IsSuccess = IsSuccess
        self.Result = Result
        self.ErrorMessages = ErrorMessages

    IsSuccess = False
    Result = None
    ErrorMessages = []


class SummarizedDocumentDto(models.Model):
    def __init__(self, DocumentId, DocumentSummary):
        self.DocumentId = DocumentId
        self.DocumentSummary = DocumentSummary

    DocumentId = ""
    DocumentSummary = ""