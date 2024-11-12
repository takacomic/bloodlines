
// Sprite splitters:
// https://www.leshylabs.com/apps/sstool/
// https://ezgif.com/maker/ezgif-2-fc6dbfd1cf-png-48x53-sprite-png/ezgif-2-fc6dbfd1cf.png
// https://www.gamedeveloperstudio.com/tools/spritesheet_slicer.php


const equipmentJson = {
  "equipment": [ "VOID", "MAGIC_MISSILE", "HOLY_MISSILE", "WHIP", "VAMPIRICA", "AXE", "SCYTHE", "KNIFE", "THOUSAND", "HOLYWATER", "BORA", "DIAMOND", "FIREBALL", "HELLFIRE", "HOLYBOOK", "VESPERS", "CROSS", "HEAVENSWORD", "GARLIC", "VORTEX", "LAUREL", "THORNS", "LIGHTNING", "LOOP", "PENTAGRAM", "ROSARY", "SIRE", "SILF", "SILF2", "SILF3", "BONE", "LANCET", "SONG", "MANNAGGIA", "CHERRY", "CART", "CART2", "GATTI", "STIGRANGATTI", "GATTI_SCRATCH", "GATTI_SCUFFLE", "FLOWER", "GUNS", "GUNS2", "GUNS3", "TRAPANO", "TRAPANO2", "SARABANDE", "FIREEXPLOSION", "NDUJA", "POWER", "AREA", "SPEED", "COOLDOWN", "DURATION", "AMOUNT", "MAXHEALTH", "ARMOR", "MOVESPEED", "MAGNET", "GROWTH", "LUCK", "GREED", "REVIVAL", "SHIELD", "REGEN", "CURSE", "SILVER", "GOLD", "ROCHER", "ROCHEREXP", "LEFT", "RIGHT", "CORRIDOR", "SHROUD", "COLDEXPLOSION", "WINDOW", "PANDORA", "VENTO", "VENTO2", "VENTO2_EXPLO", "VENTO2_EXTRA", "ROBBA", "WINDOW2", "SILF_COUNTER", "SILF2_COUNTER", "RAYEXPLOSION", "CANDYBOX", "GUNS_COUNTER", "GUNS2_COUNTER", "JUBILEE", "VICTORY", "SOLES", "GATTI_COUNTER", "NDUJA_COUNTER", "TRIASSO1", "TRIASSO2", "TRIASSO3", "BLOODLINE", "CANDYBOX2", "BUBBLES", "ASTRONOMIA", "BLOOD_GARLIC", "BLOOD_SONG", "BLOOD_PENTAGRAM", "BLOOD_LANCET", "BLOOD_LAUREL", "JUBILEE_RAYS", "MISSPELL", "MISSPELL2", "SILVERWIND", "SILVERWIND2", "FOURSEASONS", "FOURSEASONS2", "SUMMONNIGHT", "SUMMONNIGHT2", "MIRAGEROBE", "MIRAGEROBE2", "BUBBLES2", "NIGHTSWORD", "NIGHTSWORD2", "BOCCE", "BOCCE_COUNTER", "SPELL_STRING", "SPELL_STREAM", "SPELL_STRIKE", "SPELL_STROM", "BONE2", "BONE_ARM", "TETRAFORCE", "SHADOWSERVANT", "SHADOWSERVANT2", "PRISMATICMISS", "PRISMATICMISS2", "FLASHARROW", "FLASHARROW2", "SEC_MILLIONAIRE", "SWORD", "SWORD2", "PARTY", "SWORD_FINISHER", "LEGIONNAIRE", "PHASER", "SHADOWSERVANT_COUNTER", "PARTY_COUNTER", "", "INSATIABLE", "CHERRY2", "CHERRY_STAR_EXPLO", "CHERRY_STAR" ],
  "passives": ["POWER", "REGEN", "MAXHEALTH", "ARMOR", "AREA", "SPEED", "COOLDOWN", "DURATION", "AMOUNT", "MOVESPEED", "MAGNET", "LUCK", "GROWTH", "GREED", "CURSE", "REVIVAL", "REROLL", "SKIP", "BANISH", "CHARM"],
  "weapons": ["VOID", "MAGIC_MISSILE","HOLY_MISSILE","WHIP","VAMPIRICA","AXE","SCYTHE","KNIFE","THOUSAND","HOLYWATER","BORA","DIAMOND","FIREBALL","HELLFIRE","HOLYBOOK","VESPERS","CROSS","HEAVENSWORD","GARLIC","VORTEX","LAUREL","THORNS","LIGHTNING","LOOP","PENTAGRAM","ROSARY","SIRE","SILF","SILF2","SILF3","BONE","LANCET","SONG","MANNAGGIA","CHERRY","CART","CART2","GATTI","STIGRANGATTI","GATTI_SCRATCH","GATTI_SCUFFLE","FLOWER","GUNS","GUNS2","GUNS3","TRAPANO","TRAPANO2","SARABANDE","FIREEXPLOSION","NDUJA","SHIELD","SILVER","GOLD","ROCHER","ROCHEREXP","LEFT","RIGHT","CORRIDOR","SHROUD","COLDEXPLOSION","WINDOW","VENTO","VENTO2","VENTO2_EXPLO","VENTO2_EXTRA","ROBBA","WINDOW2","SILF_COUNTER","SILF2_COUNTER","RAYEXPLOSION","CANDYBOX","GUNS_COUNTER","GUNS2_COUNTER","JUBILEE","VICTORY","SOLES","GATTI_COUNTER","NDUJA_COUNTER","TRIASSO1","TRIASSO2","TRIASSO3","BLOODLINE","CANDYBOX2","BUBBLES","ASTRONOMIA","BLOOD_GARLIC","BLOOD_SONG","BLOOD_PENTAGRAM","BLOOD_LANCET","BLOOD_LAUREL","JUBILEE_RAYS","MISSPELL","MISSPELL2","SILVERWIND","SILVERWIND2","FOURSEASONS","FOURSEASONS2","SUMMONNIGHT","SUMMONNIGHT2","MIRAGEROBE","MIRAGEROBE2","BUBBLES2","NIGHTSWORD","NIGHTSWORD2","BOCCE","BOCCE_COUNTER","SPELL_STRING","SPELL_STREAM","SPELL_STRIKE","SPELL_STROM","BONE2","BONE_ARM","TETRAFORCE","SHADOWSERVANT","SHADOWSERVANT2","PRISMATICMISS","PRISMATICMISS2","FLASHARROW","FLASHARROW2","SEC_MILLIONAIRE","SWORD","SWORD2","PARTY","SWORD_FINISHER","LEGIONNAIRE","PHASER","SHADOWSERVANT_COUNTER","PARTY_COUNTER","ACADEMYBADGE","INSATIABLE","CHERRY2","CHERRY_STAR_EXPLO","CHERRY_STAR"],
  "baseWeapons": ["AXE", "CROSS", "HOLYBOOK", "FIREBALL", "GARLIC", "HOLYWATER", "DIAMOND", "LIGHTNING", "PENTAGRAM", "SILF", "SILF2", "GUNS", "GUNS2", "GATTI", "SONG", "TRAPANO", "LANCET", "LAUREL", "VENTO", "BONE", "CHERRY", "CART2", "FLOWER", "LAROBBA", "JUBILEE", "TRIASSO1", "VICTORY", "MISSPELL", "SILVERWIND", "FOURSEASONS", "SUMMONNIGHT", "MIRAGEROBE", "BUBBLES", "NIGHTSWORD", "BOCCE", "SPELL_STRING", "SPELL_STREAM", "SPELL_STRIKE", "SWORD", "FLASHARROW", "PRISMATICMISS", "SHADOWSERVANT", "PARTY"],
  "counterpartWeapons": ["SILF_COUNTER", "SILF2_COUNTER", "SILF3_COUNTER", "GUNS_COUNTER", "GUNS2_COUNTER", "GUNS3_COUNTER", "GATTI_COUNTER", "NDUJA_COUNTER", /*"BOCCE_COUNTER",*/ "SHADOWSERVANT_COUNTER", "PARTY_COUNTER"],
  "IdToNameArray": [ { "id": "MAGIC_MISSILE", "name": "Magic Wand" }, { "id": "HOLY_MISSILE", "name": "Holy Wand" }, { "id": "WHIP", "name": "Whip" }, { "id": "VAMPIRICA", "name": "Bloody Tear" }, { "id": "AXE", "name": "Axe" }, { "id": "SCYTHE", "name": "Death Spiral" }, { "id": "KNIFE", "name": "Knife" }, { "id": "THOUSAND", "name": "Thousand Edge" }, { "id": "HOLYWATER", "name": "Santa Water" }, { "id": "BORA", "name": "La Borra" }, { "id": "DIAMOND", "name": "Runetracer" }, { "id": "FIREBALL", "name": "Fire Wand" }, { "id": "HELLFIRE", "name": "Hellfire" }, { "id": "HOLYBOOK", "name": "King Bible" }, { "id": "VESPERS", "name": "Unholy Vespers" }, { "id": "CROSS", "name": "Cross" }, { "id": "HEAVENSWORD", "name": "Heaven Sword" }, { "id": "GARLIC", "name": "Garlic" }, { "id": "VORTEX", "name": "Soul Eater" }, { "id": "LAUREL", "name": "Laurel" }, { "id": "LIGHTNING", "name": "Lightning Ring" }, { "id": "LOOP", "name": "Thunder Loop" }, { "id": "PENTAGRAM", "name": "Pentagram" }, { "id": "SIRE", "name": "Gorgeous Moon" }, { "id": "SILF", "name": "Peachone" }, { "id": "SILF2", "name": "Ebony Wings" }, { "id": "SILF3", "name": "Vandalier" }, { "id": "BONE", "name": "Bone" }, { "id": "LANCET", "name": "Clock Lancet" }, { "id": "SONG", "name": "Song of Mana" }, { "id": "MANNAGGIA", "name": "Mannajja" }, { "id": "CHERRY", "name": "Cherry Bomb" }, { "id": "CART", "name": "Dairy Cart" }, { "id": "CART2", "name": "Carréllo" }, { "id": "GATTI", "name": "Gatti Amari" }, { "id": "STIGRANGATTI", "name": "Vicious Hunger" }, { "id": "FLOWER", "name": "Celestial Dusting" }, { "id": "GUNS", "name": "Phiera Der Tuphello" }, { "id": "GUNS2", "name": "Eight The Sparrow" }, { "id": "GUNS3", "name": "Phieraggi" }, { "id": "TRAPANO", "name": "Shadow Pinion" }, { "id": "TRAPANO2", "name": "Valkyrie Turner" }, { "id": "SARABANDE", "name": "Sarabande of Healing" }, { "id": "FIREEXPLOSION", "name": "Heart of Fire" }, { "id": "NDUJA", "name": "Nduja Fritta" }, { "id": "POWER", "name": "Spinach" }, { "id": "AREA", "name": "Candelabrador" }, { "id": "SPEED", "name": "Bracer" }, { "id": "COOLDOWN", "name": "Empty Tome" }, { "id": "DURATION", "name": "Spellbinder" }, { "id": "AMOUNT", "name": "Duplicator" }, { "id": "MAXHEALTH", "name": "Hollow Heart" }, { "id": "ARMOR", "name": "Armor" }, { "id": "MOVESPEED", "name": "Wings" }, { "id": "MAGNET", "name": "Attractorb" }, { "id": "GROWTH", "name": "Crown" }, { "id": "LUCK", "name": "Clover" }, { "id": "GREED", "name": "Stone Mask" }, { "id": "REVIVAL", "name": "Tirajisú" }, { "id": "REGEN", "name": "Pummarola" }, { "id": "CURSE", "name": "Skull O'Maniac" }, { "id": "SILVER", "name": "Silver Ring" }, { "id": "GOLD", "name": "Gold Ring" }, { "id": "ROCHER", "name": "NO FUTURE" }, { "id": "LEFT", "name": "Metaglio Left" }, { "id": "RIGHT", "name": "Metaglio Right" }, { "id": "CORRIDOR", "name": "Infinite Corridor" }, { "id": "SHROUD", "name": "Crimson Shroud" }, { "id": "COLDEXPLOSION", "name": "Out of Bounds" }, { "id": "WINDOW", "name": "Stained Glass" }, { "id": "PANDORA", "name": "Torrona's Box" }, { "id": "VENTO", "name": "Vento Sacro" }, { "id": "VENTO2", "name": "Fuwalafuwaloo" }, { "id": "ROBBA", "name": "La Robba" }, { "id": "WINDOW2", "name": "Game Killer" }, { "id": "SILF_COUNTER", "name": "Cygnus" }, { "id": "SILF2_COUNTER", "name": "Zhar Ptytsia" }, { "id": "CANDYBOX", "name": "Candybox" }, { "id": "GUNS_COUNTER", "name": "Red Muscle" }, { "id": "GUNS2_COUNTER", "name": "Twice Upon a Time" }, { "id": "JUBILEE", "name": "Greatest Jubilee" }, { "id": "VICTORY", "name": "Victory Sword" }, { "id": "SOLES", "name": "Sole Solution" }, { "id": "GATTI_COUNTER", "name": "Flock Destroyer" }, { "id": "NDUJA_COUNTER", "name": "Nduja Fritta" }, { "id": "TRIASSO1", "name": "Bracelet" }, { "id": "TRIASSO2", "name": "Bi-Bracelet" }, { "id": "TRIASSO3", "name": "Tri-Bracelet" }, { "id": "BLOODLINE", "name": "Divine Bloodline" }, { "id": "CANDYBOX2", "name": "Super Candybox II Turbo" }, { "id": "BUBBLES", "name": "Mille Bolle Blu" }, { "id": "ASTRONOMIA", "name": "Blood Astronomia" }, { "id": "MISSPELL", "name": "Flames of Misspell" }, { "id": "MISSPELL2", "name": "Ashes of Muspell" }, { "id": "SILVERWIND", "name": "Silver Wind" }, { "id": "SILVERWIND2", "name": "Festive Winds" }, { "id": "FOURSEASONS", "name": "Four Seasons" }, { "id": "FOURSEASONS2", "name": "Godai Shuffle" }, { "id": "SUMMONNIGHT", "name": "Summon Night" }, { "id": "SUMMONNIGHT2", "name": "Echo Night" }, { "id": "MIRAGEROBE", "name": "Mirage Robe" }, { "id": "MIRAGEROBE2", "name": "J'Odore" }, { "id": "BUBBLES2", "name": "Boo Roo Boolle" }, { "id": "NIGHTSWORD", "name": "Night Sword" }, { "id": "NIGHTSWORD2", "name": "Muramasa" }, { "id": "BOCCE", "name": "108 Bocce" }, { "id": "SPELL_STRING", "name": "SpellString" }, { "id": "SPELL_STREAM", "name": "SpellStream" }, { "id": "SPELL_STRIKE", "name": "SpellStrike" }, { "id": "SPELL_STROM", "name": "SpellStrom" }, { "id": "BONE2", "name": "Anima of Mortaccio" }, { "id": "TETRAFORCE", "name": "Tetraforce" }, { "id": "SHADOWSERVANT", "name": "Shadow Servant" }, { "id": "SHADOWSERVANT2", "name": "Ophion" }, { "id": "PRISMATICMISS", "name": "Prismatic Missile" }, { "id": "PRISMATICMISS2", "name": "Luminaire" }, { "id": "FLASHARROW", "name": "Flash Arrow" }, { "id": "FLASHARROW2", "name": "Millionaire" }, { "id": "SWORD", "name": "Eskizzibur" }, { "id": "SWORD2", "name": "Legionnaire" }, { "id": "PARTY", "name": "Party Popper" }, { "id": "PHASER", "name": "Phas3r" }, { "id": "SHADOWSERVANT_COUNTER", "name": "Silver Sliver" }, { "id": "PARTY_COUNTER", "name": "Party Pooper" }, { "id": "ACADEMYBADGE", "name": "Academy Badge" }, { "id": "INSATIABLE", "name": "Insatiable" }, { "id": "CHERRY2", "name": "Yatta Daikarin" }, { "id": "ICELANCE", "name": "Glass Fandango" }, { "id": "ICELANCE2", "name": "Celestial Voulge" }, { "id": "CART2EVO", "name": "Acchee Pee Kia" }, { "id": "FLOWER2", "name": "Profusione D'Amore" }, { "id": "LAROBBA2", "name": "Gravijoe" }, { "id": "ARMADIO", "name": "Arma Dio" }, { "id": "PHASER2", "name": "Photonstorm" }, { "id": "BATTILIA", "name": "Pako Battiliar" }, { "id": "BATTILIA2", "name": "Mazo Familiar" }, { "id": "C1_REPORT1", "name": "Report!" }, { "id": "C1_REPORT2", "name": "Emergency Meeting" }, { "id": "C1_SWIPECARD1", "name": "Lucky Swipe" }, { "id": "C1_SWIPECARD2", "name": "Crossed Wires" }, { "id": "C1_MEDICAL1", "name": "Lifesign Scan" }, { "id": "C1_MEDICAL2", "name": "Paranormal Scan" }, { "id": "C1_VENT1", "name": "Just Vent" }, { "id": "C1_VENT2", "name": "Unjust Ejection" }, { "id": "C1_GARBA1", "name": "Clear Debris" }, { "id": "C1_GARBA2", "name": "Clear Asteroids" }, { "id": "C1_TONGUE1", "name": "Sharp Tongue" }, { "id": "C1_TONGUE2", "name": "Impostongue" }, { "id": "C1_SAMPLES1", "name": "Science Rocks" }, { "id": "C1_SAMPLES2", "name": "Rocket Science" }, { "id": "C1_HATCOLLECTION1", "name": "Hats" }, { "id": "C1_SHRINK_CREWMA", "name": "Mini Crewmate" }, { "id": "C1_SHRINK_ENGINE", "name": "Mini Engineer" }, { "id": "C1_SHRINK_GHOSTT", "name": "Mini Ghost" }, { "id": "C1_SHRINK_SHAPES", "name": "Mini Shapeshifter" }, { "id": "C1_SHRINK_GUARDI", "name": "Mini Guardian" }, { "id": "C1_SHRINK_IMPOST", "name": "Mini Impostor" }, { "id": "C1_SHRINK_SCIENT", "name": "Mini Scientist" }, { "id": "C1_SHRINK_HORSEE", "name": "Mini Horse" }, { "id": "C1_TONGUE1_COUNTER", "name": "Silver Tongue" }, { "id": "PARMA", "name": "Parm Aegis" }, { "id": "DOMINION", "name": "Karoma's Mana" }, { "id": "NOVA_FIRE", "name": "Fire Nova" }, { "id": "NOVA_ICEE", "name": "Ice Nova" }, { "id": "NOVA_FEAR", "name": "Fear Nova" }, { "id": "FLIK", "name": "Flik the Blue" }, { "id": "FLIK2", "name": "Blue Impulse" }, { "id": "TRAINHAZARD", "name": "Take Us Away" }, { "id": "SANTAJAVELIN", "name": "Santa Javelin" }, { "id": "SANTAJAVELIN2", "name": "Seraphic Cry" }, { "id": "SANTAJAVELINCOUNTER", "name": "Levelin'Eh" }, { "id": "CONEOFCOLD", "name": "Sorbetto" }, { "id": "FLOOD", "name": "Acquazzone" }, { "id": "FB_FULLAUTO", "name": "Long Gun " }, { "id": "FB_RAPIDFIRE", "name": "Short Gun " }, { "id": "FB_SPREAD", "name": "Spread Shot " }, { "id": "FB_SONIC", "name": "Sonic Bloom" }, { "id": "FB_BLADECROSSBOW", "name": "Blade Crossbow" }, { "id": "FB_WAVE", "name": "Wave Beam" }, { "id": "FB_LASER", "name": "C-U-Laser" }, { "id": "FB_FIREARM", "name": "Firearm" }, { "id": "FB_METALCLAW", "name": "Metal Claw" }, { "id": "FB_CRUSH", "name": "Atmo-Torpedo" }, { "id": "FB_HOMING", "name": "Homing Miss" }, { "id": "FB_PRISMCUTLASS", "name": "Prism Lass" }, { "id": "FB_PROTONBEAM", "name": "Pronto Beam" }, { "id": "FB_FIREWALL", "name": "Fire-L3GS" }, { "id": "FB_BIGFUZZYFIST", "name": "Big Fuzzy Fist" }, { "id": "FB_MULTISTAGE", "name": "Multistage Missiles" }, { "id": "FB_TIMEWARP", "name": "Time Warp" }, { "id": "FB_WEAPONPU", "name": "Weapon Power-Up" }, { "id": "FB_PROTOTYPE_B", "name": "Prototype B" }, { "id": "FB_PROTOTYPE_A", "name": "Prototype A" }, { "id": "FB_PROTOTYPE_C", "name": "Prototype C" }, { "id": "FB_CROSSBOWCRASH", "name": "BFC2000-AD" }, { "id": "FB_DIVERMINES", "name": "Diver Mines" }, { "id": "FB_PRISMCUTLASS_COUNTER", "name": "Prism Damsel" }, { "id": "D20_JETBLACK", "name": "Wandering the Jet Black" }, { "id": "TP_CONFODERE1", "name": "Confodere" }, { "id": "TP_CONFODERE2", "name": "Vol Confodere" }, { "id": "TP_CONFODERE3", "name": "Melio Confodere" }, { "id": "TP_ALCHEMYWHIP1", "name": "Alchemy Whip" }, { "id": "TP_ALCHEMYWHIP2", "name": "Vampire Killer" }, { "id": "TP_COATOFARMS", "name": "Coat of Arms" }, { "id": "TP_MORNINGSTAR", "name": "Morning Star" }, { "id": "TP_SPELLBOOK", "name": "Belnades' Spellbook" }, { "id": "TP_DIABOLOGUE", "name": "Ebony Diabologue" }, { "id": "TP_ALUCARDSWORD1", "name": "Alucart Sworb" }, { "id": "TP_ALUCARDSWORD2", "name": "Alucard Swords" }, { "id": "TP_IRONBALL1", "name": "Iron Ball" }, { "id": "TP_IRONBALL2", "name": "Wrecking Ball" }, { "id": "TP_ALUCARDSPEAR1", "name": "Alucard Spear" }, { "id": "TP_ALUCARDSPEAR2", "name": "Thunderbolt Spear" }, { "id": "TP_MACE1", "name": "Mace" }, { "id": "TP_MACE2", "name": "Stamazza" }, { "id": "TP_CHAUVE1", "name": "Trident" }, { "id": "TP_CHAUVE2", "name": "Gungnir-Souris" }, { "id": "TP_JAVELIN1", "name": "Javelin" }, { "id": "TP_JAVELIN2", "name": "Long Inus" }, { "id": "TP_BWAKA1", "name": "Curved Knife" }, { "id": "TP_BWAKA2", "name": "Bwaka Knife" }, { "id": "TP_SHURIKEN1", "name": "Shuriken" }, { "id": "TP_SHURIKEN2", "name": "Yagyu Shuriken" }, { "id": "TP_LIGHT1", "name": "Luminatio" }, { "id": "TP_LIGHT2", "name": "Vol Luminatio" }, { "id": "TP_DARK1", "name": "Umbra" }, { "id": "TP_DARK2", "name": "Vol Umbra" }, { "id": "TP_DISCUS1", "name": "Discus" }, { "id": "TP_DISCUS2", "name": "Stellar Blade" }, { "id": "TP_WINEGLASS1", "name": "Wine Glass" }, { "id": "TP_WINEGLASS2", "name": "Meal Ticket" }, { "id": "TP_MARTIALWHIP1", "name": "Vanitas Whip" }, { "id": "TP_MARTIALWHIP2", "name": "Aurablaster Tip" }, { "id": "TP_CUSTOS1", "name": "Dextro Custos" }, { "id": "TP_CUSTOS2", "name": "Sinestro Custos" }, { "id": "TP_CUSTOS3", "name": "Centralis Custos" }, { "id": "TP_CUSTOS4", "name": "Trinum Custodem" }, { "id": "TP_WINDWHIP1", "name": "Wind Whip" }, { "id": "TP_WINDWHIP2", "name": "Spirit Tornado Tip" }, { "id": "TP_PLATINUMWHIP1", "name": "Platinum Whip" }, { "id": "TP_PLATINUMWHIP2", "name": "Cross Crasher Tip" }, { "id": "TP_DRAGONWATER1", "name": "Dragon Water Whip" }, { "id": "TP_DRAGONWATER2", "name": "Hydrostormer Tip" }, { "id": "TP_SOULSTEAL_WEAPON", "name": "Soul Steal" }, { "id": "TP_RPG1", "name": "Hand Grenade" }, { "id": "TP_RPG2", "name": "The RPG" }, { "id": "TP_ALUCARDSHIELD", "name": "Alucard Shield" }, { "id": "TP_RAPIDUS1", "name": "Sonic Dash" }, { "id": "TP_RAPIDUS2", "name": "Rapidus Fio" }, { "id": "TP_FIRE1", "name": "Raging Fire" }, { "id": "TP_FIRE2", "name": "Salamender" }, { "id": "TP_ICE1", "name": "Ice Fang" }, { "id": "TP_ICE2", "name": "Cocytus" }, { "id": "TP_WIND1", "name": "Gale Force" }, { "id": "TP_WIND2", "name": "Pneuma Tempestas" }, { "id": "TP_EARTH1", "name": "Rock Riot" }, { "id": "TP_EARTH2", "name": "Gemma Torpor" }, { "id": "TP_ELEC1", "name": "Fulgur" }, { "id": "TP_ELEC2", "name": "Tenebris Tonitrus" }, { "id": "TP_ACID1", "name": "Keremet Bubbles" }, { "id": "TP_ACID2", "name": "Keremet Morbus" }, { "id": "TP_EVIL1", "name": "Hex" }, { "id": "TP_EVIL2", "name": "Nightmare" }, { "id": "TP_HOLY1", "name": "Refectio" }, { "id": "TP_HOLY2", "name": "Sanctuary" }, { "id": "TP_SPITE1", "name": "Optical Shot" }, { "id": "TP_SPITE2", "name": "Acerbatus" }, { "id": "TP_ENERGY1", "name": "Globus" }, { "id": "TP_ENERGY2", "name": "Nitesco" }, { "id": "TP_FIRE1_COUNTER", "name": "Speculo Raging Fire" }, { "id": "TP_ICE1_COUNTER", "name": "Speculo Ice Fang" }, { "id": "TP_EARTH1_COUNTER", "name": "Speculo Rock Riot" }, { "id": "TP_WIND1_COUNTER", "name": "Speculo Gale Force" }, { "id": "TP_ELEC1_COUNTER", "name": "Speculo Fulgur" }, { "id": "TP_ACID1_COUNTER", "name": "Speculo Keremet Bubbles" }, { "id": "TP_EVIL1_COUNTER", "name": "Speculo Hex" }, { "id": "TP_HOLY1_COUNTER", "name": "Speculo Refectio" }, { "id": "TP_ENERGY1_COUNTER", "name": "Speculo Globus" }, { "id": "TP_SONICWHIP2", "name": "Crissaegrim Tip" }, { "id": "TP_HOLYWHIP1", "name": "Vibhuti Whip" }, { "id": "TP_HOLYWHIP2", "name": "Daybreaker Tip" }, { "id": "TP_GUN1", "name": "Silver Revolver" }, { "id": "TP_GUN2", "name": "Jewel Gun" }, { "id": "TP_SLASH1", "name": "Tyrfing" }, { "id": "TP_SLASH2", "name": "Rune Sword" }, { "id": "TP_UNIVERSITAS", "name": "Universitas" }, { "id": "TP_DOMINUS1", "name": "Dominus Anger" }, { "id": "TP_DOMINUS2", "name": "Dominus Hatred" }, { "id": "TP_DOMINUS3", "name": "Dominus Agony" }, { "id": "TP_DOMINUS4", "name": "Power of Sire" }, { "id": "TP_SACREDBEASTS1", "name": "Guardian's Targe" }, { "id": "TP_SACREDBEASTS2", "name": "Sacred Beasts Tower Shield" }, { "id": "TP_STARFLAIL1", "name": "Star Flail" }, { "id": "TP_STARFLAIL2", "name": "Moon Rod" }, { "id": "TP_LEMURIA1", "name": "Jet Black Whip" }, { "id": "TP_LEMURIA2", "name": "Mormegil Tip" }, { "id": "TP_SPECTRALSWORD", "name": "Spectral Sword" }, { "id": "TP_SHIELD1", "name": "Iron Shield" }, { "id": "TP_SHIELD2", "name": "Dark Iron Shield" }, { "id": "TP_NEUTRON_PICKUP", "name": "Neutron Bomb" }, { "id": "TP_NEUTRON_WEAPON", "name": "Troll Bomb" }, { "id": "TP_SOULSTEAL_PICKUP", "name": "Soul Steal" }, { "id": "TP_GEARS_WEAPON", "name": "Endo Gears" }, { "id": "TP_PENDULUM_WEAPON", "name": "Peri Pendulum" }, { "id": "TP_SAVROG_WEAPON", "name": "Svarog Statue" }, { "id": "TP_SONICWHIP1", "name": "Sonic Whip" }, { "id": "TP_GOTH_MISSILE", "name": "Arrow of Goth" }, { "id": "TP_GOTH_MISSILE_HOLYWHIP2", "name": "Arrow of Goth" }, { "id": "TP_HYDROSTORM", "name": "Hydro Storm" }, { "id": "TP_HYDROSTORM_WATERDRAGONWHIP", "name": "Hydro Storm" }, { "id": "TP_DARKRIFT", "name": "Dark Rift" }, { "id": "TP_DARKRIFT_JETBLACKWHIP", "name": "Dark Rift" }, { "id": "TP_SUMMON_SPIRIT", "name": "Summon Spirit" }, { "id": "TP_SWORD_BROTHERS", "name": "Sword Brothers" }, { "id": "TP_VALMANWAY", "name": "Valmanway" }, { "id": "TP_VALMANWAY_SONICWHIP", "name": "Valmanway" }, { "id": "TP_GRANDCROSS", "name": "Grand Cross" }, { "id": "TP_GRANDCROSS_PLATINUMWHIP", "name": "Grand Cross" }, { "id": "TP_SPIRITTORNADO", "name": "Summon Spirit Tornado" }, { "id": "TP_SPIRITTORNADO_WINDWHIP", "name": "Spirit Tornado" }, { "id": "TP_ELEVATOR_WEAPON", "name": "Myo Lift" }, { "id": "TP_HEADS_WEAPON", "name": "Epi Head" }, { "id": "TP_CLOCKTOWER_WEAPON", "name": "Clock Tower" }, { "id": "TP_AURABLAST_WEAPON", "name": "Aura Blast" }, { "id": "TP_BLUEFIRE_WEAPON", "name": "Burning Alcarde" }, { "id": "TP_AURABLAST_MARTIALWHIP", "name": "Aura Blast" }, { "id": "TP_ACC_FAMILIAR_UKOBACK", "name": "Ukoback" }, { "id": "TP_ACC_FAMILIAR_BITTERFLY", "name": "Bitterfly" }, { "id": "TP_ACC_FAMILIAR_IMP", "name": "Imp" }, { "id": "TP_ACC_FAMILIAR_ALLEGEDGHOST", "name": "Alleged Ghost" }, { "id": "TP_ACC_FAMILIAR_WIZARD", "name": "Wood Rod" }, { "id": "TP_ACC_FAMILIAR_FAIRY", "name": "Faerie" }, { "id": "TP_ACC_FAMILIAR_PUMPKIN", "name": "Pumpkin" }, { "id": "TP_FAMILIARFORGE", "name": "Familiar Forge" }, { "id": "TP_ACC_FAMILIAR_CARDINAL", "name": "Sacred Cardinal" }, { "id": "TP_ACC_FAMILIAR_DRAGON", "name": "Sacred Dragon" }, { "id": "TP_ACC_FAMILIAR_TIGER", "name": "Sacred Tiger" }, { "id": "TP_ACC_FAMILIAR_TURTLE", "name": "Sacred Turtle" }, { "id": "TP_ICEBRAND", "name": "Icebrand" }, { "id": "TP_DEATHHAND", "name": "Death Hand" }, { "id": "TP_POWEROFLIRE", "name": "Power Of Lire" } ]
};

