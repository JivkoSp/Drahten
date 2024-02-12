# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://docs.scrapy.org/en/latest/topics/item-pipeline.html

from scrapy.utils.serialize import ScrapyJSONEncoder
from haystack.schema import Document as HaystackDocument
from app import models
from app.api import serializers
import pandas
import json
import hashlib


class DrahtenScraperPipeline:
    def __init__(self):
        self.jsonEncoder = ScrapyJSONEncoder()


    def open_spider(self, spider):
        print("\n\n#### SPIDER - OPENED. ####\n\n")


    def process_item(self, item, spider):
        try:
            #Encode the scrapy object type (item) as JSON string.
            encoded_item = self.jsonEncoder.encode(item)
            
            #Convert the encoded_item from JSON string to JSON object.
            encoded_item = json.loads(encoded_item)

            ###################################################################################
            #### CHECK IF THE ITEM encoded_item EXISTS IN THE ELASTICSEARCH DATABASE - START
            ###################################################################################

            #Search for record, that has data like (data that most closely correspondes to) article_data
            #field of the encoded_item and return ONLY ONE MATCH - top_k=1 (if any).
            #The query method returns: List<Document> where Document is haystack document type.
            # query_response = self.search_engine.document_store.query(query=encoded_item['article_data'][0], 
            #                                                          top_k=1)
            #Check if the query has returned anything.
            # if query_response:
            #     #The query HAS returned data.
            #     document = query_response[0]
            #     #Check if the encoded_item has >= 90% match with the document, found from the query.
            #     if document.score >= 0.90:
            #         #The encoded_item has >= 90% match with the document, found from the query.
            #         #No need to write another record, that has >= 90% similarity in elasticsearch.
            #         return

            ###################################################################################
            #### CHECK IF THE ITEM encoded_item EXISTS IN THE ELASTICSEARCH DATABASE - END
            ###################################################################################

            #Create haystack document type from the PANDAS dataframe type.
            #This step is needed, becouse the ElasticsearchDocumentStore stores data as haystack document types.
            #Here the meta is Dict[str, Any] and it's purpose is to hold the data from the document for easy access.
            new_document = HaystackDocument(content=encoded_item['article_data'][0], 
                                            meta={"article_prev_title": encoded_item['article_prev_title'][0],
                                                  "article_title": encoded_item['article_title'][0],
                                                  "article_data": encoded_item['article_data'][0],
                                                  "article_published_date": encoded_item['article_published_date'][0],
                                                  "article_author": encoded_item['article_author'][0],
                                                  "article_link": encoded_item['article_link'][0]})

            models.search_engine_cybersecurity_news_europe.WriteDocuments([new_document])

        except Exception as ex:
            #TODO: Log the exception to logging service
            print(f"Exception: {ex}")


    def close_spider(self, spider):
        print("\n\n#### SPIDER - CLOSED. ####\n\n")