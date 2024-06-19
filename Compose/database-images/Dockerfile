FROM postgres:14.2

# Set non-interactive mode to automatically handle prompts
ENV DEBIAN_FRONTEND=noninteractive

# Install PostgreSQL contrib package which includes pgcrypto extension
RUN apt-get update && apt-get install -y postgresql-contrib

# Create pgcrypto extension
COPY create_extension.sql.sql /docker-entrypoint-initdb.d/