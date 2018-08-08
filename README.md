# RabbitMQ + ELK Log Stack

## Executanto a stack de Log (necessário ter o docker instalado na máquina)
Execute os comandos em sequencia:

```git clone https://github.com/fabricioveronez/NLog-ELK.git```

```cd ./NLog-ELK/DockerContainers```

```docker-compose up -d```

## Como acessar a stack 

### RabbitMQ
Utilize a url **[http://localhost:15672/](http://localhost:15672/)** para acessar e  **Usuário**: ```logUser``` e **Senha**: ```logPwd```

### ElasticSearch
Será alimentado pelo Logstash e consultado pelo Kibana

### LogStash
Criará a fila ApplicationsLog no virtual host Log, e irá enviar os dados que escutar da fila para o Elasticsearch

### Kibana
Utilize a url  **[http://localhost:5601/](http://localhost:5601/)** para acessar o Kibana

Baseado no projeto [EnterpriseApplicationLog](https://github.com/docker-gallery/EnterpriseApplicationLog) do - [Luis Carlos Faria](http://gago.io/)


