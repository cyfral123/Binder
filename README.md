### About 

This mod allows you to easily bind console commands to keyboard keys. 
All binds are automatically saved to the mod's config file and will persist between game sessions.
### Commands
```
bind add <key> <command>
bind modify appendto <key> <command>
bind modify removefrom <key> <position>
bind remove <key>
bind list
```

### Example
#### Creating bind:
```
bind add G noclip
```
```
bind add K loadlevels Campaign_interlude_Silo_To_Pipeworks_01
```

> [!WARNING]
> Pressing any bind key will activate cheat mode and disable run scoring.

#### Modifying a Bind:

You can modify existing key binds to remove or add individual commands without deleting the entire bind.

Example: suppose you have multiple commands bound to the N key:
```
N -> noclip, godmode, fullbright, infinitestamina
```
###### Removing a command
> To remove a command from a bind, use the removefrom action with the position of the command (1-based index):
```
bind modify removefrom N 3
```
```
N -> noclip, godmode, infinitestamina
```
###### Adding a command
> To append a new command to the end of an existing bind, use the appendto action:
```
bind modify appendto N notarget
```
```
N -> noclip, godmode, infinitestamina, notarget
```

#### To clear a bind:
```
bind clear G
```

#### To list all current binds:
```
bind list
```
