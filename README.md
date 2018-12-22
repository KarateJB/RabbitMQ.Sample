# RabbitMQ.Sample

Sample codes for RabbitMQ

## Install RabbitMQ

```
$ docker pull rabbitmq:3.6
$ docker run -d -p 5672:5672 -p 15672:15672 --name tis-rabbitmq rabbitmq:3.6
$ docker exec -it tis-rabbitmq rabbitmq-plugins enable rabbitmq_management
```

Got to http://localhost:15672 for the RabbitMQ Management UI.
The localhost allows the default user/pwd: guest/guest, to login.
However, it's recommened to change the password or permissions of "guest".



First add user and grant it as Admin:

```
$ rabbitmqctl add_user rabbitmquser rabbitmqpwd
$ rabbitmqctl set_user_tags rabbitmquser administrator
$ rabbitmqctl set_permissions -p / rabbitmquser ".*" ".*" ".*"

$ rabbitmqctl list_users

Listing users
rabbitmquser    [administrator]
guest   [administrator]
```



(1) 超級管理員(administrator)

可登陸管理控制枱(啟用management plugin的情況下)，可查看所有的信息，並且可以對用户，策略(policy)進行操作。

(2) 監控者(monitoring)

可登陸管理控制枱(啟用management plugin的情況下)，同時可以查看rabbitmq節點的相關信息(進程數，內存使用情況，磁盤使用情況等)

(3) 策略制定者(policymaker)

可登陸管理控制枱(啟用management plugin的情況下), 同時可以對policy進行管理。但無法查看節點的相關信息(上圖紅框標識的部分)。

與administrator的對比，administrator能看到這些內容

(4) 普通管理者(management)

僅可登陸管理控制枱(啟用management plugin的情況下)，無法看到節點信息，也無法對策略進行管理。

(5) 其他

無法登陸管理控制枱，通常就是普通的生產者和消費者。