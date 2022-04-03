#!/bin/bash

# set max heap count for elastic container

echo -e "\n\nNOTE: Ensure 'vm.max_map_count=262144' for elastic container, as required!!! Otherwise the elastic container will stop!!!"

# shopw global variables

source ./scripts/elastic-config.sh

echo -e "\nGlobal variables:\n"

echo -e "elasticContianerName = $elasticContianerName"
echo -e "elasticImageName = $elasticImageName"
echo -e "elasticRemoteImage = $elasticRemoteImage \n"

echo -e "kibanaContainerName = $kibanaContainerName"
echo -e "kibanaImageName = $kibanaImageName"
echo -e "kibanaRemoteImage = $kibanaRemoteImage \n"

echo -e "elasticNetworkName = $elasticNetworkName \n"

# preliminary remove existing conatiners and images

sh ./scripts/remove-elastic-containers.sh

sh ./scripts/remove-elastic-images.sh

# create elastic network if it doesnt exist

sh ./scripts/setup-elastic-netowrk.sh

# create elastic containers

sh ./scripts/setup-elastic-containers.sh