const characterJson = {
  "version": "0.2",
  "characters": [
    {
      "level": 1,
      "startingWeapon": "",
      "prefix": "",
      "charName": "",
      "surname": "",
      "textureName": "characters",
      "spriteName": "",
      "portraitName": "",
      "skins": [],
      "currentSkinIndex": 0,
      "walkingFrames": 1,
      "description": "",
      "isBought": true,
      "price": 0,
      "completedStages": [],
      "survivedMinutes": 0,
      "enemiesKilled": 0,
      "stageData": [],
      "showcase": [],
      "hiddenWeapons": [],
      "bodyOffset": {
        x: 0,
        y: 0
      },
      "statModifiers": []
    },
  ]
};

const statModifierJson = {
  "level": 0,
  "maxHp": 0,
  "armor": 0,
  "regen": 0,
  "moveSpeed": 0,
  "power": 0,
  "cooldown": 0,
  "area": 0,
  "speed": 0,
  "duration": 0,
  "amount": 0,
  "luck": 0,
  "growth": 0,
  "greed": 0,
  "curse": 0,
  "magnet": 0,
  "revivals": 0,
  "rerolls": 0,
  "skips": 0,
  "banish": 0,
  "shields": 0,
  "charm": 0
};

const skinJson = {
  "id": 0,
  "name": "Default",
  "textureName": "characters",
  "spriteName": "",
  "walkingFrames": 1,
  "unlocked": true,
  "frames": [],
  "alwaysAnimate": false
};

