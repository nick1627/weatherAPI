# weatherAPI
Web API for home weather station project, using InfluxDb for storage of the timestamped data series.

You will need to get the influxdb running first. Then do:
    dotnet user-secrets init
    dotnet user-secrets set "InfluxDB:DBToken" "your-secret-token"

TODO: tidy this up!


==========
workflow for database (just db in docker atm):
start the db
docker compose up -d

see if it's running:
docker compose ps

view its logs:
docker compose logs -f influxdb

stop it:
docker compose down

to stop it and wipe the db to start afresh
docker compose down -v

At this point as the api isn't in docker-compose, you just run it with f5


=====
Not using docker yet, so:
Start db in a different terminal window: influxdb3
THen in vscode do fn f5 -> C# -> pick a (launchsettings) config, http is fine when it's local


====
Working with InfluxDB3:
export INFLUXDB3_AUTH_TOKEN=<token>
Commands will now be authenticated.
Can also use the --token option.