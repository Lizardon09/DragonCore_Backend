#!/bin/bash

source ./scripts/elastic_sub_scripts/elastic-config.sh

# preliminary remove existing conatiners and images

sh ./scripts/elastic_sub_scripts/remove-elastic-containers.sh

sh ./scripts/elastic_sub_scripts/remove-elastic-images.sh