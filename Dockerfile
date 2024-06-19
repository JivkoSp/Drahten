FROM python:3.10

ENV PYTHONUNBUFFERED=1

WORKDIR /usr/src/app

COPY Drahten_Services_SerarchService/requirements.txt ./

RUN pip install -r requirements.txt