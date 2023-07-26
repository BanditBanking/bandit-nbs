# :bank: BANDIT NBS :bank:

# Description

This repository contains the code related to the National Bank Server, which handles analytics commands in order to retrieve data
for the databases BD_OPER_TRANSAC and BD_OPER_PROC_AUTH
 
## Commands

The server listens for the following commands :

- **ChallengeAnalyticsCommand** : Used to send multiple challenges at once to the nbs server
- **TransactionAnalyticsCommand** : Used to send a unique transaction to the nbs server

# Run the NBS server

## Prerequisites 

- First of all, the NBS server requires the usage of a .pfx file to allow its SSL communications. But be carefull :
  > :warning: **For security reason, the required pfx file is not included in the repository.** You must import it locally inside the /certs directory. This .pfx file is located on the VPS at /etc/pki/tls/tristesse.pfx

- Then, you'll need to install **Docker Desktop** to be able to launch a :whale2: dockerized application :whale2:. 
  > :link: If you don't already have it installed, here is the tutorial : https://docs.docker.com/desktop/install/windows-install/

- Finally, you may need to use **Visual Studio**. Jetbrains Rider should also work but for the moment, the installation steps will only be described for Visual Studio

## Run the code

The NBS server depends on two Databases : **PostgreSQL** and **MongoDB**. Fortunately, the project includes a **docker-compose** configuration to help you setup the proper environement, you just need to launch the project using it and see the :zap: magic :zap: happens! Here's how to use it:

 - 1. Set the "docker-compose" project as the default starting application : 
 
    ![image](https://user-images.githubusercontent.com/91737697/226138025-b31dba3e-dd7c-4689-afa4-dcc4ae7895be.png)

 - 2. Run the code using the green arrow : 

    ![image](https://user-images.githubusercontent.com/91737697/226138242-c6c0da1a-4f9d-4eda-b12b-623203cc6e6a.png)
    
 - 3. You should see 3 containers up and running in Docker Desktop: (Gnagnagna it's the screen form the ACS, olé olé olé je m'en fous 🎉)
 
    ![image](https://user-images.githubusercontent.com/91737697/226138519-98756ffa-48e6-4e6a-8426-f1de7606a13c.png)

 - 4. You should now be able to connect to the NBS Server !

## Edit run configuration

You can edit the run configuration by customizing the **docker-compose** file which is located here :

```
/docker/docker-compose.yml
```

### Bandit NBS Parameters 
      
- **NBS__NpgsqlDatabase__DatabaseName** : Database name for the Npgsql Database
- **NBS__NpgsqlDatabase__ConnectionString** : Connection string to connect to the Npgsql Database
- **NBS__MgdbDatabase__DatabaseName** : Database name for the Mgdb Database
- **NBS__MgdbDatabase__ConnectionString** : Connection string to connect to the Mgdb Database
- **NBS__SSL__ServerCertificate** : Sets the name of the server certificate (must correspond to local certificate filename)
- **NBS__TCP__Port** : Sets the TCP listening port (docker-compose's port configuration must match this one)
- **Logging__LogLevel__Default** : Sets the log level of the application (use "Information" or "None" for production, "Debug" in Developpement) 

  > :question: For further information about docker-compose configuration, you can check this link : https://docs.docker.com/compose/
