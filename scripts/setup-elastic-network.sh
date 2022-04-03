#!/bin/bash

source ./scripts/elastic-config.sh

echo -e "\n\n Proceeding with elastic network setup: \n\n"

if [ ! "$(docker network ls --filter name=$elasticNetworkName | wc -l)" -gt 1 ]; then
	echo -e "Creating $elasticNetworkName network ..."
	docker network create $elasticNetworkName
else
  echo -e "$elasticNetworkName network exists, no setup needed"
fi

echo -e "\n\n Elastic network setup done!"