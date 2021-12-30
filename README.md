# FilmesAPI

This has developed with csharp and dotnet using docker containers.
## For Run this project:

### __Requirements:__

  > * Visual Studio IDE
  > * dotnet 6
  > * Docker

### Steps:

#### 1. MS Sql Server

Get the image
>```powershell
>docker pull mcr.microsoft.com/mssql/server
>```

Run the image
>```powershell
>docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<your-password>" -p 1433:1433 -d mcr.microsoft.com/mssql/server
>```

#### 2. Run Api container on Visual Studio

#### 3. Create local Network

to connect api container on sql container is necessary a local network

Create the local network
>```powershell
>docker network create <your-network-name>
>```

Add containers to network *__obs:__ **Check your-container-name using 'docker ps' command**
>```powershell
>docker network connect <your-network-name> <your-container-name>
>```

Check the container address
>```powershell
>docker network inspect <your-network-name> 
>
>```
return:
>```json
> "Containers": {
>            "20207480ec58be02ed08f70333ab1711f7256bf299f858fd8951e6d57d737d7e": {
>                "Name": "FilmesAPI",
>                "EndpointID": ".............",
>                "MacAddress": ".............",
>                "IPv4Address": "172.18.0.3/16",
>                "IPv6Address": ""
>            },
>            "f42474a1f631e909bcb61c785f9bbe8d1c9a35999808e225ae78459bcd33a13c": {
>                "Name": "SqlServer2019",
>                "EndpointID": ".............",
>                "MacAddress": ".............",
>                "IPv4Address": "172.18.0.2/16",
>                "IPv6Address": ""
>            }
>       }
>```

Change the ConnectionStrings on **appsettings.json**
```json
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "FilmeConnection": "server=<your-sql-container-ip>,1433;database=filmeDb;user=sa;password=<your-password>"
  }
```
