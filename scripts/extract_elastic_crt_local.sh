#!/bin/bash

if [ "$(docker ps -a --filter name="dragoncoreElastic" | wc -l)" -gt 1 ]; then
    echo -e "Container dragoncoreElastic exists, extracting ca.crt..."
	docker cp dragoncoreElastic:/usr/share/elasticsearch/config/certs/ca/ca.crt ./DragonCore.API
	echo -e "Extracted Successfully"
else
	echo -e "dragoncoreElastic container does not exist"
fi

