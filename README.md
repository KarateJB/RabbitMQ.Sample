# RabbitMQ.Sample

Sample codes for RabbitMQ

## Install RabbitMQ

```
$ docker pull rabbitmq:3.6
$ docker run -d -p 5672:5672 -p 15672:15672 --name tis-rabbitmq rabbitmq:3.6
$ docker exec -it tis-rabbitmq rabbitmq-plugins enable rabbitmq_management
```
