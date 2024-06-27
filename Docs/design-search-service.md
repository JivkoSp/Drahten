# Search Service - Responsible for actions related to searching information from the internet on topics offered by the application

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/SearchService-1.PNG" alt="Logo" width="450">
</p>

## The components of the diagram have the following meaning

* **Actor** - This can be a user or a process from the application. The user or process requests information. In the diagram, an example request for "Cybersecurity news from Europe and America" is shown. The request is forwarded to the Search Service API, which then returns a response containing the appropriate HTTP code and information (if found).
   
* **Search Service API** - A web API providing various endpoints. The diagram shows two such endpoints: "Find news for Europe" and "Find news for America," responding to HTTP GET requests for information requested by third parties/users. When a request is made to one of the provided endpoints, it is forwarded to Haystack, where it is processed and sent to ElasticSearch. ElasticSearch returns a response in JSON format to the party/user who made the request to the Search Service API.
   
* **JWT Authentication & Authorization** - A layer for authentication and authorization, requiring JWT tokens to authenticate the party/user requesting access to protected resources from the Search Service API.
   
* **Web Crawler** - An internet bot that systematically browses web pages to extract information.
   
* **ElasticSearch** - Used for searching and storing information extracted by Web Crawler processes.
   
* **Haystack** - Acts as a wrapper for ElasticSearch to build capabilities for semantic search. Together, Haystack and ElasticSearch form a search engine for semantic information retrieval. The information passed to Haystack by Web Crawler processes is processed using techniques such as removing unnecessary white spaces, tokenization (e.g., splitting text by words or sentences), etc. After processing, the information is stored in ElasticSearch. Information to be retrieved from ElasticSearch through an HTTP request by third parties/users to Haystack is either forwarded directly to ElasticSearch (if the request does not require semantic information retrieval) or processed by Haystack using techniques such as retrieving information from ElasticSearch that matches the request using the TF-IDF algorithm. The result is then passed to a transformer model that provides the final result for the request. The result is returned in JSON format to the party/user who made the request to the Search Service API.
   
* **Scrapy** - Used for extracting information from websites through Web Crawler processes.
   
* **Celery Scheduler** - Used to execute Scrapy at predefined intervals.
   
* **Web sites** - Web pages from which the Web Crawler extracts information.
    
* **Logging** - Used for recording information about various events in the Search service. This information is forwarded to the Log Collection Service.
