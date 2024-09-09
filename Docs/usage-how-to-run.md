# Docker deployment

Make sure that the [prerequisites](dev-run-prerequisites.md) have been met before continuing

# Instances

* Integration
  - RabbitMq - 1 
* Data storage
  - PostgreSQL - 4
  - Elasticsearch - 1
* Data Collection
  - Celery - 2
* Authentication and Authorization
   - Keycloak - 1
* Observability
  - Logging - 1 ElasticSearch, 1 Kibana


## Application services

The convenience of `docker compose` makes it possible to run exactly what is needed:

Run all services:
Navigate to the root directory of the project -> Drahten\Compose\ and run `docker-compose up`
After this stop the following containers:
 - celery_beat
 - celery_worker
 - search_service_grpc_servoce
 - search_service_consumer_server
 - search_service

Run the stopped containers once the `setup-1` container has stopped. This signals that the elasticsearch containers are started properly.
The stoping of the above containers is needed because if the elasticsearch containers are not fully started the services would not be able to make proper connection with the elasticsearch instancies and would not work.

# ⚠ Caution
Remember that the elasticsearch and postgresql containers would need TSL certificates and local variables for passwords and other user information. In the docker-compose.yml file there are `env_file` sections.
Generate the following files (the names of the files are not important and you can replace them if you want, just make shure that the docker-compose.yml will point to the new names):
 - database-privatehistory-service.dev.env 
     ```html
        < Example configuration >
     
       POSTGRES_DB: PrivateHistoryServiceDb --> The database name
       POSTGRES_USER: PrivateHistoryServiceAdmin --> The database user name
       POSTGRES_PASSWORD: password --> The database user password
     ```
- database-publichistory-service.dev.env
  ```html
     < Example configuration >
     
       POSTGRES_DB: PublicHistoryServiceDb --> The database name
       POSTGRES_USER: PublicHistoryServiceAdmin --> The database user name
       POSTGRES_PASSWORD: password --> The database user password
  ```
- database-topic-article-service.dev.env
   ```html
     < Example configuration >
     
       POSTGRES_DB: TopicArticleServiceDb --> The database name
       POSTGRES_USER: TopicArticleServiceAdmin --> The database user name
       POSTGRES_PASSWORD: password --> The database user password
  ```
- database-user-service.dev.env
  ```html
     < Example configuration >
     
       POSTGRES_DB: UserServiceDb --> The database name
       POSTGRES_USER: UserServiceAdmin --> The database user name
       POSTGRES_PASSWORD: password --> The database user password
  ```
- database.dev.env
  ```html
     < Example configuration >
     
       PGADMIN_DEFAULT_EMAIL: admin@admin.com --> PG_ADMIN username
       PGADMIN_DEFAULT_PASSWORD: root --> PG_ADMIN password
  ```
- django_restapi.dev.env
  ```html
     < Example configuration >
     
      CERT_FILE=/certs/server.crt --> The locally generated TLS certificate
      KEY_FILE=/certs/server.key --> The certificate key
  ```
- keycloak_database.dev.env
   ```html
     < Example configuration >
     
      POSTGRES_DB: keycloack_db --> The database name
      POSTGRES_USER: keycloak_admin --> The database user name
      POSTGRES_PASSWORD: password --> The database user password
      PGADMIN_DEFAULT_EMAIL: admin@admin.com 
      PGADMIN_DEFAULT_PASSWORD: root
  ```
- keycloak.dev.env
  ```html
     < Example configuration >
     
     KC_DB: postgres --> Defines the database provider.
     KC_DB_URL_HOST: keycloak_postgres --> Defines the postgres container, that will be used by keycloak
     KC_DB_URL_DATABASE: keycloack_db --> The name of the postgres database
     KC_DB_PASSWORD: password --> The database passwod
     KC_DB_USERNAME: keycloak_admin --> The database username
     KC_DB_SCHEMA: public
     KEYCLOAK_ADMIN: root 
     KEYCLOAK_ADMIN_PASSWORD: root
  ```
- logstash.conf
    ```html
     < Example configuration >
     
      input {
          http {
              port => 5000
              additional_codecs => { "application/json" => "json_lines" }
          }
      }
      
      output {
          elasticsearch {
              index => "logstash-%{+YYYY.MM.dd}"
              hosts=> "${ELASTIC_HOSTS}"
              user=> "${ELASTIC_USER}"
              password=> "${ELASTIC_PASSWORD}"
              cacert=> "certs/ca/ca.crt"
          }
      }
  ```
