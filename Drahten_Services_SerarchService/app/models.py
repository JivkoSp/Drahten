"""
Definition of models.
"""
from django.db import models
from haystack.document_stores import ElasticsearchDocumentStore
from haystack import Pipeline
from haystack.pipelines import ExtractiveQAPipeline
from haystack.nodes import PreProcessor, BM25Retriever, EmbeddingRetriever, FARMReader, TransformersSummarizer, QuestionGenerator

class SearchEngine():
    def _DefineQueryPipeline(self):
        query_pipeline = Pipeline()
        query_pipeline.add_node(component=self.tf_idf_retriever, name="retriever", inputs=["Query"])
        query_pipeline.add_node(component=self.farm_reader, name="reader", inputs=["retriever"])
        return query_pipeline

    def _DefineSummarizerPipeline(self):
        query_pipeline = Pipeline()
        query_pipeline.add_node(component=self.summarizer, name="Summarizer", inputs=["File"])
        return query_pipeline

    def __init__(self, index_name):
        self.document_store = ElasticsearchDocumentStore(host="es01",
                                                         port=9200,
                                                         scheme="https",
                                                         ca_certs="""MIIDSjCCAjKgAwIBAgIVAILMijQGhZqi1wLEWo0jaEUJXwDqMA0GCSqGSIb3DQEB
                                                                    CwUAMDQxMjAwBgNVBAMTKUVsYXN0aWMgQ2VydGlmaWNhdGUgVG9vbCBBdXRvZ2Vu
                                                                    ZXJhdGVkIENBMB4XDTI0MDIwMzE5NDA1NloXDTI3MDIwMjE5NDA1NlowNDEyMDAG
                                                                    A1UEAxMpRWxhc3RpYyBDZXJ0aWZpY2F0ZSBUb29sIEF1dG9nZW5lcmF0ZWQgQ0Ew
                                                                    ggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQCumcwhTaJj0N2peDtmgr6d
                                                                    H3dDl4ChBSOZ24Mi9n/uzTfQ30LKX0qiYPvuu7dyKjy97TUHgwBMPjc655GY9cDh
                                                                    lr9tYHE1r1BnuI1a8uZvmQLExRwU3mpt3K8oyMnR3eywJ2NRRkAV055BMTbbyRqr
                                                                    f39NoqrBMtNf4LKWVwthi0haa4Kvp4wPIJ0HDuw7+d/GVOS4NFYMzBvyvvt1FOxp
                                                                    vhK3BFw2FTuomJUv832VxB/yISLprKSwG0B1Vx6iyPFLXAyiFxAcpXnbKDaQsnpO
                                                                    49fM8hZvO5sIPu7mRXUVM5yqi+uPmc+fYSQsR+Rr8epQNpAXMT7gflozHpI6rzPR
                                                                    AgMBAAGjUzBRMB0GA1UdDgQWBBRz9E9fpjHhDAH0pKYl6hZr0hS/6zAfBgNVHSME
                                                                    GDAWgBRz9E9fpjHhDAH0pKYl6hZr0hS/6zAPBgNVHRMBAf8EBTADAQH/MA0GCSqG
                                                                    SIb3DQEBCwUAA4IBAQAGVIGdh/J0VT+Y2KpJQniEoK4SWqXUQIvGLiUP7uo1Wvr/
                                                                    vorppPnA8+whanzBStmPtA2etidlWum5CmzkKWwPZu5l0+5/ae+ZLhAVpFuV5wok
                                                                    5LpwMZ3aRmY3Yd0NvnjNdY+t6nKZYKZp5gVJkiXzwoUZ8d/IztMbrPfboeIMQQx2
                                                                    6ChwBNoAiAkXh+nC22Ol+exIdnC3DwRXc9uHJsM9YU4vvvPJgWIwFuLy3eATBKKu
                                                                    u6hhKJWkVu6/9S8pAdURMqpDT8HRW/4As6qpZAehkQOKfCXlQ4ldROZJNK4om2sl
                                                                    M7xAEhMbuY/REoTtyH7OrCUnbb0T6Qy2ahiiGByM
                                                                """,
                                                         verify_certs=False,
                                                         username="elastic",
                                                         password="elastic123!",
                                                         index=index_name)
        self.preprocessor = PreProcessor(
            clean_whitespace=True,
            clean_header_footer=True,
            clean_empty_lines=True,
            remove_substrings=None,
            split_by="word",
            split_length=100000,
            split_overlap=200,
            split_respect_sentence_boundary=True)
        
        #BM25 is a variant of TF-IDF.
        self.tf_idf_retriever = BM25Retriever(document_store=self.document_store)

        self.farm_reader = FARMReader(model_name_or_path="deepset/roberta-base-squad2", use_gpu=True)

        self.summarizer = TransformersSummarizer(model_name_or_path="facebook/bart-large-cnn")

        self.query_pipeline = self._DefineQueryPipeline()

        self.summarizer_pipeline = self._DefineSummarizerPipeline()

        self.question_generator = QuestionGenerator()

        print("\n\n#### SEARCH ENGINE - STARTED. ####\n\n")


    def WriteDocuments(self, documents):
        execution_result = {
            "exception_message": "",
            "value": False
        }
        try:
            preprocessed_documents = self.preprocessor.process(documents=documents)
            self.document_store.write_documents(documents=preprocessed_documents, duplicate_documents="skip")
            execution_result['value'] = True
        except Exception as ex:
            execution_result['exception_message'] = ex

        return execution_result
    


class NLPQueryAnswer(models.Model):
    def __init__(self, answer):
        self.answer = answer['answer']
        self.answer_type = answer['type']
        self.score = answer['score']
        self.context = answer['context']
        self.offsets_in_document = answer['offsets_in_document']
        self.offsets_in_context = answer['offsets_in_context']
        self.document_id = answer['document_ids'][0]
        self.document = answer['meta']
    
    answer = ""
    answer_type = ""
    score = None
    context = None
    offsets_in_document = None
    offsets_in_context = None
    document_id = []
    document = []



class QueryAnswer(models.Model):
    def __init__(self, answer):
        self.document_id = answer['id']
        self.score = answer['score']
        self.document = answer['meta']
    
    document_id = ""
    score = float
    document = []



#Instantiate SearchEngine instance for cybersecurity_news_europe index.
search_engine_cybersecurity_news_europe = SearchEngine(index_name="cybersecurity_news_europe")