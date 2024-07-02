# Log Collection Service

## Responsible for collecting and recording information in an Elasticsearch database regarding events occurring in the services offered by the application

<p align="center">
    <img src="https://raw.githubusercontent.com/JivkoSp/Drahten/master/Assets/LogCollectionService-1.PNG" alt="Logo" width="450">
</p>

### The components of the diagram have the following meaning

* **Data source** - The source of data. It can be a process or service from the application;
* **Log Aggregator** - Log collector, responsible for:
  - Receiving logs from predefined types of input data (Inputs);
  - Optionally processing the input data (Filters) before sending them to output destinations (Outputs).
* **Data base** - Log repository;
* **GUI** - Graphical interface for viewing logs from the repository;
* **Actor** - Person responsible for system administration.
