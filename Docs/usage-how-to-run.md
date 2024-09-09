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
 - database-privatehistory-service.dev.env - Must contain: POSTGRES_DB: - this is the database name; POSTGRES_USER: - this is the database user name; POSTGRES_PASSWORD: - this is the database user password
    - Example configuration: `POSTGRES_DB: PrivateHistoryServiceDb
                              POSTGRES_USER: PrivateHistoryServiceAdmin
                              POSTGRES_PASSWORD: password`