const weaponIdToNameMap = new Map(equipmentJson.IdToNameArray.map(({id, name}) => [id, name]));

const spritePortraitString = "<i class=\"fas fa-file-image\"></i> Sprite Portrait";
const spriteFrameString = "<i class=\"fas fa-file-image\"></i> Frames";

let numOfSkins = 0;
let numOfModifiers = 0;

const clone = (obj) => { return JSON.parse(JSON.stringify(obj)); }

const createJsonOutput = (form) => {
  // console.debug(`Running createJsonOutput on form: ${form.id}`);
  if (!form) {
    return;
  }

  const currentJson = document.getElementById("outputJsonTextArea");
  let json = {};

  if (currentJson == null || currentJson.textContent.length == 0) {
    json = clone(characterJson);
  } else {
    json = JSON.parse(currentJson.textContent);
  }

  const gotField = [];

  const char = json.characters[0];
  for (let field of form.elements) {
    if (!field.id || field.disabled || field.type === "reset" || field.type === "submit" || field.type === "button") {
      continue;
    }

    let fieldId = field.id;

    if (field.id.includes(".")) {
      const split = field.id.split(".");
      if (split.length > 2 || !Object.hasOwn(char, split[0]) || !Object.hasOwn(char[split[0]], split[1])) {
        console.error("Invalid input id: " + field.id);
      }
      char[split[0]][split[1]] = getFieldValue(field);
      continue;
    } else if (field.id.startsWith("mod-")) {
      // Modifier objects
      const modField = field.id.substring(field.id.indexOf("-") + 1, field.id.lastIndexOf("-"));
      const num = parseInt(field.id.substring(field.id.lastIndexOf("-") + 1, field.id.length));

      while (char.statModifiers[num] == null) {
        char.statModifiers.push({});
      }

      const modObject = char.statModifiers[num];
      const fieldValue = getFieldValue(field);

      if (fieldValue > 0) {
        modObject[modField] = fieldValue;
      }

      continue;
    } else if (field.id.startsWith("skin-")) {
      let skinFieldId = field.id;
      if (skinFieldId.endsWith("-input")) {
        skinFieldId = skinFieldId.substring(0, skinFieldId.indexOf("-input"));
      }
      
      skinField = skinFieldId.substring(skinFieldId.indexOf("-") + 1, skinFieldId.lastIndexOf("-"));
      const num = parseInt(skinFieldId.substring(skinFieldId.lastIndexOf("-") + 1, skinFieldId.length));

      if (isNaN(num)) {
        console.debug(`Invalid field: ${skinFieldId} ${num} ${skinField}`);
        return;
      }

      while (char.skins[num] == null) {
        char.skins.push({});
      }

      const skinObject = char.skins[num];

      if (skinField === "frames") {
        if (!gotField.includes(skinFieldId)) {
          skinObject[skinField] = getFieldValue({ type: "file", files: currentFiles.get(skinFieldId) || [] })
          gotField.push(skinFieldId);
        }
        continue;
      } else if (skinField === "spriteName") {
        if (field && field.files && field.files.length > 0) {
          skinObject[skinField] = field.files[0].name;
        }
        continue;
      }

      const fieldValue = getFieldValue(field);

      skinObject[skinField] = fieldValue;
      continue;
    }

    if (!Object.hasOwn(char, fieldId)) {
      console.error("Json object doesn't have property: " + field.id);
      continue;
    }

    char[fieldId] = getFieldValue(field);
  }

  // Need: portrait, onEveryLevelup
  // Hide: bodyOffset


  if (char?.skins?.length > 0) {
    for (let i = 0; i < char.skins.length; i++) {
      char.skins[i].id = i;
      char.skins[i].walkingFrames = char.skins[i].frames?.length ?? 0;
      char.walkingFrames = char.skins[i].walkingFrames;
    }
    char.walkingFrames = char.skins[0].walkingFrames;

    if (char.skins[0].spriteName) {
      char.spriteName = char.skins[0].spriteName;
      char.portraitName = char.skins[0].spriteName;
    }
  }
  // readonly variables to set based on variables above or just default stuff.
  

  setJsonOutputValue(json);
}

