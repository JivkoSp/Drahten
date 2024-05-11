# Define your item pipelines here
#
# Don't forget to add your pipeline to the ITEM_PIPELINES setting
# See: https://docs.scrapy.org/en/latest/topics/item-pipeline.html

from scrapy.utils.serialize import ScrapyJSONEncoder
from haystack.schema import Document as HaystackDocument
import json
from app.asyncDataServices import messageBusClient


class DrahtenScraperPipeline:
    def __init__(self):
        self.messageBusPublisher = messageBusClient.MessageBusPublisher()
        self.jsonEncoder = ScrapyJSONEncoder()


    def open_spider(self, spider):
        print("\n\n#### SPIDER - OPENED. ####\n\n")


    def process_item(self, item, spider):
        try:
            spider_name = item.get('spider_name')[0]

            print(f"\n\nPROCESSED ITEM FROM SPIDER: {spider_name}\n\n")

            #Encode the scrapy object type (item) as JSON string.
            encoded_item = self.jsonEncoder.encode(item)
            
            #Convert the encoded_item from JSON string to JSON object.
            encoded_item = json.loads(encoded_item)

            #Create haystack document type.
            #This step is needed, becouse the ElasticsearchDocumentStore stores data as haystack document types.
            #Here the meta is Dict[str, Any] and it's purpose is to hold the data from the document for easy access.
            new_document = HaystackDocument(content=encoded_item['article_data'][0], 
                                            meta={"article_prev_title": encoded_item['article_prev_title'][0],
                                                  "article_title": encoded_item['article_title'][0],
                                                  "article_data": encoded_item['article_data'][0],
                                                  "article_published_date": encoded_item['article_published_date'][0],
                                                  "article_author": encoded_item['article_author'][0],
                                                  "article_link": encoded_item['article_link'][0]})

            # Publish the Haystack document (new_document) object to the message bus (RabbitMq).
            self.messageBusPublisher.PublishDocumentForSimilarityCheck(new_document, spider_name)
        except Exception as ex:
            #TODO: Log the exception to logging service
            print(f"Exception: {ex}")


    def close_spider(self, spider):
        print("\n\n#### SPIDER - CLOSED. ####\n\n")