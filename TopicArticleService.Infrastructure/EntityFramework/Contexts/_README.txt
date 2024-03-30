This directory contains two DbContext classes - ReadDbContext and WriteDbContext. 
-------------------------
The ReadDbContext contains DbSet<T> properties that represent database models (i.e have suffix of ReadModel) and the configuration for this models (the configuration is contained in the
ModelConfiguration.ReadConfiguration directory).
-------------------------
The WriteDbContext contains DbSet<T> properties that represent domain entities and value objects and the configuration for them 
(the configuration is contained in the ModelConfiguration.WriteConfiguration directory).