const getFieldValue = (field) => {
  switch (field.type) {
    case "text":
    case "select-one":
      return field.value;
    case "number":
      return parseFloat(field.value) || 0;
    case "checkbox":
      return field.checked;
    case "file":
      const retArray = [];
      if (field.files) {
        for (const file of field.files) {
          retArray.push(file.name);
        }
      }
      return retArray;
      // return field.value.split(/(\\|\/)/g).pop();
    case "select-multiple":
      if (showCaseChoices._baseId.endsWith(field.id)) {
        return showCaseChoices.getValue(true);
      }
    default:
      console.error("Unknown input type: " + field.type);
  }

  return null;
}

const setupStartingWeapon = () => {
  var startingWeapon = document.getElementById("startingWeapon");

  const sortedWeapons = equipmentJson.weapons.sort();

  const defaultOption = document.createElement("option");
  defaultOption.text = "Choose a Starting Weapon";
  defaultOption.value = "";
  defaultOption.hidden = true;
  defaultOption.selected = true;
  defaultOption.disabled = true;
  startingWeapon.add(defaultOption);

  for (const weapon of sortedWeapons) {
    const option = document.createElement("option");
    option.text = weaponIdToNameMap.get(weapon);
    if (option.text === "undefined") {
      continue;
    }
    option.value = weapon;
    startingWeapon.add(option);
  }
};

