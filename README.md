# RabbitMQ.Sample

Sample codes for RabbitMQ

## Install RabbitMQ

```
$ docker pull rabbitmq:3.6
$ docker run -d -p 5672:5672 -p 15672:15672 --name <your_container_name> rabbitmq:3.6
$ docker start <your_container_name>
$ docker exec -it <your_container_name> rabbitmq-plugins enable rabbitmq_management
```

We can enable the RabbitMQ Management plugin later by,

```
$ rabbitmq-plugins enable rabbitmq_management
$ rabbitmq-plugins list -e
 Configured: E = explicitly enabled; e = implicitly enabled
 | Status:   * = running on rabbit@1f3f73f8659e
 |/
[e*] amqp_client               3.6.16
[e*] cowboy                    1.0.4
[e*] cowlib                    1.0.2
[E*] rabbitmq_management       3.6.16
[e*] rabbitmq_management_agent 3.6.16
[e*] rabbitmq_web_dispatch     3.6.16
```


> To skip the steps of enabling RabbitMQ Management plugin, use the docker image with tag name `<version>-management` 

Got to http://localhost:15672 for the RabbitMQ Management UI.
There is a default user/pwd: guest/guest, is allowed to login thru localhost.
However, it's recommened to delete "guest" or change the password/permissions of "guest".




## Create new Virtual host


Create a new virtual host named "vhost_demo" by

```
$ rabbitmqctl add_vhost vhost_demo
$ rabbitmqctl list_vhosts
/
vhost_demo
```


## Create new User (as Administrator)


```
$ rabbitmqctl add_user rabbitmquser rabbitmqpwd
$ rabbitmqctl set_user_tags rabbitmquser administrator
$ rabbitmqctl set_permissions -p vhost_demo rabbitmquser ".*" ".*" ".*"

$ rabbitmqctl list_users

Listing users
rabbitmquser    [administrator]
guest   [administrator]
```



### Delete the queue 


Delete the queue on RabbitMQ Management UI:




Or thru `rabbitmqadmin`

```
$ rabbitmqadmin delete queue name=<queue_name>
```