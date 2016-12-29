using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChestHeartNpcEditor
{
    public class Chest
    {

        public string name;
        public int region;
        public int image_id;
        public int address;
        public Item itemin;
        public Chest(string name,int region,int image_id,int address,Item itemin = null)
        {
            this.name = name;
            this.region = region;
            this.image_id = image_id;
            this.address = address;
            this.itemin = itemin;
        }
        public string Name
        {
            get
            {
                return name;
            }
        }

    }


    public partial class Form1 : Form
    {
        public void add_chests()
        {
            //Escape Chests
            chest_list.Add(new Chest("Escape - Secret entrance of hyrule castle", 0, 0, 0xE971));
            chest_list.Add(new Chest("Escape - 1st blue keyguard room (Map Chest)", 0, 1, 0xEB0C));
            chest_list.Add(new Chest("Escape - Boomerang chest", 0, 2, 0xE974));
            chest_list.Add(new Chest("Escape - Zelda cell chest", 0, 3, 0xEB09));
            chest_list.Add(new Chest("Escape - Dark room key chest", 0, 4, 0xE96E));
            chest_list.Add(new Chest("Escape - Bombable part [Chest 1 Left]", 0, 5, 0xEB5D));
            chest_list.Add(new Chest("Escape - Bombable part [Chest 2 Middle]", 0, 5, 0xEB60));
            chest_list.Add(new Chest("Escape - Bombable part [Chest 3 Right]", 0, 5, 0xEB63));
            chest_list.Add(new Chest("Escape - Sanctuary chest", 0, 6, 0xEA79));
            //Eastern Chests
            chest_list.Add(new Chest("Eastern Palace - Compass Room", 1, 1, 0xE977));
            chest_list.Add(new Chest("Eastern Palace - Big Chest", 1, 4, 0xE97D));
            chest_list.Add(new Chest("Eastern Palace - Canon Ball Room", 1, 0, 0xE9B3));
            chest_list.Add(new Chest("Eastern Palace - Big Key", 1, 3, 0xE9B9));
            chest_list.Add(new Chest("Eastern Palace - Map Room", 1, 2, 0xE9F5));
            chest_list.Add(new Chest("Eastern Palace - BOSS Drop", 1, 2, 0x180150));
            //Desert Palace
            chest_list.Add(new Chest("Desert Palace - Big Chest", 2, 1, 0xE98F));
            chest_list.Add(new Chest("Desert Palace - Map Room", 2, 0, 0xE9B6));
            chest_list.Add(new Chest("Desert Palace - Compass Room", 2, 2, 0xE9CB));
            chest_list.Add(new Chest("Desert Palace - Big Key", 2, 3, 0xE9C2));
            chest_list.Add(new Chest("Desert Palace - BOSS Drop", 2, 3, 0x180151));
            //Hera Tower
            chest_list.Add(new Chest("Hera Tower - Big Key", 3, 0, 0xE9E6));
            chest_list.Add(new Chest("Hera Tower - Big Chest", 3, 2, 0xE9F8));
            chest_list.Add(new Chest("Hera Tower - Big Chest Room", 3, 3, 0xE9FB));
            chest_list.Add(new Chest("Hera Tower - Entrance", 3, 1, 0xE9AD));
            chest_list.Add(new Chest("Hera Tower - BOSS Drop", 3, 1, 0x180152));
            //Agahnim Tower
            chest_list.Add(new Chest("Agahnim Tower - Dark Maze Room", 4, 0, 0xEAB2));
            chest_list.Add(new Chest("Agahnim Tower - Invisible 2 blue guard spawn", 4, 1, 0xEAB5));
            //Palace of Darkness
            chest_list.Add(new Chest("Palace of Darkness - Big Key", 5, 3, 0xEA37));
            chest_list.Add(new Chest("Palace of Darkness - Invisible stalfos spawn chest", 5, 1, 0xEA49));
            chest_list.Add(new Chest("Palace of Darkness - Big Chest", 5, 8, 0xEA40));
            chest_list.Add(new Chest("Palace of Darkness - Compass Room", 5, 7, 0xEA43));
            chest_list.Add(new Chest("Palace of Darkness - Spike Statue Room", 5, 6, 0xEA46));
            chest_list.Add(new Chest("Palace of Darkness - Dark Basement Left", 5, 2, 0xEA4C));
            chest_list.Add(new Chest("Palace of Darkness - Dark Basement Right", 5, 2, 0xEA4F));
            chest_list.Add(new Chest("Palace of Darkness - Statue Push Room", 5, 4, 0xEA52));
            chest_list.Add(new Chest("Palace of Darkness - Dark Maze Top Chest", 5, 10, 0xEA55));
            chest_list.Add(new Chest("Palace of Darkness - Dark Maze Bottom Chest", 5, 9, 0xEA58));
            chest_list.Add(new Chest("Palace of Darkness - First Basement Chest", 5, 0, 0xEA5B));
            chest_list.Add(new Chest("Palace of Darkness - Main Room Left Chest", 5, 9, 0xEA3D));
            chest_list.Add(new Chest("Palace of Darkness - Main Room Right Chest", 5, 9, 0xEA3A));
            chest_list.Add(new Chest("Palace of Darkness - BOSS Drop", 5, 9, 0x180153));
            //Swamp Palace
            chest_list.Add(new Chest("Swamp Palace - First Room", 6, 0, 0xEA9D));
            chest_list.Add(new Chest("Swamp Palace - South of Big Chest", 6, 2, 0xEAA0));
            chest_list.Add(new Chest("Swamp Palace - Big Key", 6, 5, 0xEAA6));
            chest_list.Add(new Chest("Swamp Palace - Flood Room Left Chest", 6, 6, 0xEAA9));
            chest_list.Add(new Chest("Swamp Palace - Flood Room Right Chest", 6, 6, 0xEAAC));
            chest_list.Add(new Chest("Swamp Palace - Hidden Waterfall Room", 6, 7, 0xEAAF));
            chest_list.Add(new Chest("Swamp Palace - Left side of Big Key", 6, 4, 0xEAA3));
            chest_list.Add(new Chest("Swamp Palace - Map Room (bombable)", 6, 1, 0xE986));
            chest_list.Add(new Chest("Swamp Palace - Big Chest", 6, 3, 0xE989));
            chest_list.Add(new Chest("Swamp Palace - BOSS Drop", 6, 3, 0x180154));
            //Skull Woods
            chest_list.Add(new Chest("Skull Woods - Compass Room", 7, 6, 0xE992));
            chest_list.Add(new Chest("Skull Woods - Big Chest", 7, 1, 0xE998));
            chest_list.Add(new Chest("Skull Woods - East of Big Chest", 7, 0, 0xE99B));
            chest_list.Add(new Chest("Skull Woods - Big Key Room", 7, 3, 0xE99E));
            chest_list.Add(new Chest("Skull Woods - West of Big Chest", 7, 2, 0xE9A1));
            chest_list.Add(new Chest("Skull Woods - Trap Room South of Big Chest", 7, 4, 0xE9C8));
            chest_list.Add(new Chest("Skull Woods - Boss Section", 7, 5, 0xE9FE));
            chest_list.Add(new Chest("Skull Woods - BOSS Drop", 7, 5, 0x180155));
            //Thieve Town
            chest_list.Add(new Chest("Thieves Town - Bottom Left North Room", 8, 4, 0xEA0A));
            chest_list.Add(new Chest("Thieves Town - Bottom Left East Room", 8, 5, 0xEA07));
            chest_list.Add(new Chest("Thieves Town - Big Key", 8, 2, 0xEA04));
            chest_list.Add(new Chest("Thieves Town - Top Left Entrance Room", 8, 3, 0xEA01));
            chest_list.Add(new Chest("Thieves Town - Room Above boss (bomb floor)", 8, 6, 0xEA0D));
            chest_list.Add(new Chest("Thieves Town - Big Chest", 8, 1, 0xEA10));
            chest_list.Add(new Chest("Thieves Town - Blind cell", 8, 0, 0xEA13));
            chest_list.Add(new Chest("Thieves Town - BOSS Drop", 8, 0, 0x180156));
            //Ice Palace
            chest_list.Add(new Chest("Ice Palace - Invisible 2iceman spawn", 9, 4, 0xE995));
            chest_list.Add(new Chest("Ice Palace - Big Key", 9, 1, 0xE9A4));
            chest_list.Add(new Chest("Ice Palace - Big Chest", 9, 5, 0xE9AA));
            chest_list.Add(new Chest("Ice Palace - Compass Room", 9, 0, 0xE9D4));
            chest_list.Add(new Chest("Ice Palace - Map Room", 9, 2, 0xE9DD));
            chest_list.Add(new Chest("Ice Palace - Spike Room", 9, 3, 0xE9E0));
            chest_list.Add(new Chest("Ice Palace - Invisible chest Staircase", 9, 6, 0xE9E3));
            chest_list.Add(new Chest("Ice Palace - BOSS Drop", 9, 6, 0x180157));
            //Misery Mire
            chest_list.Add(new Chest("Misery Mire - Invisible chest Spike room", 10, 5, 0xE9DA));
            chest_list.Add(new Chest("Misery Mire - Invisible chest main room", 10, 2, 0xEA5E));
            chest_list.Add(new Chest("Misery Mire - Bridge chest", 10, 6, 0xEA61));
            chest_list.Add(new Chest("Misery Mire - Compass Room", 10, 1, 0xEA64));
            chest_list.Add(new Chest("Misery Mire - Big Chest", 10, 4, 0xEA67));
            chest_list.Add(new Chest("Misery Mire - Map Room", 10, 3, 0xEA6A));
            chest_list.Add(new Chest("Misery Mire - Big Key", 10, 0, 0xEA6D));
            chest_list.Add(new Chest("Misery Mire - BOSS Drop", 10, 0, 0x180158));
            //Turtle Rock
            chest_list.Add(new Chest("Turtle Rock - Invisible Chain chomp chest", 11, 2, 0xEA16));
            chest_list.Add(new Chest("Turtle Rock - Big Chest", 11, 4, 0xEA19));
            chest_list.Add(new Chest("Turtle Rock - Map Room [Left Chest]", 11, 1, 0xEA1C));
            chest_list.Add(new Chest("Turtle Rock - Map Room [Right Chest]", 11, 1, 0xEA1F));
            chest_list.Add(new Chest("Turtle Rock - Compass Room", 11, 0, 0xEA22));
            chest_list.Add(new Chest("Turtle Rock - Big Key Room", 11, 3, 0xEA25));
            chest_list.Add(new Chest("Turtle Rock - Laser Room 1st Right Chest", 11, 6, 0xEA28));
            chest_list.Add(new Chest("Turtle Rock - Laser Room 1st Left Chest", 11, 6, 0xEA2B));
            chest_list.Add(new Chest("Turtle Rock - Laser Room 2nd Right Chest", 11, 6, 0xEA2E));
            chest_list.Add(new Chest("Turtle Rock - Laser Room 2nd Left Chest", 11, 6, 0xEA31));
            chest_list.Add(new Chest("Turtle Rock - Roller Switch Room", 11, 5, 0xEA34));
            chest_list.Add(new Chest("Turtle Rock - BOSS Drop", 11, 5, 0x180159));
            //Ganon Tower
            chest_list.Add(new Chest("Ganon Tower - North of Gap room [Top Left Chest]", 12, 0, 0xEAB8));
            chest_list.Add(new Chest("Ganon Tower - North of Gap room [Top Right Chest]", 12, 0, 0xEABB));
            chest_list.Add(new Chest("Ganon Tower - North of Gap room [Bottom Left Chest]", 12, 0, 0xEABE));
            chest_list.Add(new Chest("Ganon Tower - North of Gap room [Bottom Right Chest]", 12, 0, 0xEAC1));
            chest_list.Add(new Chest("Ganon Tower - West of warp room (bomb) [Top Left Chest]", 12, 4, 0xEAC4));
            chest_list.Add(new Chest("Ganon Tower - West of warp room (bomb) [Top Right Chest]", 12, 4, 0xEAC7));
            chest_list.Add(new Chest("Ganon Tower - West of warp room (bomb) [Bottom Left Chest]", 12, 4, 0xEACA));
            chest_list.Add(new Chest("Ganon Tower - West of warp room (bomb) [Bottom Right Chest]", 12, 4, 0xEACD));
            chest_list.Add(new Chest("Ganon Tower - Hookshot firebar chest", 12, 8, 0xEAD0));
            chest_list.Add(new Chest("Ganon Tower - Map Room", 12, 1, 0xEAD3));
            chest_list.Add(new Chest("Ganon Tower - Big Chest", 12, 2, 0xEAD6));
            chest_list.Add(new Chest("Ganon Tower - East Side 1st room [Left Chest]", 12, 3, 0xEAD9));
            chest_list.Add(new Chest("Ganon Tower - East Side 1st room [Right Chest]", 12, 3, 0xEADC));
            chest_list.Add(new Chest("Ganon Tower - Tile Room", 12, 6, 0xEAE2));
            chest_list.Add(new Chest("Ganon Tower - Before Ice Armos", 12, 5, 0xEADF));
            chest_list.Add(new Chest("Ganon Tower - Compass Room [Top Left Chest]", 12, 7, 0xEAE5));
            chest_list.Add(new Chest("Ganon Tower - Compass Room [Top Right Chest]", 12, 7, 0xEAE8));
            chest_list.Add(new Chest("Ganon Tower - Compass Room [Bottom Left Chest]", 12, 7, 0xEAEB));
            chest_list.Add(new Chest("Ganon Tower - Compass Room [Bottom Right Chest]", 12, 7, 0xEAEE));
            chest_list.Add(new Chest("Ganon Tower - North of ice Armos [Bottom Chest]", 12, 12, 0xEAF1));
            chest_list.Add(new Chest("Ganon Tower - North of ice Armos [Left Chest] ", 12, 12, 0xEAF4));
            chest_list.Add(new Chest("Ganon Tower - North of ice Armos [Right Chest] ", 12, 12, 0xEAF7));
            chest_list.Add(new Chest("Ganon Tower - North of 4 torch falling floor [Left Chest]", 12, 9, 0xEAFD));
            chest_list.Add(new Chest("Ganon Tower - North of 4 torch falling floor [Right Chest]", 12, 9, 0xEB00));
            chest_list.Add(new Chest("Ganon Tower - Before Moldorm2", 12, 10, 0xEB03));
            chest_list.Add(new Chest("Ganon Tower - After Moldorm2", 12, 11, 0xEB06));
            //Caves / Houses
            chest_list.Add(new Chest("Cave - Magic Cape Chest", 13, 5, 0xE97A));
            chest_list.Add(new Chest("Cave - Light World Swamp Chest", 13, 14, 0xE98C));
            chest_list.Add(new Chest("House - Link's House Chest", 13, 25, 0xE9BC));
            chest_list.Add(new Chest("Cave - Spiral Cave Chest", 13, 2, 0xE9BF));
            chest_list.Add(new Chest("Cave - Mimic Cave (mirror Trock)", 13, 3, 0xE9C5));
            chest_list.Add(new Chest("House - Tavern", 13, 11, 0xE9CE));
            chest_list.Add(new Chest("House - Chicken House", 13, 10, 0xE9E9));
            chest_list.Add(new Chest("House - Bombable Hut (bottom TT)", 13, 21, 0xE9EC));
            chest_list.Add(new Chest("House - C-Shaped House (DW)", 13, 24, 0xE9EF));
            chest_list.Add(new Chest("Cave - Aginah's Cave Chest", 13, 13, 0xE9F2));
            chest_list.Add(new Chest("Cave - West Mire [Left Chest]", 13, 22, 0xEA73));
            chest_list.Add(new Chest("Cave - West Mire [Right Chest]", 13, 22, 0xEA76));
            chest_list.Add(new Chest("Cave - DW Death Mountain [Top Chest]", 13, 19, 0xEA7C));
            chest_list.Add(new Chest("Cave - DW Death Mountain [Bottom Chest]", 13, 19, 0xEA7F));
            chest_list.Add(new Chest("Cave - Sahasrahla's hut [Left Chest]", 13, 12, 0xEA82));
            chest_list.Add(new Chest("Cave - Sahasrahla's hut [Middle Chest]", 13, 12, 0xEA85));
            chest_list.Add(new Chest("Cave - Sahasrahla's hut [Right Chest]", 13, 12, 0xEA88));
            chest_list.Add(new Chest("Cave - Byrna Cave Chest", 13, 16, 0xEA8B));
            chest_list.Add(new Chest("Cave - Kakariko Well top (bombable) chest", 13, 6, 0xEA8E));
            chest_list.Add(new Chest("Cave - Kakariko Well [Left Chest]", 13, 7, 0xEA91));
            chest_list.Add(new Chest("Cave - Kakariko Well [Middle Chest]", 13, 7, 0xEA94));
            chest_list.Add(new Chest("Cave - Kakariko Well [Right Chest]", 13, 7, 0xEA97));
            chest_list.Add(new Chest("Cave - Kakariko Well [Bottom Chest]", 13, 7, 0xEA9A));
            chest_list.Add(new Chest("Cave - Thieve Hut [Top Chest]", 13, 8, 0xEB0F));
            chest_list.Add(new Chest("Cave - Thieve Hut [Top Left Chest]", 13, 9, 0xEB12));
            chest_list.Add(new Chest("Cave - Thieve Hut [Top Right Chest]", 13, 9, 0xEB15));
            chest_list.Add(new Chest("Cave - Thieve Hut [Bottom Left Chest]", 13, 9, 0xEB18));
            chest_list.Add(new Chest("Cave - Thieve Hut [Bottom Right Chest]", 13, 9, 0xEB1B));
            chest_list.Add(new Chest("Cave - Northeast Swamp (bomb)[Top Chest]", 13, 23, 0xEB1E));
            chest_list.Add(new Chest("Cave - Northeast Swamp (bomb)[Middle Top Chest]", 13, 23, 0xEB21));
            chest_list.Add(new Chest("Cave - Northeast Swamp (bomb)[Middle Bottom Chest]", 13, 23, 0xEB24));
            chest_list.Add(new Chest("Cave - Northeast Swamp (bomb)[Bottom Chest]", 13, 23, 0xEB27));
            chest_list.Add(new Chest("Cave - Wall of caves (LW) [Top Left Chest]", 13, 0, 0xEB2A));
            chest_list.Add(new Chest("Cave - Wall of caves (LW) [Top Middle Left Chest]", 13, 0, 0xEB2D));
            chest_list.Add(new Chest("Cave - Wall of caves (LW) [Top Middle Right Chest]", 13, 0, 0xEB30));
            chest_list.Add(new Chest("Cave - Wall of caves (LW) [Top Right Chest]", 13, 0, 0xEB33));
            chest_list.Add(new Chest("Cave - Wall of caves (LW) [Bottom Chest]", 13, 0, 0xEB36));
            chest_list.Add(new Chest("Cave - Wall of caves (LW) [Left Chest]", 13, 1, 0xEB39));
            chest_list.Add(new Chest("Cave - Wall of caves (LW) [Right Chest]", 13, 1, 0xEB3C));
            chest_list.Add(new Chest("Cave - West Sanctuary Pile of rock", 13, 4, 0xEB3F));
            chest_list.Add(new Chest("Cave - Minimoldorm Cave [Bottom Left Chest]", 13, 15, 0xEB42));
            chest_list.Add(new Chest("Cave - Minimoldorm Cave [Top Left Chest]", 13, 15, 0xEB45));
            chest_list.Add(new Chest("Cave - Minimoldorm Cave [Top Right Chest]", 13, 15, 0xEB48));
            chest_list.Add(new Chest("Cave - Minimoldorm Cave [Bottom Right Chest]", 13, 15, 0xEB4B));
            chest_list.Add(new Chest("Cave - Ice Rod Cave Chest", 13, 20, 0xEB4E));
            chest_list.Add(new Chest("Cave - Death Mountain Under Rock (DW) [Top Right Chest]", 13, 17, 0xEB51));
            chest_list.Add(new Chest("Cave - Death Mountain Under Rock (DW) [Top Left Chest]", 13, 17, 0xEB54));
            chest_list.Add(new Chest("Cave - Death Mountain Under Rock (DW) [Bottom Left Chest]", 13, 18, 0xEB57));
            chest_list.Add(new Chest("Cave - Death Mountain Under Rock (DW) [Bottom Right Chest]", 13, 18, 0xEB5A));
            //Standing Heartpieces/ Items,Minigame,Npcs
            chest_list.Add(new Chest("Treasure Chest Minigame", 14, 7, 0xEDA8));
            chest_list.Add(new Chest("Npc - Bottle Vendor", 14, 0, 0x2EB18));
            chest_list.Add(new Chest("Npc - Sahasrahla", 14, 0, 0x2F1FC));
            chest_list.Add(new Chest("Npc - Flute Boy", 14, 0, 0x330C7));
            chest_list.Add(new Chest("Npc - Sick Kid", 14, 4, 0x339CF));
            chest_list.Add(new Chest("Npc - Purple Chest", 14, 25, 0x33D68));
            chest_list.Add(new Chest("Npc - Uncle", 14, 0, 0x2DF45));
            chest_list.Add(new Chest("Npc - Hobo", 14, 0, 0x33E7D));
            chest_list.Add(new Chest("Npc - Ether", 14, 16, 0x180016)); // 0x48B7C));
            chest_list.Add(new Chest("Npc - Bombos", 14, 22, 0x180017));// 0x48B81));
            chest_list.Add(new Chest("Npc - Catfish", 14, 23, 0xEE185));
            chest_list.Add(new Chest("Npc - King Zora", 14, 12, 0xEE1C3));
            chest_list.Add(new Chest("Npc - Old man", 14, 0, 0xF69FA));
            chest_list.Add(new Chest("Npc - Minimoldorm Cave", 14, 6, 0x180010));
            chest_list.Add(new Chest("Npc - North east Swamp (DW)", 14, 10, 0x180011));
            chest_list.Add(new Chest("Npc - Witch", 14, 0, 0x180014));
            chest_list.Add(new Chest("Npc - Cursing Bat (double magic)", 14, 0, 0x180015));
            chest_list.Add(new Chest("Npc - Dwarf Smith", 14, 0, 0x3355C)); //0x3348E+1 CHECK
            chest_list.Add(new Chest("Npc - Fairy Sword Exchange", 14, 0, 0x180028)); //
            
            chest_list.Add(new Chest("Standing Item - Forest Thieve Hideout Heart Piece", 14, 1, 0x180000));
            chest_list.Add(new Chest("Standing Item - Lumberjack Tree Heart Piece", 14, 11, 0x180001));
            chest_list.Add(new Chest("Standing Item - Spectacle Rock Cave Heart Piece", 14, 2, 0x180002));
            chest_list.Add(new Chest("Standing Item - South of hunted grove Heart Piece", 14, 8, 0x180003));
            chest_list.Add(new Chest("Standing Item - Graveyard Cave Heart Piece", 14, 3, 0x180004));
            chest_list.Add(new Chest("Standing Item - Desert North east Block Maze Heart Piece", 14, 9, 0x180005));
            chest_list.Add(new Chest("Standing Item - Blacksmith Pegs Heart Piece", 14, 26, 0x180006));
            chest_list.Add(new Chest("Standing Item - Library", 14, 5, 0x180012));
            chest_list.Add(new Chest("Standing Item - Mushroom", 14, 14, 0x180013));
            chest_list.Add(new Chest("Standing Item - Spectacle Rock Heart Piece", 14, 15, 0x180140));
            chest_list.Add(new Chest("Standing Item - DeathMountain Floating Island Heart Piece", 14, 17, 0x180141));
            chest_list.Add(new Chest("Standing Item - Race Heart Piece", 14, 18, 0x180142));
            chest_list.Add(new Chest("Standing Item - Desert West Side Ledge Heart Piece", 14, 21, 0x180143));
            chest_list.Add(new Chest("Standing Item - Lake Hylia Heart Piece", 14, 19, 0x180144));
            chest_list.Add(new Chest("Standing Item - Swamp Water Drain Heart Piece", 14, 20, 0x180145));
            chest_list.Add(new Chest("Standing Item - Bumper Cave Heart Piece", 14, 24, 0x180146));
            chest_list.Add(new Chest("Standing Item - Pyramid Heart Piece", 14, 27, 0x180147));
            chest_list.Add(new Chest("Standing Item - Digging game Heart Piece", 14, 0, 0x180148));
            chest_list.Add(new Chest("Standing Item - Zora River Heart Piece", 14, 13, 0x180149));
            chest_list.Add(new Chest("Standing Item - Hunted Grove Item dig", 14, 0, 0x18014A));
            chest_list.Add(new Chest("Standing Item - Master Sword Pedestal", 14, 0, 0x289B0));
            //180030

        }
    }
}