const setJsonOutputValue = (json) => {
  const output = document.getElementById("outputJsonTextArea");
  // output.add
  output.textContent = JSON.stringify(json, null, 2);
  output.style.height = 0;
  output.style.height = (output.scrollHeight) + "px";
};

document.addEventListener("DOMContentLoaded", () => {
  setJsonOutputValue(clone(characterJson));
});

const createColumnDiv = () => {
  const div = document.createElement("div");
  div.setAttribute("class", "col-6 col-12-small");
  return div;
};

const createRowDiv = (classes) => {
  const div = document.createElement("div");
  div.setAttribute("class", "row");
  if (classes) {
    div.classList.add(...classes);
  }
  return div;
};

const createInput = (type, name, id, placeholder) => {
  const input = document.createElement("input");
  input.setAttribute("type", type);
  input.setAttribute("name", name);
  input.setAttribute("id", id);
  if (type !== "checkbox") {
    input.setAttribute("placeholder", placeholder);
  }
  return input;
}

const createLabel = (innerHTML) => {
  const label = document.createElement("label");
  label.innerHTML = innerHTML;
  return label;
}

const resetFileInput = (element) => {
  if (!element) {
    console.error("resetFileInput - Invalid element");
    return;
  }

  const resetAndSetString = (element, newStr) => {
    element.value = '';
    const label = document.getElementById(element.id + "-label");
    if (label) {
      label.innerHTML = newStr;
      label.classList.remove("file-label--active");
    }
  }

  if (element.id.startsWith("skin-spriteName")) {
    resetAndSetString(element, spritePortraitString);
  }
}

