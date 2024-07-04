# Semantic Search

One of the tasks of the current application is to deliver semantic search capabilities. In the [Data Storage](Docs/design-decision-data-storage.md) section 
of the current documentation, I specified why Elasticsearch will be needed to achieve semantic search in the application. 
However, Elasticsearch is only one part of the puzzle. Applying semantic search in a microservices application involves various challenges, such as the complexity and diversity of underlying framework technologies. It requires working with different data storage systems, indexing mechanisms, query languages, machine learning models, and integrating all these components into a cohesive and scalable solution. This can be costly, time-consuming, and may result in errors in the final solution. 

## Why Choose Haystack?

* Haystack provides a modular and flexible architecture that facilitates easy combination of various components for adding, preprocessing, extracting using different algorithms (such as TF-IDF), and ranking information;
* Haystack supports various databases like Elasticsearch, SQL, and enables the use of state-of-the-art language models for semantic search, such as BERT, GPT, and T5.
