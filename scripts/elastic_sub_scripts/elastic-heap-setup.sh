#!/bin/bash

source ./scripts/elastic_sub_scripts/elastic-config.sh

echo -e "\nNOTE: Ensure 'vm.max_map_count=$vm_max_map_count' for elastic container, as required!!! Otherwise the elastic container will stop!!!"

echo -e "\nCurrent vm.max_map_count = "

wsl -d docker-desktop cat /proc/sys/vm/max_map_count

echo -e "\n Trying to set vm.max.map_count..."

wsl -d docker-desktop sysctl -w vm.max_map_count=$vm_max_map_count

echo -e "\nCurrent vm.max_map_count = "

wsl -d docker-desktop cat /proc/sys/vm/max_map_count