#!/bin/bash

# show global variables

source ./scripts/elastic_sub_scripts/elastic-config.sh

echo -e "\nGlobal variables:\n"

echo -e "elasticContianerName = $elasticContianerName"
echo -e "elasticImageName = $elasticImageName"
echo -e "elasticRemoteImage = $elasticRemoteImage \n"

echo -e "kibanaContainerName = $kibanaContainerName"
echo -e "kibanaImageName = $kibanaImageName"
echo -e "kibanaRemoteImage = $kibanaRemoteImage \n"

echo -e "elasticNetworkName = $elasticNetworkName \n"

echo -e "vm_max_map_count = $vm_max_map_count \n"

# set max heap count for elastic container

sh ./scripts/elastic_sub_scripts/elastic-heap-setup.sh

# preliminary remove existing conatiners and images

sh ./scripts/elastic_sub_scripts/remove-elastic-containers.sh

sh ./scripts/elastic_sub_scripts/remove-elastic-images.sh

# create elastic network if it doesnt exist

sh ./scripts/elastic_sub_scripts/setup-elastic-network.sh

# create elastic containers

sh ./scripts/elastic_sub_scripts/setup-elastic-containers.sh