const setOnImageChange = (element) => {
  element.addEventListener("change", (ev) => {
    const elem = ev.target;
    const [file] = elem.files;
    if (file) {
      const label = document.getElementById(elem.id + "-label");
      if (label) {
        label.innerHTML = "<img id=\"" + elem.id + "-img" + "\" src=\"" + URL.createObjectURL(file) + "\" alt=\"" + file.name + "\" /><input type=\"button\" class=\"image-remove-button\"value=\"remove\" onclick=\"resetFileInput(document.getElementById('" + elem.id + "'));\" />";
        label.classList.add("file-label--active");
      }
    }
  });
};

const createSkinElement = ({ labelName, inputId, inputType, required = false, readOnly = false, hidden = false, value = undefined, classList = [] }) => {
  let label = null;
  if (inputType === "file" || inputType === "checkbox") {
    label = createLabel(labelName);
    label.setAttribute("for", inputId);
    label.setAttribute("class", inputType + "-label");
    label.id = inputId + "-label";
  }

  const input = createInput(inputType, inputId, inputId, labelName);
  input.required = required;
  input.readOnly = readOnly;
  if (inputType == "file") {
    input.accept="image/png";
    setOnImageChange(input);
  }

  if (value !== undefined) {
    input.value = value;
  }
  if (hidden) {
    input.className += " hide";
  }
  const div = createDiv();
  if (label != null && inputType !== "checkbox") {
    div.appendChild(label);
  }
  div.appendChild(input);
  if (label != null && inputType === "checkbox") {
    div.appendChild(label);
  }
  
  div.classList.add(...classList || []);

  return div;
}

