# SaveScumAgent savegame manager

This is a ***very early work-in-progress*** application with a single purpose - to assist with save scumming in roguelike games.

I'm building it because I was playing State of Decay, and a physics glitch cost me not only a truck full of ammo, but also the lives of 3 party members. Oh, the profanity I invented that day... So I whipped up a quick console application that created a zip archive of my entire savegame folder every time the game saved.

Then, being me, I thought "shit, I could make something way better than this". So I started doing that.

Anyway, here's the feature list that will be included in the final version:

 * Watches a savegame folder for changes, and automatically archives them
 * Support for multiple archiving methods, including .zip files and git repositories
 * Only watches savegame folders when a supported game is detected as running
 * Will include a library of supported roguelike games, with support for community contribution via [SaveScum profiles](https://github.com/SaveScum/SaveScumAgent-profiles)

Does this kill the fun of roguelike games? Is it ethical to cheat at singleplayer games like this? Are there other philisophical questions involved? Damned if I know. I just enjoy building stuff. I'll let more interested parties debate philosophy.

SaveScumAgent is written in C#, primiarly using [Visual Studio 2015 Preview](https://www.visualstudio.com/en-us/downloads/visual-studio-2015-downloads-vs.aspx).