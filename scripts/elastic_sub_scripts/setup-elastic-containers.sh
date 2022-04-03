#!/bin/bash

source ./scripts/elastic_sub_scripts/elastic-config.sh

echo -e "\n\nProceeding with container setups: \n\n"

#create elastic container in network, also set max heap size required: https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html

echo -e "Creating $elasticContianerName container ..."

if [ "$(docker ps -a --filter name="$elasticContianerName" | wc -l)" -gt 1 ]; then
	echo -e "Container $elasticContianerName already exists..."
else
	docker pull $elasticRemoteImage
	docker tag $elasticRemoteImage $elasticImageName
	docker run -d --name $elasticContianerName --net $elasticNetworkName -p 9200:9200 -p 9300:9300 -t $elasticImageName
	echo -e "\nContainer $elasticContianerName created..."
fi

#create kibana container in network

echo -e "\n\nCreating 'dragoncore-kibana' container ..."

if [ "$(docker ps -a --filter name="$kibanaContainerName" | wc -l)" -gt 1 ]; then
	echo -e "Container $kibanaContainerName already exists..."
else
	docker pull $kibanaRemoteImage
	docker tag $kibanaRemoteImage $kibanaImageName
	docker run -d --name $kibanaContainerName --net $elasticNetworkName -p 5601:5601 -t $kibanaImageName
	echo -e "Container $kibanaContainerName created..."
fi

echo -e "\n\nContainer setups done!"