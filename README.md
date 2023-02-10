# DragonCore_Backend
This is primarily a backend application, with the intention to serve as a playground, innovation and testing bed for any ideas/implimentations for custom 
libraries and code logic made by Lizardon09.

## Overlying Solution Details:

Language: C# <br />
Target Framework: .NET 6.0 <br />
Dependencies:
  - Docker
  - Elastic
  - DatabaseHelper (@Lizardon09)
  - ElasticSearchHelper (@Lizardon09)

## Project Scopes:

### DragonCore

Serves as an intermediary between custom libraries/implimentations to faciliate interaction with and potentially between them.
  
### BasicHelpers

Serves to experiment with the implimentation idea of selective depency injection, with the collation of various general libraries/implimentations that have 
multiple services.

## Solution Docker-Files/Docker-Compose

In addition to the project scopes, there is a provided docker compose setup for starting up a docker environment for a single cluster Elastic instance
with Kibana hooked up. The setup creates a basic certificate policy for security for Elastic and Kibana. The certificate files can be found generated within
the 'dragoncore_certs' volume and can either be pulled down to local solution instance for debugging or pointed to from, provided appsettings of the
'DragonCore' project.
