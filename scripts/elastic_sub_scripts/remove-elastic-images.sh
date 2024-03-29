#!/bin/bash

source ./scripts/elastic_sub_scripts/elastic-config.sh

echo -e "\nImage cleanup ... \n\n"

if [[ "$(docker ps -a --filter name="$elasticContianerName" | wc -l)" -gt 1 || "$(docker ps -a --filter name="$kibanaContainerName" | wc -l)" -gt 1 ]]; then
    echo -e "Error!!! Containers still exist. Remove those first..."
	exit 64
else
	docker rmi $elasticImageName
	docker rmi $elasticRemoteImage
	echo -e "\n\nRemoved 'elasticsearch-image'!"

	docker rmi $kibanaImageName
	docker rmi $kibanaRemoteImage
	echo -e "\n\nRemoved 'kibana-image'!"

	echo -e "\n\nDone image cleanup!"
fi

