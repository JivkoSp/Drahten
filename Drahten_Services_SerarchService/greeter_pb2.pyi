from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from typing import ClassVar as _ClassVar, Mapping as _Mapping, Optional as _Optional, Union as _Union

DESCRIPTOR: _descriptor.FileDescriptor

class FindDocumentsRequest(_message.Message):
    __slots__ = ()
    def __init__(self) -> None: ...

class Document(_message.Message):
    __slots__ = ("articleId", "prevTitle", "title", "content", "publishingDate", "author", "link")
    ARTICLEID_FIELD_NUMBER: _ClassVar[int]
    PREVTITLE_FIELD_NUMBER: _ClassVar[int]
    TITLE_FIELD_NUMBER: _ClassVar[int]
    CONTENT_FIELD_NUMBER: _ClassVar[int]
    PUBLISHINGDATE_FIELD_NUMBER: _ClassVar[int]
    AUTHOR_FIELD_NUMBER: _ClassVar[int]
    LINK_FIELD_NUMBER: _ClassVar[int]
    articleId: str
    prevTitle: str
    title: str
    content: str
    publishingDate: str
    author: str
    link: str
    def __init__(self, articleId: _Optional[str] = ..., prevTitle: _Optional[str] = ..., title: _Optional[str] = ..., content: _Optional[str] = ..., publishingDate: _Optional[str] = ..., author: _Optional[str] = ..., link: _Optional[str] = ...) -> None: ...

class DocumentResponse(_message.Message):
    __slots__ = ("documentTopic", "document")
    DOCUMENTTOPIC_FIELD_NUMBER: _ClassVar[int]
    DOCUMENT_FIELD_NUMBER: _ClassVar[int]
    documentTopic: str
    document: Document
    def __init__(self, documentTopic: _Optional[str] = ..., document: _Optional[_Union[Document, _Mapping]] = ...) -> None: ...

class SimilarityScoreResponse(_message.Message):
    __slots__ = ("similarityScore",)
    SIMILARITYSCORE_FIELD_NUMBER: _ClassVar[int]
    similarityScore: float
    def __init__(self, similarityScore: _Optional[float] = ...) -> None: ...
