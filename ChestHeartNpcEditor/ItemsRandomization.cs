using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Drawing.Imaging;

namespace ChestHeartNpcEditor
{
    public partial class BlockDesigner
    {
    }
    public partial class Form1
    {
        private void checkallButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < itemcheckedListBox.Items.Count; i++)
            {
                itemcheckedListBox.SetItemChecked(i, true);
            }
        }

        private void checkjunkButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < itemcheckedListBox.Items.Count; i++)
            {
                itemcheckedListBox.SetItemChecked(i, true);
            }

            itemcheckedListBox.SetItemChecked(13, false);
            itemcheckedListBox.SetItemChecked(14, false);
            itemcheckedListBox.SetItemChecked(15, false);
            itemcheckedListBox.SetItemChecked(17, false);
            itemcheckedListBox.SetItemChecked(18, false);
            itemcheckedListBox.SetItemChecked(19, false);
            //checkedListBox1.SetItemChecked(21, false);
            itemcheckedListBox.SetItemChecked(40, false);
            itemcheckedListBox.SetItemChecked(41, false);
            itemcheckedListBox.SetItemChecked(42, false);
            itemcheckedListBox.SetItemChecked(44, false);
            itemcheckedListBox.SetItemChecked(76, false);
            for (int i = 21; i < 39; i++)
            {
                itemcheckedListBox.SetItemChecked(i, false);
            }
            for (int i = 45; i < 63; i++)
            {
                itemcheckedListBox.SetItemChecked(i, false);
            }
            for (int i = 64; i < 75; i++)
            {
                itemcheckedListBox.SetItemChecked(i, false);
            }
            itemcheckedListBox.SetItemChecked(73, true);
        }

        private void uncheckallButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < itemcheckedListBox.Items.Count; i++)
            {
                itemcheckedListBox.SetItemChecked(i, false);
            }
        }

        private void itemsrandomizeButton_Click(object sender, EventArgs e)
        {
            foreach (Chest c in chest_list)
            {
                foreach (Item i in itemcheckedListBox.CheckedItems)
                {
                    if (Form1.ROM_DATA[c.address] == i.address)
                    {
                        iitem.Add(c.address);
                    }
                }
            }


            Random r = new Random();
            for (int i = 0; i < iitem.Count; i++)
            {
                int rp = r.Next(itemcheckedListBox.CheckedItems.Count);
                Form1.ROM_DATA[iitem[i]] = (byte)((itemcheckedListBox.CheckedItems[rp] as Item).address);
                foreach (Chest c in chest_list)
                {
                    if (c.address == iitem[i]) { LogAdd(c.name + " : " + (itemcheckedListBox.CheckedItems[rp] as Item).Name, Color.Black); break; }
                }
            }
        }

        private void logdungeonsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 13; i++)
            {
                LogAdd("Dungeon : [" + dungeonComboBox.Items[i] + "]", Color.Black);
                bool map = false;
                bool compass = false;
                bool bigkey = false;
                byte keys = 0;


                foreach (Chest c in chest_list)
                {
                    if (c.region == i) //Check All Regions except agahtower
                    {
                        if (c.region != 4)
                        {
                            if (Form1.ROM_DATA[c.address] == 0x33) //map
                            {
                                map = true;
                            }
                            if (Form1.ROM_DATA[c.address] == 0x25) //compass
                            {
                                compass = true;
                            }
                            if (Form1.ROM_DATA[c.address] == 0x32) //compass
                            {
                                bigkey = true;
                            }
                        }
                        else
                        {
                            //check if both chest have a key

                        }
                        if (Form1.ROM_DATA[c.address] == 0x24) //compass
                        {
                            keys++;
                        }
                    }

                }
                if (i != 4)
                {
                    if (map) { LogAdd("Map Found", Color.Black); } else { LogAdd("WARNING:Map Not Found", Color.DarkBlue); };
                    if (compass) { LogAdd("Compass Found", Color.Black); } else { LogAdd("WARNING:Compass Not Found", Color.DarkBlue); };
                }
                if (i == 0 | i == 4 | i == 6 | i == 7)
                {
                    if (bigkey) { LogAdd("Big Key Found", Color.Black); } else { LogAdd("WARNING:Big Key Not Found", Color.DarkBlue); };
                }
                else
                {
                    if (bigkey) { LogAdd("Big Key Found", Color.Black); } else { LogAdd("ERROR:Big Key Not Found", Color.Red); };
                }
                if (i == 0) { if (keys >= 1) { LogAdd("Keys : " + keys + " / 1", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 1", Color.Red); } }
                if (i == 1) { if (keys >= 0) { LogAdd("Keys : " + keys + " / 0", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 0", Color.Red); } }
                if (i == 2) { if (keys >= 0) { LogAdd("Keys : " + keys + " / 0", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 0", Color.Red); } }
                if (i == 3) { if (keys >= 0) { LogAdd("Keys : " + keys + " / 0", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 0", Color.Red); } }
                if (i == 4) { if (keys >= 2) { LogAdd("Keys : " + keys + " / 2", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 2", Color.Red); } }
                if (i == 5) { if (keys >= 6) { LogAdd("Keys : " + keys + " / 6", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 6", Color.Red); } }
                if (i == 6) { if (keys >= 1) { LogAdd("Keys : " + keys + " / 1", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 1", Color.Red); } }
                if (i == 7) { if (keys >= 3) { LogAdd("Keys : " + keys + " / 3", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 3", Color.Red); } }
                if (i == 8) { if (keys >= 1) { LogAdd("Keys : " + keys + " / 1", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 1", Color.Red); } }
                if (i == 9) { if (keys >= 2) { LogAdd("Keys : " + keys + " / 2", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 2", Color.Red); } }
                if (i == 10) { if (keys >= 3) { LogAdd("Keys : " + keys + " / 3", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 3", Color.Red); } }
                if (i == 11) { if (keys >= 4) { LogAdd("Keys : " + keys + " / 4", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 4", Color.Red); } }
                if (i == 12) { if (keys >= 2) { LogAdd("Keys : " + keys + " / 2", Color.Black); } else { LogAdd("Warning Keys : " + keys + " / 2", Color.Red); } }
            }
        }

        private void logcheckitemsButton_Click(object sender, EventArgs e)
        {
            int[] itemscount = new int[itemcheckedListBox.CheckedItems.Count];
            foreach (Chest c in chest_list)
            {
                for (int i = 0; i < itemcheckedListBox.CheckedItems.Count; i++)
                {
                    if (Form1.ROM_DATA[c.address] == (itemcheckedListBox.CheckedItems[i] as Item).address)
                    {
                        itemscount[i]++;
                    }
                }
            }
            for (int i = 0; i < itemcheckedListBox.CheckedItems.Count; i++)
            {
                LogAdd((itemcheckedListBox.CheckedItems[i] as Item).Name + " : " + itemscount[i], Color.Black);
            }
        }

        private void clearlogButton_Click(object sender, EventArgs e)
        {
            itemlogsRichtextbox.Clear();
        }

        private void compassmapButton_Click(object sender, EventArgs e)
        {
            //Check all chest of specific dungeon to see if there's any compass or map
            //
            List<string> log = new List<string>();

            for (int j = 0; j < 13; j++)
            {
                bool map = false;
                bool comp = false;
                //Check if there's map and compass
                foreach (Chest c in chest_list)
                {
                    if (c.region == j) //Escape
                    {
                        if (c.region != 4)
                        {

                            if (Form1.ROM_DATA[c.address] == 0x33) //map
                            {
                                map = true;
                                //LOG MAP WAS FOUND IN {REGION}
                                LogAdd("Map found in " + dungeonComboBox.Items[j], Color.Green);
                            }
                            if (Form1.ROM_DATA[c.address] == 0x25) //compass
                            {
                                comp = true;
                                //LOG COMPASS WAS FOUND IN {REGION}
                                LogAdd("Compass found in " + dungeonComboBox.Items[j], Color.Green);
                            }
                            if (map && comp)
                            {
                                break;
                            }
                        }
                    }
                }


                bool mapset = false;
                bool compset = false;
                //search for the 1st selected item replace it by the map
                foreach (Chest c in chest_list)
                {
                    if (c.region == j) //Check All Regions except agahtower
                    {
                        if (c.region != 4)
                        {
                            if (map == false)
                            {
                                foreach (Item i in itemcheckedListBox.CheckedItems)
                                {
                                    if (Form1.ROM_DATA[c.address] == i.address)
                                    {

                                        Form1.ROM_DATA[c.address] = 0x33;//set it to map
                                        LogAdd("Map added in " + dungeonComboBox.Items[j] + " Replaced : " + i.Name, Color.BlueViolet);
                                        mapset = true;
                                        map = true;
                                        break;
                                    }
                                }
                                if (mapset == false)
                                {
                                    //LOG CAN'T FIND A PLACE TO STORE A MAP IN SELECTED ITEMS
                                    LogAdd("no chest avaible for a map in " + dungeonComboBox.Items[j], Color.Yellow);
                                }
                            }
                            if (comp == false)
                            {

                                foreach (Item i in itemcheckedListBox.CheckedItems)
                                {
                                    if (Form1.ROM_DATA[c.address] == i.address)
                                    {
                                        Form1.ROM_DATA[c.address] = 0x25;//set it to compass
                                        LogAdd("Compass added in " + dungeonComboBox.Items[j] + " Replaced : " + i.Name, Color.BlueViolet);
                                        compset = true;
                                        comp = true;
                                        break;
                                    }
                                }
                                if (compset == false)
                                {
                                    //LOG CAN'T FIND A PLACE TO STORE A COMPASS IN SELECTED ITEMS
                                    LogAdd("no chest avaible for a compass in" + dungeonComboBox.Items[j], Color.Yellow);
                                }

                            }
                        }
                        if (map && comp)
                        {
                            //log.Add("map and compass have been added in " + f.dungeonComboBox.Items[j]);
                            break;
                        }

                    }
                }
            }
        }
    }
}
