import requests
from loguru import logger

class HttpHandler:
    def __init__(self, url):
        self.url = url

    def write(self, message):
        log_entry = message.record["message"]
        headers = {'Content-type': 'application/json'}
        try:
            response = requests.post(self.url, data=log_entry, headers=headers)
            response.raise_for_status()
        except requests.exceptions.RequestException as ex:
            print(f"\nException sending log entry: {ex}\n")
        except Exception as ex:
            print(f"\nException: {ex}\n")


logstash_url = 'http://logstash:5000'
http_handler = HttpHandler(logstash_url)

# Configure loguru to use the custom HTTP handler
logger.remove()  # Remove the default handler
logger.add(http_handler, serialize=True)