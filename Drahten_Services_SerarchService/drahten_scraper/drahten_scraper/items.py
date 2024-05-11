# Define here the models for your scraped items
#
# See documentation in:
# https://docs.scrapy.org/en/latest/topics/items.html

import scrapy


class DrahtenScraperItem(scrapy.Item):
    article_link = scrapy.Field()
    article_prev_title = scrapy.Field()
    article_title = scrapy.Field()
    article_data = scrapy.Field()
    article_published_date = scrapy.Field()
    article_author = scrapy.Field()
    spider_name = scrapy.Field()
