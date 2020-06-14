# FoundryLanding
Discord auth and multiple server login for Foundry VTT.

# Setup
## Access to Foundry VTT data files
Make sure FoundryLanding has access to the world data for each instance of Foundry VTT that it's managing.

If running through docker, mount the parent directory of the foundry instance(s) to /foundry/data in the container:
```
/path/to/foundry/stuff  <---- Point Foundry Landing here
+- /instance-data-1 <---- one foundry server
|  +- /Config
|  |  +- options.json
|  +- /Data
|  |  +- /worlds
|  |     +- /<world name>
|  |        +- world.json
|  +- /Logs
+- /instance-data-2 <---- another foundry server
|  +- /Config
|  +- /Data
|  +- /Logs
...
+- /instance-data-n <---- how many do we have?
   +- /Config
   +- /Data
   +- /Logs
```

## Add URL to foundry config.json
Add a string value named `"url"` to the end of `<path to foundry data>/Config/options.json`, containing the base URL of the Foundry VTT instance *without a trailing slash*.

## (Optional) Add owners to world.json
Add a JSON array called `"owners"` to the end of `<path to foundry data>/Data/worlds/<world name>/world.json`. Each entry in this array is a string containing the discord username, e.g.:
```JS
"owners": ["nobody#2511", "somebody#1530"]
```

## (Optional) Add discord users to foundry users
Run this in the JS console when logged into Foundry VTT as a GM:
```JS
((name, discord)=>game.users.find(u => u.name == name).setFlag("world", "discord-user", discord))("<foundry user name>", "<discord user name#abcd")
```

Users with no discord name attached to them will be available to *anyone* with access to the landing page.
