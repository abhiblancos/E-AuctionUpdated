Kafka queries 

.\bin\windows\zookeeper-server-start.bat config\zookeeper.properties

.\bin\windows\kafka-server-start.bat config\server.properties


kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic test