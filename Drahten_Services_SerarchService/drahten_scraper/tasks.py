from celery import shared_task
from billiard import Process
from scrapy.crawler import CrawlerProcess
from scrapy.settings import Settings
from drahten_scraper.drahten_scraper import settings as drahten_scraper_settings
from drahten_scraper.drahten_scraper.spiders.cybersecurity_news_europe import CybersecurityNewsEuropeSpider


class CrawlerInitializer():
    def __init__(self):
        self.crawler_settings = Settings()
        self.crawler_settings.setmodule(drahten_scraper_settings)
        self.crawler = CrawlerProcess(settings=self.crawler_settings)
    
    def _crawl(self):
        self.crawler.crawl(CybersecurityNewsEuropeSpider)
        self.crawler.start()
        self.crawler.stop()
    
    def crawl(self):
        process = Process(target=self._crawl)
        process.start()
        process.join()


crawler = CrawlerInitializer()

@shared_task
def start_crawler():
    crawler.crawl()