const createDiv = ({id, classList } = {}) => {
  const div = document.createElement("div");
  if (id) {
    div.id = id;
  }
  if (classList) {
    div.classList.add(...classList);
  }
  return div;
};

const createFramesDiv = (num) => {
  const row = createDiv({ classList: ["box", "align-center"]});

  const text = document.createElement("p");
  text.innerHTML = "Drag & drop individual walking frames here, they will be sorting alphabetically.";

  const inputContainer = document.createElement("div");
  inputContainer.id = `skin-frames-${num}`;
  inputContainer.classList.add(`skin-frames-${num}`);

  const input = document.createElement("input");
  input.type = "file";
  input.id = `skin-frames-${num}-input`;
  input.multiple = true;
  input.accept = "image/png";
  input.addEventListener("change", (ev) => {
    filesManager(ev.target.parentNode.id, ev.target.files);
    return;
  });

  const inputLabel = document.createElement("label");
  inputLabel.classList.add("button");
  inputLabel.setAttribute("for", input.id);
  inputLabel.innerHTML = "Or click to upload from your computer.";

  inputContainer.appendChild(input);
  inputContainer.appendChild(inputLabel);

  const removeContainer = createDiv({id: `skin-frames-${num}-rm-container`, classList: ["row", "aln-center"] });
  const gallery = createDiv({id: `skin-frames-${num}-gallery`, classList: ["gallery", "row", "aln-center"] });

  handleFrameDropbox(inputContainer);

  row.appendChild(text);
  row.appendChild(inputContainer);
  row.appendChild(removeContainer);
  row.appendChild(gallery);

  return row;
}

document.getElementById("addSkinForm").addEventListener("click", () => {
  const div = document.createElement("div");
  div.setAttribute("class", "box");
  div.setAttribute("id", "skin-form-" + numOfSkins);

  // div.appendChild(createLabel("Skin " + (numOfSkins + 1)));
  const header = document.createElement("h4");
  header.innerHTML = "Skin " + (numOfSkins + 1);
  div.appendChild(header);
  // div.appendChild(createSkinElement("Id", "skin-id-" + numOfSkins, "number", false, true, true));
  div.appendChild(
    createSkinElement({
      labelName: "Skin name",
      inputId: "skin-name-" + numOfSkins,
      inputType: "text"}
    )
  );
  div.appendChild(
    createSkinElement({
      labelName: spritePortraitString,
      inputId: "skin-spriteName-" + numOfSkins,
      inputType: "file"}
    )
  );

  const checkboxDiv = createDiv({ classList: [ "row" ]});

  checkboxDiv.appendChild(
    createSkinElement({
      labelName: "Unlocked",
      inputId: "skin-unlocked-" + numOfSkins,
      inputType: "checkbox", 
      classList: ["col-2"]
    })
  );

  checkboxDiv.appendChild(
    createSkinElement({
      labelName: "Always Animate",
      inputId: "skin-alwaysAnimate-" + numOfSkins,
      inputType: "checkbox", 
      classList: ["col-3"]
    })
  );

  div.appendChild(checkboxDiv);
  div.appendChild(createFramesDiv(numOfSkins));

  document.getElementById("skinContainer").appendChild(div);
  document.getElementById("skin-unlocked-" + numOfSkins).checked = true;
  numOfSkins += 1;
  removeHide("removeSkinForm");
});

document.getElementById("removeSkinForm").addEventListener("click", () => {
  if (numOfSkins > 0) {
    const element = document.getElementById("skin-form-" + (numOfSkins - 1));
    element.parentNode.removeChild(element);
    numOfSkins -= 1;
    if (numOfSkins === 0) {
      hide("removeSkinForm");
    }
    return false;
  }
  return true;
});


const createModifierElement = (labelName, inputId, required = false, step=0.1) => {
  const label = createLabel(capitalizeFirstLetter(labelName));
  label.setAttribute("for", inputId);
  const input = createInput("number", inputId, inputId);
  input.value = 0;
  input.required = required;
  input.step = step;
  const div = createDiv({classList: [ "col-2" ]});
  div.appendChild(label);
  div.appendChild(input);
  return div;
}


const capitalizeFirstLetter = (string) => {
    return string.charAt(0).toUpperCase() + string.slice(1);
}
 

const createModiferSection = (i) => {
  const box = createDiv({classList: [ "box" ]});
  const div = createDiv({classList: ["modifier-form-instance", "row", "gtr-uniform"], id: `modifier-form-${i}`});
  if (i == 0) {
    box.appendChild(createLabel("Initial stats"));
  } else {
    box.appendChild(createLabel("Stat Modifier " + (i)));
  }

  const ShiftByOne = ["level", "revivals", "skips", "banish", "rerolls"]

  for (const [key] of Object.entries(statModifierJson)) {
    div.appendChild(createModifierElement(key, "mod-" + key + "-" + i, key === "level", ShiftByOne.includes(key) ? 1 : 0.1));
  }

  box.appendChild(div);
  return box;
}

document.getElementById("addModifierForm").addEventListener("click", () => {
  let i = numOfModifiers;
  const div = createModiferSection(i);
  document.getElementById("modifierContainer").appendChild(div);
  if (i == 0) {
    document.getElementById("mod-level-0").value = 1;
    document.getElementById("mod-level-0").readOnly = true;
  }
  numOfModifiers += 1;
  removeHide("removeModifierForm");
});

document.getElementById("removeModifierForm").addEventListener("click", () => {
  if (numOfModifiers > 0) {
    const element = document.getElementById("modifier-form-" + (numOfModifiers - 1));
    element.parentNode.parentElement.removeChild(element.parentNode);
    numOfModifiers -= 1;
    if (numOfModifiers === 0) {
      hide("removeModifierForm");
    }
    return false;
  }
  return true;
});

let showCaseChoices = null;

