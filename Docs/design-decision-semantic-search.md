# Semantic Search

One of the tasks of the current application is to deliver semantic search capabilities. In the [Data Storage](Docs/design-decision-data-storage.md) section 
of the current documentation, I specified why Elasticsearch will be needed to achieve semantic search in the application. 
However, Elasticsearch is only one part of the puzzle. Applying semantic search in a microservices application involves various challenges, such as the complexity and diversity of underlying framework technologies. It requires working with different data storage systems, indexing mechanisms, query languages, machine learning models, and integrating all these components into a cohesive and scalable solution. This can be costly, time-consuming, and may result in errors in the final solution. 

## Why Choose Haystack?

* Haystack provides a modular and flexible architecture that facilitates easy combination of various components for adding, preprocessing, extracting using different algorithms (such as TF-IDF), and ranking information;
  
* Haystack supports various databases like Elasticsearch, SQL, and enables the use of state-of-the-art language models for semantic search, such as BERT, GPT, and T5.

## Combining Elasticsearch and Haystack to create Semantic Search Engine

Elasticsearch and Haystack make an excellent combination for building modern search solutions. Some of the advantages of using them together include:

#### Easy Integration
Haystack provides a convenient wrapper for Elasticsearch, defined as ElasticsearchDocumentStore, which allows for easy document indexing, querying, and updating using Python. Haystack also supports other document stores, such as SQL, which can be used alongside Elasticsearch for various use cases.

#### Scalability
Elasticsearch can scale horizontally across multiple nodes and clusters, handling large volumes of data. Haystack can also scale components up or down when constructing search pipelines, depending on the load and available resources. For example, documents can be retrieved using the BM25Retriever, based on the TF-IDF algorithm, or the EmbeddingRetriever, which utilizes a language model. These are two different methods for information retrieval, and the choice between them can affect the speed, accuracy, and overall complexity of the search engine.

#### Flexibility
Elasticsearch and Haystack allow for the creation of customized search solutions by choosing different components and language models for various domain-specific tasks. They support different formats and languages, making them suitable for diverse and multilingual data and queries.
