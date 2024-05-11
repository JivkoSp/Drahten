import scrapy
from drahten_scraper.drahten_scraper.items import DrahtenScraperItem
from scrapy.loader import ItemLoader


class CybersecurityNewsEuropeSpider(scrapy.Spider):
    name = "CybersecurityNewsEurope"
    start_urls = ["https://cybersecurity-centre.europa.eu/news_en"]

    def parse(self, response):
        new_articles = response.css("article.ecl-content-item")

        for news_aricle in new_articles:
            news_article_loader = ItemLoader(item=DrahtenScraperItem(), selector=news_aricle)
            
            news_aricle_link = news_aricle.css("div a::attr(href)").get()

            news_article_loader.add_css('article_published_date', "div ul li time::text")
            news_article_loader.add_value('article_link', f"https://cybersecurity-centre.europa.eu{news_aricle_link}")
            news_article_loader.add_css('article_prev_title', "div p::text")

            # Set the spider_name field
            news_article_loader.add_value('spider_name', self.name)

            yield scrapy.Request(
                url = f"https://cybersecurity-centre.europa.eu{news_aricle_link}",
                callback = self.parse_article_data,
                cb_kwargs = dict(loader=news_article_loader)
            )

    def parse_article_data(self, response, loader):
        article_body = response.css("body")
        loader.selector = article_body
        loader.add_css('article_title', "div h1 span::text")
        article_body_text_list = article_body.xpath('//div[@class="ecl"]//p//text()').getall()
        article_body_text_string = ""

        for element in article_body_text_list:
            article_body_text_string += element
        
        loader.add_value('article_data', article_body_text_string)
        loader.add_css('article_author', "dl dd a::text")

        yield loader.load_item()