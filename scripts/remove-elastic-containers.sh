#!/bin/bash

source ./scripts/elastic-config.sh

echo -e "\n\n Proceeding with container cleanup: \n\n"
  
# remove existing dragoncore-elastic container if it exists

if [ "$(docker ps -a --filter name="$elasticContianerName" | wc -l)" -gt 1 ]; then
    echo -e "Container $elasticContianerName exists, removing ..."
	docker stop $elasticContianerName && docker rm $elasticContianerName
else
	echo -e "$elasticContianerName container does not exist"
fi
	
# remove existing dragoncore-kibana container if it exists
	
if [ "$(docker ps -a --filter name="$kibanaContainerName" | wc -l)" -gt 1 ]; then
    echo -e "Container $kibanaContainerName exists, removing ..."
	docker stop $kibanaContainerName && docker rm $kibanaContainerName
else
	echo -e "$kibanaContainerName container does not exist"
fi

echo -e "\n\n Container cleanup done!"