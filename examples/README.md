# Examples

In this directory will be example character packs

## Gus.zip
This is a basic example of a character pack. A json file with image(s).
The json data is a copy of Antonio's settings with the starting weapon changed to an AXE. Check the Vampire Survivors wiki for the names to use for starting weapon (the ID tag). 

## Example json:
With version 0.1, the character json is a copy of what vampire survivors use as serialized data of their characters. This character "Gus" is a copy of Antonio's data.

The format of this json body will be changing in future releases, but I'm hoping to keep it somewhat backwards compatible after the first release.

## Changes from v0.1 to v0.2
Character json file format v0.2 is out!
	- version "0.1" -> "0.2"
	- Renamed character -> characters
	- characters.skins.frames - Names of each individual walking frame
	- characters.onEveryLevelUp - A statModifier object that effects the character every level.
	- characters.statModifiers - Moved initial stats from the base character object to this array. (Should have level: 1)
	- Multiple characters in one pack is probably supported but untested :)

## v0.2:

<details>

<summary>Copyable json v0.2</summary>

```json
```
</details>

<details>

<summary>Json with comments</summary>

```jsonc
```


</details>