const createChoices = () => {
  const defaults = () => {
    const choices = [];
    for (const item of equipmentJson.equipment) {
      choices.push({
        value: item,
        label: item
      });
    }
    return choices;
  }
  

  const element = document.querySelector("#showcase");
  showCaseChoices = new Choices(element, {
    removeItemButton: true,
    choices: defaults(),
    placeholder: true,
    placeholderValue: "Showcase items   ",
    itemSelectText: "",
    allowHTML: true,
    classNames: {
      button: "choices__button ignore-style",
      item: "choices__item" // button",
    },
    callbackOnCreateTemplates: (template) => {
      return {
        item: ({classNames}, data) => {
          const div = Choices.defaults.templates.item.call(this, showCaseChoices.config, data, showCaseChoices.config.removeItemButton);
          div.className += " button";
          if(div.hasChildNodes()) {
            for (const node of div.childNodes) {
              if (node.type === "button") {
                node.innerHTML = "<i class=\"fas fa-times\" style=\"color:#f56a6a\"></i>";
                break;
              }
            }
          }
          return div;
        }
      }
    }
  });

  const form = document.querySelector("#basic-form");
  element.addEventListener("addItem", (e) => {
    showCaseChoices.setChoices([{
      value: e.detail.value,
      label: e.detail.label,
    }], "value", "label", false);
    createJsonOutput(form);
  });

  element.addEventListener("removeItem", (e) => {
    showCaseChoices.setChoices(defaults(), "value", "label", true);
    createJsonOutput(form);
  });
};

const setupDownload = () => {
  const button = document.getElementById("downloadButton");
  const zipButton = document.getElementById("downloadZipButton");

  button.addEventListener("click", () => {
    const data = [document.getElementById("outputJsonTextArea").textContent];
    const file = new File(data, "character.json", {
      type: "text/json",
    });

    // This is the url to download the image
    const blobURL = URL.createObjectURL(file);

    // Force the download
    const anchor = document.createElement("a");
    const clickEvent = new MouseEvent("click");
    anchor.href = blobURL;
    anchor.download = file.name;
    anchor.dispatchEvent(clickEvent);
  });

  zipButton.addEventListener("click", async () => {
    const obj = new FieldsToZipFile("character_pack", "outputJsonTextArea", ["spriteName"], ["skin-spriteName-"], [...currentFiles.values()].flat(1));
    obj.zip();
  });


};

const removeHide = (elementId) => {
  const element = document.getElementById(elementId);
  let className = element.className;
  className = className.replace(/\b(hide)\b/g, "");
  element.className = className;
};

const hide = (elementId) => {
  const element = document.getElementById(elementId);
  element.className += " hide";
}


const handleFrameDropbox = (container) => {

  // modify all of the event types needed for the script so that the browser
  // doesn't open the image in the browser tab (default behavior)
  ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(evt => {
    container.addEventListener(evt, prevDefault, false);
  });
  function prevDefault (e) {
    e.preventDefault();
    e.stopPropagation();
  }

  // remove and add the hover class, depending on whether something is being
  // actively dragged over the box area
  ['dragenter', 'dragover'].forEach(evt => {
    container.addEventListener(evt, hover, false);
  });
  ['dragleave', 'drop'].forEach(evt => {
    container.addEventListener(evt, unhover, false);
  });
  function hover(e) {
    container.classList.add('hover');
  }
  function unhover(e) {
    container.classList.remove('hover');
  }

  container.addEventListener('drop', mngDrop, false);
  function mngDrop(e) {
    let dataTrans = e.dataTransfer;
    let files = dataTrans.files;
    filesManager(container.id, files);
  }
}

// use the FileReader API to get the image data, create an img element, and add
// it to the gallery div. The API is asynchronous so onloadend is used to get the
// result of the API function
function previewFile(idPrefix, file) {

  let imageType = /image.*/;
  if (file.type.match(imageType)) {
    let fReader = new FileReader();
    let gallery = document.getElementById(`${idPrefix}-gallery`);
    
    fReader.readAsDataURL(file);

    fReader.onloadend = function() {
      let wrap = document.createElement('div');
      wrap.classList.add("file-label", "file-label--active", "col-3");

      let img = document.createElement('img');
      img.classList.add("frame-img");
      img.src = fReader.result;

      let imgCapt = document.createElement('p');
      let fSize = (file.size / 1000) + ' KB';
      imgCapt.innerHTML = `<span class="fName">${file.name}</span>`; //<span class="fSize">${fSize}</span><span class="fType">${file.type}</span>

      gallery.appendChild(wrap).appendChild(img);
      gallery.appendChild(wrap).appendChild(imgCapt);
    }

    const rmContainer = document.getElementById(`${idPrefix}-rm-container`);
    if (rmContainer && rmContainer.childElementCount === 0) {
      const rmButton = document.createElement("input");
      rmButton.type = "button";
      rmButton.id = `${idPrefix}-rm-button`;
      rmButton.value = `Remove All Images`;
      rmButton.addEventListener("click", (ev) => {
        const button = ev.target;
        const prefix = button.id.substring(0, button.id.indexOf("-rm-button"));
        const inputId = `${prefix}-input`;
        const galleryId = `${prefix}-gallery`;
        const input = document.getElementById(inputId);
        const gall = document.getElementById(galleryId);
        // Remove contents of Frames
        input.value = "";
        gall.innerHTML = "";
        // Remove this button too.
        document.getElementById(`${prefix}-rm-container`).innerHTML = "";
        currentFiles.set(idPrefix, []);
        createJsonOutput(document.querySelector("#skins"));
      });
      rmContainer.appendChild(rmButton);
    }

  } else {
    console.error("Only images are allowed!", file);
  }
}

const currentFiles = new Map();

const filesManager = (idPrefix, files) => {
  if (!currentFiles.has(idPrefix)) {
    currentFiles.set(idPrefix, []);
  }

  files = [...files];
  files.forEach((file) => previewFile(idPrefix, file));

  currentFiles.get(idPrefix).push(...files);

  // Update output:
  const form = document.querySelector("#skins");
  if (form) {
    createJsonOutput(form);
  }
};

const showMain = () => {
  const gei = (id) => document.getElementById(id);
  gei("basic-section").classList.remove("hide");
  gei("skins-section").classList.remove("hide");
  gei("modifier-section").classList.remove("hide");
  gei("output-section").classList.remove("hide");
  gei("about-section").classList.add("hide");
}

const showAbout = () => {
  const gei = (id) => document.getElementById(id);
  gei("basic-section").classList.add("hide");
  gei("skins-section").classList.add("hide");
  gei("modifier-section").classList.add("hide");
  gei("output-section").classList.add("hide");
  gei("about-section").classList.remove("hide");
};

// Onload
window.addEventListener("load", (event) => {
  const forms = document.querySelectorAll("form");
  for (const form of forms) {
    const onInput = () => {
      // Handle the different types of forms here.
      if (form) {
        createJsonOutput(form);
      }
    };
    if (form.id === "basic-form" || form.id === "modifiers" || form.id === "skins")
      form.addEventListener("input", onInput);
  }

  setupStartingWeapon();
  createChoices();
  setupDownload();
});



