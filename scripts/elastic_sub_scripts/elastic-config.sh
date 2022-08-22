#!/bin/bash

elasticContianerName="dragoncore-elastic"
elasticImageName="elasticsearch-image"
elasticRemoteImage="docker.elastic.co/elasticsearch/elasticsearch:8.1.1"

kibanaContainerName="dragoncore-kibana"
kibanaImageName="kibana-image"
kibanaRemoteImage="docker.elastic.co/kibana/kibana:8.1.1"

elasticNetworkName="dragoncore-elastic-network"

vm_max_map_count=262144