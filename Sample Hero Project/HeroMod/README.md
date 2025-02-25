# Ulfvitr, the Wise Wolf

A hero mod, introducing Ulfvitr, a Shaman imprisoned by Gorio. 

This currently does not include any events or quests related to Ulfvitr. This will be updated in the future with a future mod release, it will include a sub-zone or three and a few quests. 

A couple of notes:
## Notes:
- I understand that things are going to be janky at times, and there are definitely bugs that will be worked out
- **What to do if Ulfvitr is not unlocked:** Due to some jankiness of the way the code works, Ulfvitr is unlocked only for the profile that is open when you launch the game (and for new profiles). So if they aren't unlocked in the correct profile, switch to that profile, close the game and re-open it and they will be unlocked. I'll fix this in the future, but most people won't notice it. You can also just use the profile editor to fix it.
- I know that the Hero selection sprite for Ulfvitr does not fit properly. I was too lazy to create custom sprites for this. I will fix it sometime soon.
- There are **no character events** for Ulfvitr at this time. This is intentional as I am going to be releasing another mod in the next few months that will include loads of events for them along with some additional sub-zones.
- Ulfvitr's selection location (in the Hero Selection screen) is intentionally far to the right. I have not yet automated the process of placing characters, and this is to accommodate Charls and a couple of heroes that I'm still working on. 
- I don't know how to add hinting to enchantments, so the tooltips in the Tome of Knowledge won't be fully updated. These will be updated one day.

This mod relies on [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).

<details>
<summary>Traits</summary>

### Level 1
Call the Rain: At the start of your turn, apply 1 wet to all characters


### Level 2

![Storm Channeler](/Storm_Channeler.png)

![Waters of Life](/Waters_of_Life.png)

### Level 3

Magnet: When you play a Lightning Spell that costs Energy, refund 1 Energy and apply 1 Spark to a random enemy. [3 times/turn]
Regenerator: Regeneration +1. When you apply Regeneration on a hero heal them by the amount of wet on them. This heal is not affected by modifiers

### Level 4

![Electric Current](./Electric_Current.png)

![Tears of the Spirits](../Tears_of_the_Spririts.png)

### Level 5

Conductor: Spark +3. Applying wet on enemies deals lighting damage equal to their spark x0.5. This damage is not affected by modifiers.
Life Bloom: Wet +1, Regeneration +2. At end of turn, heal all heroes by wet x0.70.

</details>


## Installation (manual)

1. Install [Obeliskial Essentials](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Essentials/) and [Obeliskial Content](https://across-the-obelisk.thunderstore.io/package/meds/Obeliskial_Content/).
2. Click _Manual Download_ at the top of the page.
3. In Steam, right-click Across the Obelisk and select _Manage_->_Browse local files_.
4. Extract the archive into the game folder. Your _Across the Obelisk_ folder should now contain a _BepInEx_ folder and a _doorstop\_libs_ folder.
5. Run the game. If everything runs correctly, you will see this mod in the list of registered mods on the main menu.
6. Press F5 to open/close the Config Manager and F1 to show/hide mod version information.
7. Note: I am not certain about these install instructions. In the worst case, just copy _TheWiseWolf.dll_ into the _BepInEx\plugins_ folder, and the _Ulfvitr_ folder (the one with the subfolders containing the json files) into _BepInEx\config\Obeliskial\_importing_

## Installation (automatic)

1. Download and install [Thunderstore Mod Manager](https://www.overwolf.com/app/Thunderstore-Thunderstore_Mod_Manager) or [r2modman](https://across-the-obelisk.thunderstore.io/package/ebkr/r2modman/).
2. Click **Install with Mod Manager** button on top of the page.
3. Run the game via the mod manager.

## Support

This has been updated for version 1.4.

Hope you enjoy it and if have any issues, ping me in Discord or make a post in the **modding #support-and-requests** channel of the [official Across the Obelisk Discord](https://discord.gg/across-the-obelisk-679706811108163701).

## Donation

Please do not donate to me. If you wish to support me, I would prefer it if you just gave me feedback. 