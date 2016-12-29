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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static List<Item> ItemsList = new List<Item>();
        public static List<Chest> chest_list = new List<Chest>();
        public static byte[] ROM_DATA = new byte[2097152];
        string file = "";
        Bitmap loadedImage;
        bool userChanged = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            imageList1.ImageSize = new Size(256, 256);
            imageList1.ColorDepth = ColorDepth.Depth16Bit;
            //(tabPage2 as Control).Enabled = false;
            this.Text = snestopc(0x700000).ToString("X6");
            add_chests();
            itemlistcreate();
            //initEntrances();
            foreach (Item i in ItemsList)
            {
                itemComboBox.Items.Add(i);
            }

            itemComboBox.DisplayMember = "Name";
            randomization_create();
            createDungeonsInfos();

        }
        //Items Randomization Section
        List<int> iitem = new List<int>();
        public void randomization_create()
        {
            itemcheckedListBox.Items.Clear();
            foreach (Item i in Form1.ItemsList)
            {
                itemcheckedListBox.Items.Add(i, true);

            }
            itemcheckedListBox.DisplayMember = "Name";
        }

        public void LogAdd(string Text, Color c)
        {
            itemlogsRichtextbox.SelectionColor = c;
            itemlogsRichtextbox.AppendText(Text + "\n");

        }

        private void chestListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IF Selected Chest Drobbox Change : 

            pictureBox1.Image = imageList1.Images[(chestListBox.SelectedItem as Chest).image_id];
            //Check the address of the chest (if file!=null)
            if (file != "")
            {
                byte b = ROM_DATA[(chestListBox.SelectedItem as Chest).address];
                for (int i = 0; i < ItemsList.Count; i++)
                {
                    if (ItemsList[i].address == b)
                    {
                        itemComboBox.SelectedIndex = i;

                        break;
                    }
                }

            }

        }

        private void dungeonComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //IF Selected Dungeon Dropbox Change : 
            //entranceDestinationCombobox.Enabled = false;
            switch (dungeonComboBox.SelectedIndex)
            {
                case 0:
                    loadedImage = new Bitmap(@"DungeonsChests\Escape.png");
                    // comboBox8.Enabled = false;
                    break;
                case 1:
                    loadedImage = new Bitmap(@"DungeonsChests\Eastern.png");
                    break;
                case 2:
                    loadedImage = new Bitmap(@"DungeonsChests\Desert.png");
                    break;
                case 3:
                    loadedImage = new Bitmap(@"DungeonsChests\Hera.png");
                    break;
                case 4:
                    loadedImage = new Bitmap(@"DungeonsChests\AgahnimTower.png");
                    break;
                case 5:
                    loadedImage = new Bitmap(@"DungeonsChests\PalaceofDarkness.png");
                    break;
                case 6:
                    loadedImage = new Bitmap(@"DungeonsChests\SwampPalace.png");
                    break;
                case 7:
                    loadedImage = new Bitmap(@"DungeonsChests\SkullWood.png");
                    break;
                case 8:
                    loadedImage = new Bitmap(@"DungeonsChests\ThieveTown.png");
                    break;
                case 9:
                    loadedImage = new Bitmap(@"DungeonsChests\IcePalace.png");
                    break;
                case 10:
                    loadedImage = new Bitmap(@"DungeonsChests\MiseryMire.png");
                    break;
                case 11:
                    loadedImage = new Bitmap(@"DungeonsChests\TurtleRock.png");
                    break;
                case 12:
                    loadedImage = new Bitmap(@"DungeonsChests\GanonTower.png");
                    break;
                case 13:
                    loadedImage = new Bitmap(@"DungeonsChests\HouseCaves.png");
                    //entranceDestinationCombobox.Enabled = false;
                    break;
                case 14:
                    loadedImage = new Bitmap(@"DungeonsChests\StandingNpc.png");
                    //entranceDestinationCombobox.Enabled = false;
                    break;
            }
            if (entranceDestinationCombobox.Enabled == true)
            {

            }

            imageList1.Images.Clear();
            imageList1.Images.AddStrip(loadedImage);

            chestListBox.Items.Clear();
            foreach (Chest c in chest_list)
            {
                if (c.region == dungeonComboBox.SelectedIndex)
                {
                    chestListBox.Items.Add(c);
                }
            }
            chestListBox.DisplayMember = "Name";
            if (chestListBox.Items.Count >= 1)
            {
                chestListBox.SelectedIndex = 1;
                chestListBox.SelectedIndex = 0;
            }


        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) // Load Function
        {

            if (e.Cancel == false)
            {
                if (openFileDialog1.FileName != "")
                {
                    if (File.Exists(openFileDialog1.FileName))
                    {
                        FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                        ROM_DATA = new byte[2097152];
                        fs.Read(ROM_DATA, 0, (int)fs.Length);


                        if (fs.Length < 1100000) //probably the original rom so patch
                        {
                            PatchFile();
                        }
                        fs.Close();
                        {
                            file = openFileDialog1.FileName;

                            dungeonComboBox.SelectedIndex = 1;
                            dungeonComboBox.SelectedIndex = 0;
                            comboBox4.SelectedIndex = getRequiredMedaillon(1);
                            comboBox5.SelectedIndex = getRequiredMedaillon(0);
                            getfountain();
                            loadEntrances();
                            loadUncleText();
                            loadZoraText();
                            loadInitialStuff();
                            loadMagicValues();
                            this.Text = file;

                            for (int i = 0; i < SpritesData.spritesNames.Length; i++)
                            {
                                spritesListbox.Items.Add(SpritesData.spritesNames[i]);
                            }

                            dmg_sword1_updown.Value = ROM_DATA[0x6B8FA];
                            dmg_sword2_updown.Value = ROM_DATA[0x6B902];
                            dmg_sword3_updown.Value = ROM_DATA[0x6B90A];
                            dmg_sword4_updown.Value = ROM_DATA[0x6B912];

                            //Pendants
                            comboBox14.SelectedIndex = ROM_DATA[0x048B7D] - (0x37); //eastern
                            comboBox13.SelectedIndex = ROM_DATA[0x048B7D + 1] - (0x37);//desert
                            comboBox9.SelectedIndex = ROM_DATA[0x048B7D + 2] - (0x37);//hera


                            dungeonCrystal[0] = (byte)Array.IndexOf(crystalMap, ROM_DATA[0x5452D]);  //pod
                            dungeonCrystal[1] = (byte)Array.IndexOf(crystalMap, ROM_DATA[0x54527]);
                            dungeonCrystal[2] = (byte)Array.IndexOf(crystalMap, ROM_DATA[0x5452C]);
                            dungeonCrystal[3] = (byte)Array.IndexOf(crystalMap, ROM_DATA[0x5452A]);
                            dungeonCrystal[4] = (byte)Array.IndexOf(crystalMap, ROM_DATA[0x54528]);
                            dungeonCrystal[5] = (byte)Array.IndexOf(crystalMap, ROM_DATA[0x54529]);
                            dungeonCrystal[6] = (byte)Array.IndexOf(crystalMap, ROM_DATA[0x5452B]);


                            if (ROM_DATA[0x372EF] == 0xF0)
                            {
                                ganonhammerCheckbox.Checked = true;
                            }
                            else
                            {
                                ganonhammerCheckbox.Checked = false;
                            }

                            tabControl1.Enabled = true;

                        }

                        loadPalettes();

                    }

                }

            }

        }

        public void loadMagicValues()
        {
            magicnbrRods1.Value = (ROM_DATA[0x3B070 + (0 * 3) + 0]);
            magicnbrRods2.Value = (ROM_DATA[0x3B070 + (0 * 3) + 1]);
            magicnbrRods3.Value = (ROM_DATA[0x3B070 + (0 * 3) + 2]);

            magicnbrMedaillon1.Value = (ROM_DATA[0x3B070 + (1 * 3) + 0]);
            magicnbrMedaillon2.Value = (ROM_DATA[0x3B070 + (1 * 3) + 1]);
            magicnbrMedaillon3.Value = (ROM_DATA[0x3B070 + (1 * 3) + 2]);

            magicnbrPowder1.Value = (ROM_DATA[0x3B070 + (2 * 3) + 0]);
            magicnbrPowder2.Value = (ROM_DATA[0x3B070 + (2 * 3) + 1]);
            magicnbrPowder3.Value = (ROM_DATA[0x3B070 + (2 * 3) + 2]);

            magicnbrSomaria1.Value = (ROM_DATA[0x3B070 + (4 * 3) + 0]);
            magicnbrSomaria2.Value = (ROM_DATA[0x3B070 + (4 * 3) + 1]);
            magicnbrSomaria3.Value = (ROM_DATA[0x3B070 + (4 * 3) + 2]);

            magicnbrLamp1.Value = (ROM_DATA[0x3B070 + (6 * 3) + 0]);
            magicnbrLamp2.Value = (ROM_DATA[0x3B070 + (6 * 3) + 1]);
            magicnbrLamp3.Value = (ROM_DATA[0x3B070 + (6 * 3) + 2]);

            magicnbrByrna1.Value = (ROM_DATA[0x3B070 + (8 * 3) + 0]);
            magicnbrByrna2.Value = (ROM_DATA[0x3B070 + (8 * 3) + 1]);
            magicnbrByrna3.Value = (ROM_DATA[0x3B070 + (8 * 3) + 2]);

            magicnbrByrnaover1.Value = (ROM_DATA[0x45C42 + 0]);
            magicnbrByrnaover2.Value = (ROM_DATA[0x45C42 + 1]);
            magicnbrByrnaover3.Value = (ROM_DATA[0x45C42 + 2]);

            magicnbrCape1.Value = (ROM_DATA[0x3ADA7 + 0]);
            magicnbrCape2.Value = (ROM_DATA[0x3ADA7 + 1]);
            magicnbrCape3.Value = (ROM_DATA[0x3ADA7 + 2]);
        }

        public void saveMagicValues()
        {
            ROM_DATA[0x3B070 + (0 * 3) + 0] = (byte)magicnbrRods1.Value;
            ROM_DATA[0x3B070 + (0 * 3) + 1] = (byte)magicnbrRods2.Value;
            ROM_DATA[0x3B070 + (0 * 3) + 2] = (byte)magicnbrRods3.Value;

            ROM_DATA[0x3B070 + (1 * 3) + 0] = (byte)magicnbrMedaillon1.Value;
            ROM_DATA[0x3B070 + (1 * 3) + 1] = (byte)magicnbrMedaillon2.Value;
            ROM_DATA[0x3B070 + (1 * 3) + 2] = (byte)magicnbrMedaillon3.Value;

            ROM_DATA[0x3B070 + (2 * 3) + 0] = (byte)magicnbrPowder1.Value;
            ROM_DATA[0x3B070 + (2 * 3) + 1] = (byte)magicnbrPowder2.Value;
            ROM_DATA[0x3B070 + (2 * 3) + 2] = (byte)magicnbrPowder3.Value;

            ROM_DATA[0x3B070 + (4 * 3) + 0] = (byte)magicnbrSomaria1.Value;
            ROM_DATA[0x3B070 + (4 * 3) + 1] = (byte)magicnbrSomaria2.Value;
            ROM_DATA[0x3B070 + (4 * 3) + 2] = (byte)magicnbrSomaria3.Value;

            ROM_DATA[0x3B070 + (6 * 3) + 0] = (byte)magicnbrLamp1.Value;
            ROM_DATA[0x3B070 + (6 * 3) + 1] = (byte)magicnbrLamp2.Value;
            ROM_DATA[0x3B070 + (6 * 3) + 2] = (byte)magicnbrLamp3.Value;

            ROM_DATA[0x3B070 + (8 * 3) + 0] = (byte)magicnbrByrna1.Value;
            ROM_DATA[0x3B070 + (8 * 3) + 1] = (byte)magicnbrByrna2.Value;
            ROM_DATA[0x3B070 + (8 * 3) + 2] = (byte)magicnbrByrna3.Value;

            ROM_DATA[0x45C42 + 0] = (byte)magicnbrByrnaover1.Value;
            ROM_DATA[0x45C42 + 1] = (byte)magicnbrByrnaover2.Value;
            ROM_DATA[0x45C42 + 2] = (byte)magicnbrByrnaover3.Value;

            ROM_DATA[0x3ADA7 + 0] = (byte)magicnbrCape1.Value;
            ROM_DATA[0x3ADA7 + 1] = (byte)magicnbrCape2.Value;
            ROM_DATA[0x3ADA7 + 2] = (byte)magicnbrCape3.Value;
            if (hudupdateCheckbox.Checked == true)
                ROM_DATA[0x180030] = 1;
            else
                ROM_DATA[0x180030] = 0;
        }

        public void CreatePatchFile()
        {

            FileStream fs = new FileStream("rnd.sfc", FileMode.Open, FileAccess.Read);
            byte[] DATA = new byte[2097152];
            fs.Read(DATA, 0, (int)fs.Length);
            fs.Close();
            byte[] d = new byte[1000000];
            Zdata[] zd = new Zdata[1000000];
            //List<Zdata> zdata = new List<Zdata>();
            int zdpos = 0;
            int dpos = 0;
            int size = -1;
            int offset = 0;
            List<byte> arr = new List<byte>();

            //BinaryReader br = new BinaryReader(new FileStream("patch.zpf", FileMode.Open, FileAccess.Read));

            for (int i = 0; i < DATA.Length; i++)
            {
                if (DATA[i] != ROM_DATA[i])
                {
                    if (size == -1)
                    {
                        offset = i;
                        size = 1;
                        arr.Clear();
                    }
                    arr.Add(DATA[i]);
                }
                else
                {
                    if (size != -1)
                    {
                        zd[zdpos] = (new Zdata(offset, arr.ToArray()));
                        zdpos++;
                        size = -1;
                    }
                }

            }

            BinaryWriter bw = new BinaryWriter(new FileStream("patch.zpf", FileMode.OpenOrCreate, FileAccess.Write));
            int le = 0;

            for (int i = 0; i < zd.Length; i++)
            {
                if (zd[i] != null)
                {
                    le++;
                }
            }
            bw.Write((int)le);
            for (int i = 0; i < zd.Length; i++)
            {
                if (zd[i] != null)
                {
                    bw.Write((int)zd[i].offset);
                    bw.Write((short)zd[i].array.Length);
                    for (int j = 0; j < (zd[i].array.Length); j++)
                    {
                        bw.Write(zd[i].array[j]);
                    }
                }
            }

            bw.Close();

        }

        public void PatchFile()
        {
            List<Zdata> zdata = new List<Zdata>();
            BinaryReader br = new BinaryReader(new FileStream("patch.zpf", FileMode.Open, FileAccess.Read));
            int datacount = br.ReadInt32();
            for (int i = 0; i < datacount; i++)
            {
                int offset = br.ReadInt32();
                short arraysize = br.ReadInt16();
                byte[] array = new byte[arraysize];
                for (int j = 0; j < (arraysize); j++)
                {
                    array[j] = br.ReadByte();
                }
                zdata.Add(new Zdata(offset, array));
            }
            br.Close();

            foreach (Zdata z in zdata)
            {

                for (int i = 0; i < z.array.Length; i++)
                {
                    ROM_DATA[z.offset + i] = z.array[i];
                }
            }
        }

        public void getfountain()
        {
            byte v = ROM_DATA[0x3493B];
            if (v == 0x2b) { comboBox6.SelectedIndex = 0; }
            if (v == 0x2c) { comboBox6.SelectedIndex = 1; }
            if (v == 0x2d) { comboBox6.SelectedIndex = 2; }
            if (v == 0x3c) { comboBox6.SelectedIndex = 3; }
            if (v == 0x3d) { comboBox6.SelectedIndex = 5; }
            if (v == 0x48) { comboBox6.SelectedIndex = 4; }
            v = ROM_DATA[0x348FF];
            if (v == 0x2b) { comboBox7.SelectedIndex = 0; }
            if (v == 0x2c) { comboBox7.SelectedIndex = 1; }
            if (v == 0x2d) { comboBox7.SelectedIndex = 2; }
            if (v == 0x3c) { comboBox7.SelectedIndex = 3; }
            if (v == 0x3d) { comboBox7.SelectedIndex = 5; }
            if (v == 0x48) { comboBox7.SelectedIndex = 4; }
        }

        public int getRequiredMedaillon(byte dung)
        {
            //Mire dung = 0, Trock dung = 1
            if (ROM_DATA[0x180022 + dung] == 0x00)
            {
                //Bombos
                return 0;
            }
            if (ROM_DATA[0x180022 + dung] == 0x01)
            {
                //Ether
                return 1;
            }
            if (ROM_DATA[0x180022 + dung] == 0x02)
            {
                //Quake
                return 2;
            }
            if (ROM_DATA[0x180022 + dung] == 0x03)
            {
                //Quake
                return 3;
            }
            return 0;
        }

        public void save_medaillons()
        {
            if (comboBox5.SelectedIndex == 0)//bombos
            {
                //MIRE BOMBOS
                ROM_DATA[0x4FF2] = 0x31;
                ROM_DATA[0x50D1] = 0x80;
                ROM_DATA[0x51B0] = 0x00;
                ROM_DATA[0x180022] = 0x00;
            }
            if (comboBox5.SelectedIndex == 1)//ether
            {
                //MIRE ETHER
                ROM_DATA[0x4FF2] = 0x13;
                ROM_DATA[0x50D1] = 0x9F;
                ROM_DATA[0x51B0] = 0xF1;
                ROM_DATA[0x180022] = 0x01;
            }
            if (comboBox5.SelectedIndex == 2)//quake
            {
                //MIRE QUAKE
                ROM_DATA[0x4FF2] = 0x31;
                ROM_DATA[0x50D1] = 0x88;
                ROM_DATA[0x51B0] = 0x00;
                ROM_DATA[0x180022] = 0x02;
            }
            if (comboBox5.SelectedIndex == 3)//blank
            {
                //MIRE QUAKE
                ROM_DATA[0x4FF2] = 0x31;
                ROM_DATA[0x50D1] = 0xB4;
                ROM_DATA[0x51B0] = 0x03;
                ROM_DATA[0x180022] = 0x03;
            }
            if (comboBox4.SelectedIndex == 0)//bombos
            {
                //TROCK BOMBOS
                ROM_DATA[0x5020] = 0x31;
                ROM_DATA[0x50FF] = 0x90;
                ROM_DATA[0x51DE] = 0x00;
                ROM_DATA[0x180023] = 0x00;
            }
            if (comboBox4.SelectedIndex == 1)//ether
            {
                //TROCK ETHER
                ROM_DATA[0x5020] = 0x31;
                ROM_DATA[0x50FF] = 0x98;
                ROM_DATA[0x51DE] = 0x00;
                ROM_DATA[0x180023] = 0x01;
            }
            if (comboBox4.SelectedIndex == 2)//quake
            {
                //TROCK QUAKE
                ROM_DATA[0x5020] = 0x14;
                ROM_DATA[0x50FF] = 0xEF;
                ROM_DATA[0x51DE] = 0xC4;
                ROM_DATA[0x180023] = 0x02;
            }
            if (comboBox4.SelectedIndex == 3)//blank
            {
                //TROCK QUAKE 31C03B
                ROM_DATA[0x5020] = 0x31;
                ROM_DATA[0x50FF] = 0xC0;
                ROM_DATA[0x51DE] = 0x3B;
                ROM_DATA[0x180023] = 0x03;
            }

            //100EA6 added code here 1D0000:3A8000
            byte[] mireblank = new byte[] { 0xAF, 0xF0, 0xF2, 0x7E, 0x09, 0x20, 0x8F, 0xF0, 0xF2, 0x7E };
            byte[] trockblank = new byte[] { 0xAF, 0xC7, 0xF2, 0x7E, 0x09, 0x20, 0x8F, 0xC7, 0xF2, 0x7E };
            //AFC7F27E09208FC7F27E
            //8FC7F37E
            ROM_DATA[0x1D0000] = 0x8F;
            ROM_DATA[0x1D0000 + 1] = 0xC7;
            ROM_DATA[0x1D0000 + 2] = 0xF3;
            ROM_DATA[0x1D0000 + 3] = 0x7E;
            //IF NONE SET
            int pos = 4;
            if (comboBox4.SelectedIndex != 3 && comboBox5.SelectedIndex != 3)
            {
                ROM_DATA[0x1D0000 + 4] = 0x6B;
            }
            else
            {
                if (comboBox5.SelectedIndex == 3)
                {
                    //mire
                    for (int i = 0; i < mireblank.Length; i++)
                    {
                        ROM_DATA[0x1D0000 + pos] = mireblank[i];
                        pos++;
                    }
                }
                if (comboBox4.SelectedIndex == 3)
                {
                    for (int i = 0; i < trockblank.Length; i++)
                    {
                        ROM_DATA[0x1D0000 + pos] = trockblank[i];
                        pos++;
                    }
                }
                ROM_DATA[0x1D0000 + pos] = 0x6B;
            }

            //A9 80 8D 04 02
            //AFF0F27E09208FF0F27E
            //AFC7F27E09208FC7F27E
            //6B
            //64f35 added code (jump)
            //22208E95EA


            ROM_DATA[0x2DD9A] = 0x22;
            ROM_DATA[0x2DD9A + 1] = 0x00;
            ROM_DATA[0x2DD9A + 2] = 0x80;
            ROM_DATA[0x2DD9A + 3] = 0x3A;
            //22958E20EA

            FileStream fs = new FileStream("99ff1_blank.gfx", FileMode.Open, FileAccess.Read);
            Console.WriteLine(fs.Length);
            for (int i = 0; i < fs.Length; i++)
            {
                ROM_DATA[0x18B403 + i] = (byte)fs.ReadByte();

            }
            fs.Close();
            fs = new FileStream("a6fc4_blank.gfx", FileMode.Open, FileAccess.Read);
            for (int i = 0; i < fs.Length; i++)
            {
                ROM_DATA[0x18C03B + i] = (byte)fs.ReadByte();
            }
            fs.Close();
        }

        public void save_fairy()
        {
            byte v = 0x00;
            if (comboBox6.SelectedIndex == 0) { v = 0x2b; }
            if (comboBox6.SelectedIndex == 1) { v = 0x2c; }
            if (comboBox6.SelectedIndex == 2) { v = 0x2d; }
            if (comboBox6.SelectedIndex == 3) { v = 0x3c; }
            if (comboBox6.SelectedIndex == 4) { v = 0x48; }
            if (comboBox6.SelectedIndex == 5) { v = 0x3d; }
            ROM_DATA[0x3493B] = v;
            v = 0x00;
            if (comboBox7.SelectedIndex == 0) { v = 0x2b; }
            if (comboBox7.SelectedIndex == 1) { v = 0x2c; }
            if (comboBox7.SelectedIndex == 2) { v = 0x2d; }
            if (comboBox7.SelectedIndex == 3) { v = 0x3c; }
            if (comboBox7.SelectedIndex == 4) { v = 0x48; }
            if (comboBox7.SelectedIndex == 5) { v = 0x3d; }
            ROM_DATA[0x348FF] = v;

        }

        public void wizz_save()
        {
            if (wizzbox.Checked == false)
            {


                ROM_DATA[0x03A943] = 0xD0;//Normal Mirror
                ROM_DATA[0x03A3F4] = 0xD0; ROM_DATA[0x03A3F5] = 0xD4;//Normal Flute
                ROM_DATA[0x045F5F] = 0x22; ROM_DATA[0x045F60] = 0xE8; ROM_DATA[0x045F61] = 0x9E; ROM_DATA[0x045F62] = 0x09;//Flute Hook Removed
                ROM_DATA[0x03A96D] = 0xF0;//Portal appear when you warp



            }
            else
            {

                byte[] fluteext = new byte[] { 0x22,0xE8,0x9E,0x09,0xAD,0xFF,0x0F,0x80,0x06,0x6B,0xF0,0x1C,0xEA,0xEA,0xEA,0xEA,0xA9,0x23,0x85,
0x11,0x9C,0xF8,0x03,0xA9,0x01,0x8D,0xDB,0x02,0x64,0xB0,0x64,0x27,0x64,0x28,0xA9,0x14,0x85,0x5D,
0xEA,0xEA,0x6B,0xEA,0xEA,0xD0,0xE1,0xA5,0xF6,0x29,0x20,0xF0,0x06,0xA9,0x0C,0x8D,0x2C,0x01,0xEA,
0xA5,0xF6,0x29,0x10,0xF0,0x06,0xA9,0x0A,0x8D,0x2C,0x01,0xEA,0x6B};
                for (int i = 0; i < fluteext.Length; i++)
                {
                    ROM_DATA[0x1D0026 + i] = fluteext[i];
                }
                //22E89E09
                ROM_DATA[0x045F5F] = 0x22; ROM_DATA[0x045F60] = 0x26; ROM_DATA[0x045F61] = 0x80; ROM_DATA[0x045F62] = 0x3A;//Flute Hook
                if (radioBothFlute.Checked)
                {
                    ROM_DATA[0x03A3F4] = 0xEA; ROM_DATA[0x03A3F5] = 0xEA;//Normal Flute
                    ROM_DATA[0x1D0026 + 7] = 0xD0;
                }
                else if (radioDWFlute.Checked)
                {
                    ROM_DATA[0x03A3F4] = 0xF0; ROM_DATA[0x03A3F5] = 0xD4;//Normal Flute
                    ROM_DATA[0x1D0026 + 7] = 0xD0;
                }
                else if (radioLWFlute.Checked)
                {
                    //22E89E09
                    ROM_DATA[0x045F5F] = 0x22; ROM_DATA[0x045F60] = 0xE8; ROM_DATA[0x045F61] = 0x9E; ROM_DATA[0x045F62] = 0x09;//don't use hook
                    ROM_DATA[0x03A3F4] = 0xD0; ROM_DATA[0x03A3F5] = 0xD4;//Normal Flute
                }

                if (radioBothMirror.Checked)
                {
                    ROM_DATA[0x03A943] = 0x80;//Normal Mirror
                }
                else if (radioDWMirror.Checked)
                {
                    ROM_DATA[0x03A943] = 0xD0;//Normal Mirror
                }
                else if (radioLWMirror.Checked)
                {
                    ROM_DATA[0x03A943] = 0xF0;//Normal Mirror
                }
                if (checkBoxPortal.Checked == false)
                {
                    ROM_DATA[0x03A96D] = 0xF0;//Mirror appear when you warp
                }
                else
                {
                    ROM_DATA[0x03A96D] = 0x80;//Mirror do not appear when you warp
                }

            }
        }

        public void saveChests()
        {
            foreach (Chest c in chest_list)
            {
                if (c.address == 0x48B7C)
                {
                    ROM_DATA[0x44AA9] = ROM_DATA[c.address];
                }
                if (c.address == 0x48B81)
                {
                    ROM_DATA[0x44AAE] = ROM_DATA[c.address];
                }
                if (c.address == 0x48B7C)
                {
                    ROM_DATA[0x44AA9] = ROM_DATA[c.address];
                }
                if (c.address == 0x48B81)
                {
                    ROM_DATA[0x44AAE] = ROM_DATA[c.address];
                }
            }
        }

        public void saveMisc()
        {
            //Ganon hammer
            if (ganonhammerCheckbox.Checked)
            {
                ROM_DATA[0x372EF] = 0xF0;
                ROM_DATA[0x372F0] = 0x29;
            }
            else
            {
                ROM_DATA[0x372EF] = 0xB0;
                ROM_DATA[0x372F0] = 0x36;
            }
            //bee spawn code : 
            byte[] bee = new byte[]
            {
    0xC9, 0x55, 0xD0, 0x39, 0xEA, 0xEA, 0xEA, 0xEA, 0xEA, 0xDA, 0x5A, 0xA9, 0x79, 0x22, 0x5D, 0xF6,
    0x1D, 0x30, 0x25, 0xA5, 0xEE, 0x99, 0x20, 0x0F, 0xA5, 0x22, 0x18, 0x69, 0x08, 0x99, 0x10, 0x0D,
    0xA5, 0x23, 0x18, 0x69, 0x00, 0x99, 0x30, 0x0D, 0xA5, 0x20, 0x18, 0x69, 0x10, 0x99, 0x00, 0x0D,
    0xA5, 0x21, 0x18, 0x69, 0x00, 0x99, 0x20, 0x0D, 0x7A, 0xFA, 0x82, 0x00, 0x00, 0xAD, 0xE9, 0x02,
    0xC9, 0x01, 0x6B, };
            for (int i = 0; i < bee.Length; i++)
            {
                ROM_DATA[0x1D010C+i] = bee[i];
            }

            ROM_DATA[0x101BEC] = 0x22;
            ROM_DATA[0x101BEC+1] = 0x0C;
            ROM_DATA[0x101BEC+2] = 0x81;
            ROM_DATA[0x101BEC+3] = 0x3A;
            ROM_DATA[0x101BEC+4] = 0xEA;
        }

        private void savePalettes()
        {
            for (int i = 0; i < palettes.Length; i++)
            {
                if (palettes[i] != null)
                {
                    palettes[i].save();
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            save();
        }

        private void saveZoraText()
        {
            flippersTextbox.Text = flippersTextbox.Text.ToLower();
            for (int i = 0; i < 20; i++)
            {
                if (i < flippersTextbox.Text.Length)
                {
                    byte b = (byte)(flippersTextbox.Text[i] - 0x47);
                    if (flippersTextbox.Text[i] == ' ')
                    {
                        b = 0x9F;
                    }
                    ROM_DATA[0x76A85 + i] = b;
                }
                else
                {

                    ROM_DATA[0x76A85 + i] = 0x9F;
                }

            }
        }

        private void saveEntrance()
        {
            for (int i = 0; i < 0x4F; i++)
            {
                ROM_DATA[0x15AEE + (i * 2)] = exit_data[(new_exit_data[i] * 2)];
                ROM_DATA[0x15AEE + (i * 2)+1] = exit_data[(new_exit_data[i] * 2)+1];
            }
            ROM_DATA[0x1D7FFF] = 1;
            for (int i = 0; i < 0x4F; i++)
            {
                ROM_DATA[0x1D8000 + i] = new_exit_data[i];
            }
        }

        public void save()
        {
            save_fairy();
            save_medaillons();
            saveEntrance();
            saveChests();
            saveUncleText();
            saveZoraText();
            wizz_save();
            saveMisc();
            saveInitialStuff();
            savePalettes();
            saveMagicValues();
            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Write);
            fs.Write(ROM_DATA, 0, 2097152);
            fs.Close();
        }

        private void loadPendantsAndCrystals()
        {
            ROM_DATA[0x048B7D] = (byte)(0x37 + comboBox14.SelectedIndex); //
            ROM_DATA[0x048B7D + 1] = (byte)(0x37 + comboBox13.SelectedIndex); //
            ROM_DATA[0x048B7D + 2] = (byte)(0x37 + comboBox9.SelectedIndex); //

            ROM_DATA[0x053EF8] = pendantsMap[comboBox14.SelectedIndex];
            ROM_DATA[0x053F1C] = pendantsMap[comboBox13.SelectedIndex];
            ROM_DATA[0x053F0A] = pendantsMap[comboBox9.SelectedIndex];

            ROM_DATA[0x01209C] = pendantsB[comboBox14.SelectedIndex];//
            ROM_DATA[0x01209D] = pendantsB[comboBox13.SelectedIndex];//
            ROM_DATA[0x0120A5] = pendantsB[comboBox9.SelectedIndex];//

            ROM_DATA[0x5452D] = crystalMap[dungeonCrystal[0]];  //pod
            ROM_DATA[0x54527] = crystalMap[dungeonCrystal[1]];  //swamp
            ROM_DATA[0x5452C] = crystalMap[dungeonCrystal[2]];  //skull
            ROM_DATA[0x5452A] = crystalMap[dungeonCrystal[3]];  //thieve
            ROM_DATA[0x54528] = crystalMap[dungeonCrystal[4]];  //ice
            ROM_DATA[0x54529] = crystalMap[dungeonCrystal[5]];  //mire
            ROM_DATA[0x5452B] = crystalMap[dungeonCrystal[6]];  //trock


            dungeonCrystalX = new byte[] { 0x02, 0x10, 0x40, 0x20, 0x04, 0x01, 0x08 };
            ROM_DATA[0x545D1 + 0] = dungeonCrystalX[dungeonCrystal[0]];
            ROM_DATA[0x545D1 + 1] = dungeonCrystalX[dungeonCrystal[2]];
            ROM_DATA[0x545D1 + 2] = dungeonCrystalX[dungeonCrystal[6]];
            ROM_DATA[0x545D1 + 3] = dungeonCrystalX[dungeonCrystal[3]];
            ROM_DATA[0x545D1 + 4] = dungeonCrystalX[dungeonCrystal[5]];
            ROM_DATA[0x545D1 + 5] = dungeonCrystalX[dungeonCrystal[4]];
            ROM_DATA[0x545D1 + 6] = dungeonCrystalX[dungeonCrystal[1]];

            ROM_DATA[0x120A0 + 0] = dungeonCrystalX[dungeonCrystal[1]];
            ROM_DATA[0x120A0 + 1] = dungeonCrystalX[dungeonCrystal[0]];
            ROM_DATA[0x120A0 + 2] = dungeonCrystalX[dungeonCrystal[5]];
            ROM_DATA[0x120A0 + 3] = dungeonCrystalX[dungeonCrystal[2]];
            ROM_DATA[0x120A0 + 4] = dungeonCrystalX[dungeonCrystal[4]];
            ROM_DATA[0x120A0 + 6] = dungeonCrystalX[dungeonCrystal[3]];
            ROM_DATA[0x120A0 + 7] = dungeonCrystalX[dungeonCrystal[6]];

        }

        private void loadZoraText()
        {
            string text = "";

            for (int i = 0; i < 20; i++)
            {

                byte b = (byte)(ROM_DATA[0x76A85 + i]);
                if (b == 0x9F)
                {
                    b = (byte)' ';
                }
                else
                {
                    b += 0x47;
                }
                text += (char)b;
            }
            flippersTextbox.Text = text;
        }
        byte[] pendantsB = new byte[] { 0x38, 0x32, 0x34 };//G R B
        byte[] pendantsMap = new byte[] { 0x4, 0x2, 0x0 };//G B R
        byte[] crystalMap = new byte[] { 0x7F, 0x79, 0x6C, 0x6D, 0x6E, 0x6F, 0x7C };
        byte[] dungeonCrystalO = new byte[] { 0x02, 0x10, 0x40, 0x20, 0x04, 0x01, 0x08 };

        byte[] dungeonCrystalX;
        byte[] dungeonCrystal = new byte[7];

        private void saveUncleText()
        {
            int pos = 0;

            pos = 0x1022A9;
            int pos2 = 0xE3211;
            //ROM_DATA[pos] = 0x74;
            string text = "";
            string text2 = "";
            for (int i = 0; i < 14; i++)
            {
                if (i < uncleTextbox1.Text.Length)
                {
                    text += uncleTextbox1.Text[i];
                }
                else
                {
                    text += " ";
                }

                if (i < raceladyTextbox1.Text.Length)
                {
                    text2 += raceladyTextbox1.Text[i];
                }
                else
                {
                    text2 += " ";
                }
            }
            for (int i = 0; i < 14; i++)
            {
                if (i < uncleTextbox2.Text.Length)
                {
                    text += uncleTextbox2.Text[i];
                }
                else
                {
                    text += " ";
                }
                if (i < raceladyTextbox2.Text.Length)
                {
                    text2 += raceladyTextbox2.Text[i];
                }
                else
                {
                    text2 += " ";
                }
            }
            for (int i = 0; i < 14; i++)
            {
                if (i < uncleTextbox3.Text.Length)
                {
                    text += uncleTextbox3.Text[i];
                }
                else
                {
                    text += " ";
                }
                if (i < raceladyTextbox3.Text.Length)
                {
                    text2 += raceladyTextbox3.Text[i];
                }
                else
                {
                    text2 += " ";
                }
            }
            //pos += 1;
            for (int i = 0; i < 42; i++)
            {
                ROM_DATA[pos] = 0;
                byte b = Convert.ToByte((text.ToUpper()[i]));
                if (b >= 48 && b <= 58)
                {
                    b += 112;
                }
                if (b >= 65 && b <= 90)
                {
                    b += 105;
                }
                if (b == 0x20) { b = 0xff; }
                pos += 1;
                ROM_DATA[pos] = b;
                pos += 1;


                if (i <= 39)
                {
                    b = Convert.ToByte((text2.ToUpper()[i]));
                    if (b >= 48 && b <= 58)
                    {
                        b += 112;
                    }
                    if (b >= 65 && b <= 90)
                    {
                        b += 105;
                    }
                    if (b == 0x20) { b = 0xff; }
                    pos2 += 1;
                    ROM_DATA[pos2] = b;
                }
            }
            pos2 = 0xE3211;
            ROM_DATA[pos2 + 40] = 0xFE;
            ROM_DATA[pos2 + 41] = 0x7F;

            pos = 0x1022A9 + 84;
            ROM_DATA[pos] = 0x7F;
            pos++;
            ROM_DATA[pos] = 0x7F;
        }

        private void loadUncleText()
        {
            string text = "";
            int pos = 0x1022A9 + 1;
            uncleTextbox1.Text = "";
            uncleTextbox2.Text = "";
            uncleTextbox3.Text = "";
            for (int i = 0; i < 42; i++)
            {
                byte b = ROM_DATA[pos];
                if (b >= 160 && b < 170)
                {
                    b -= 112;
                }
                if (b >= 170 && b <= 195)
                {
                    b -= 105;
                }
                if (b == 0xff) { b = 0x20; }
                text += (char)b;
                pos += 2;
            }
            for (int i = 0; i < 14; i++)
            {
                uncleTextbox1.Text += text[i];
                uncleTextbox2.Text += text[i + 14];
                uncleTextbox3.Text += text[i + 28];

            }

        }

        DungeonData[] dungeonsData = new DungeonData[14];
        private void createDungeonsInfos()
        {
            dungeonsData[0] = new DungeonData(15); //sewer
            dungeonsData[1] = new DungeonData(14); //hyrule castle
            dungeonsData[2] = new DungeonData(13); //eastern
            dungeonsData[3] = new DungeonData(12); // desert
            dungeonsData[4] = new DungeonData(5); // hera
            dungeonsData[5] = new DungeonData(11); //hyrule castle2
            dungeonsData[6] = new DungeonData(9); // pod
            dungeonsData[7] = new DungeonData(10); //swamp
            dungeonsData[8] = new DungeonData(7); //sw
            dungeonsData[9] = new DungeonData(4); //tt
            dungeonsData[10] = new DungeonData(6); //ice
            dungeonsData[11] = new DungeonData(8); //mm
            dungeonsData[12] = new DungeonData(3); //trock
            dungeonsData[13] = new DungeonData(2); //gtower
        }

        private void saveInitialStuff()
        {
            for (int i = 0; i < 35; i++)
            {
                ROM_DATA[0x271A6 + i] = 0;
            }

            if (equipBowCheckbox.Checked)
            {
                ROM_DATA[0x271A6] = 1;
            }
            if (equipSilverarrowCheckBox.Checked)
            {
                ROM_DATA[0x271A6] = 3;
            }
            if (equipBoomerangCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 1] = 1;
            }
            if (equipBoomerangredCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 1] = 2;
            }
            if (equipHookshotCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 2] = 1;
            }
            if (equipBombsCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 3] = 10;
            }
            if (equipMushroomCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 4] = 1;
            }
            if (equipPowderCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 4] = 2;
            }
            if (equipFirerodCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 5] = 1;
            }
            if (equipIcerodCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 6] = 1;
            }
            if (equipBombosCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 7] = 1;
            }
            if (equipEtherCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 8] = 1;
            }
            if (equipQuakeCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 9] = 1;
            }
            if (equipLanternCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 10] = 1;
            }
            if (equipHammerCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 11] = 1;
            }
            if (equipShovelCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 12] = 1;
            }
            if (equipFluteCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 12] = 2;
            }
            if (equipFluteactiveCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 12] = 3;
            }
            if (equipNetCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 13] = 1;
            }
            if (equipBookCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 14] = 1;
            }

            if (equipSomariaCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 16] = 1;
            }
            if (equipByrnaCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 17] = 1;
            }
            if (equipCapeCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 18] = 1;
            }
            if (equipMirrorCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 19] = 1;
            }

            if (equipBootsCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 21] = 1;
            }
            if (equipFlippersCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 22] = 1;
            }
            if (equipMoonpearlCheckbox.Checked)
            {
                ROM_DATA[0x271A6 + 23] = 1;
            }
            ROM_DATA[0x6DB48] = (byte)maxbombs_updown.Value;
            ROM_DATA[0x6DB4F] = (byte)maxbombsUp_updown.Value;
            ROM_DATA[0x6DB58] = (byte)maxarrow_updown.Value;
            ROM_DATA[0x6DB5F] = (byte)maxarrowUp_updown.Value;
            ROM_DATA[0x6CCC0] = (byte)racetimer_updown.Value;

            ROM_DATA[0x271A6 + 20] = (byte)(equipGlovescomboBox.SelectedIndex);
            ROM_DATA[0x271A6 + 25] = (byte)(equipSwordcomboBox.SelectedIndex);
            ROM_DATA[0x271A6 + 26] = (byte)(equipShieldcomboBox.SelectedIndex);
            ROM_DATA[0x271A6 + 27] = (byte)(equipMailcomboBox.SelectedIndex);
            ROM_DATA[0x271A6 + 28] = (byte)(equipBottle1Combobox.SelectedIndex);
            ROM_DATA[0x271A6 + 29] = (byte)(equipBottle2Combobox.SelectedIndex);
            ROM_DATA[0x271A6 + 30] = (byte)(equipBottle3Combobox.SelectedIndex);
            ROM_DATA[0x271A6 + 31] = (byte)(equipBottle4Combobox.SelectedIndex);
            if (equipBottle1Combobox.SelectedIndex != 0 || equipBottle2Combobox.SelectedIndex != 0 ||
                equipBottle3Combobox.SelectedIndex != 0 || equipBottle4Combobox.SelectedIndex != 0)
            {
                ROM_DATA[0x271A6 + 15] = 1;
            }
            else
            {
                ROM_DATA[0x271A6 + 15] = 0;
            }

            byte[] rupeec = new byte[2];
            BitConverter.ToInt16(rupeec, 0);
            rupeec = BitConverter.GetBytes((int)rupeeNumericupdown.Value);
            //ROM_DATA[0x271A6 + 32] = rupeec[0]; //30, 31
            //ROM_DATA[0x271A6 + 33] = rupeec[1]; //30, 31
            //31,30,29,28
            ROM_DATA[0x271A6 + 32] = rupeec[0]; //30, 31
            ROM_DATA[0x271A6 + 33] = rupeec[1]; //30, 31
            ROM_DATA[0x271A6 + 34] = rupeec[0]; //30, 31
            ROM_DATA[0x271A6 + 35] = rupeec[1]; //30, 31
            //h = hera, d = desert, e = eastern, s = sewers, c = castle, a = agah tower, - unused, nbr crystal order

            byte Data1 = 0;
            byte Data2 = 0;

            for (int i = 0; i < 13; i++)
            {
                if (dungeonsData[i].compass == true)
                {
                    if (dungeonsData[i].dungeonPos < 8)
                    {
                        setBit(ref Data1, dungeonsData[i].dungeonPos);
                    }
                    else
                    {
                        setBit(ref Data2, (dungeonsData[i].dungeonPos - 8));
                    }
                }
            }
            ROM_DATA[0x271A6 + 36] = (byte)(Data1); //Compass [35h478--]
            ROM_DATA[0x271A6 + 37] = (byte)(Data2); //Compass [sceda216]


            Data1 = 0;
            Data2 = 0;

            for (int i = 0; i < 13; i++)
            {
                if (dungeonsData[i].bigKey == true)
                {
                    if (dungeonsData[i].dungeonPos < 8)
                    {
                        setBit(ref Data1, dungeonsData[i].dungeonPos);
                    }
                    else
                    {
                        setBit(ref Data2, (dungeonsData[i].dungeonPos - 8));
                    }
                }
            }

            ROM_DATA[0x271A6 + 38] = (byte)(Data1); //Big Key [35h478--]
            ROM_DATA[0x271A6 + 39] = (byte)(Data2); //Big Key [sceda216]

            Data1 = 0;
            Data2 = 0;

            for (int i = 0; i < 13; i++)
            {
                if (dungeonsData[i].map == true)
                {
                    if (dungeonsData[i].dungeonPos < 8)
                    {
                        setBit(ref Data1, dungeonsData[i].dungeonPos);
                    }
                    else
                    {
                        setBit(ref Data2, (dungeonsData[i].dungeonPos - 8));
                    }
                }
            }

            ROM_DATA[0x271A6 + 40] = (byte)(Data1); //Map [35h478--]
            ROM_DATA[0x271A6 + 41] = (byte)(Data2); //Map [sceda216]

            Data1 = 0;
            Data2 = 0;

            for (int i = 0; i < 13; i++)
            {
                if (dungeonsData[i].map == true)
                {

                }
            }

            ROM_DATA[0x271A6 + 44] = (byte)(startingheart_updown.Value * 8);
            ROM_DATA[0x271A6 + 45] = (byte)(startingheart_updown.Value * 8);
            if (checkBox22.Checked)
            {
                ROM_DATA[0x271A6 + 47] = 50; //Big Key [sceda216]
            }

            Data1 = 0;
            if (dungeonsData[2].crystal == true)//e
            {
                setBit(ref Data1, 2);
            }

            if (dungeonsData[3].crystal == true)//d
            {
                setBit(ref Data1, 1);
            }

            if (dungeonsData[4].crystal == true)//h
            {
                setBit(ref Data1, 0);
            }

            ROM_DATA[0x271A6 + 52] = Data1;

            Data1 = 0;
            //11,6,10,12,7,9,8
            if (dungeonsData[11].crystal == true)//e
            {
                setBit(ref Data1, 0);
            }

            if (dungeonsData[6].crystal == true)//d
            {
                setBit(ref Data1, 1);
            }

            if (dungeonsData[10].crystal == true)//h
            {
                setBit(ref Data1, 2);
            }

            if (dungeonsData[12].crystal == true)//h
            {
                setBit(ref Data1, 3);
            }
            if (dungeonsData[7].crystal == true)//h
            {
                setBit(ref Data1, 4);
            }
            if (dungeonsData[9].crystal == true)//h
            {
                setBit(ref Data1, 5);
            }
            if (dungeonsData[8].crystal == true)//h
            {
                setBit(ref Data1, 6);
            }
            ROM_DATA[0x271A6 + 58] = Data1;
            Data1 = 0xF8;
            if (equipBootsCheckbox.Checked)
            {
                setBit(ref Data1, 2);
            }
            if (equipFlippersCheckbox.Checked)
            {
                setBit(ref Data1, 1);
            }
            ROM_DATA[0x271A6 + 57] = Data1;
            //57 = ability [F8] if boots set bit 2, flippter bit1



        }

        public void setBit(ref byte b, int pos)
        {
            int[] positions = new int[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
            b = (byte)(b | positions[pos]);
        }

        public bool getBit(byte b, byte pos)
        {
            int[] positions = new int[] { 0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80 };
            if ((b & (byte)(positions[pos])) == (byte)positions[pos])
            {
                return true;
            }
            return false;
        }

        private void loadInitialStuff()
        {
            foreach (Control c in groupBox5.Controls)
            {
                if (c.GetType() == typeof(CheckBox))
                {
                    (c as CheckBox).Checked = false;
                }
            }

            if (ROM_DATA[0x271A6] == 1)
            {
                equipBowCheckbox.Checked = true;
            }

            if (ROM_DATA[0x271A6] == 3)
            {
                equipSilverarrowCheckBox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 1] == 1)
            {
                equipBoomerangCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 1] == 2)
            {
                equipBoomerangredCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 2] == 1)
            {
                equipHookshotCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 3] == 10)
            {
                equipBombsCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 4] == 1)
            {
                equipMushroomCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 4] == 2)
            {
                equipPowderCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 5] == 1)
            {
                equipFirerodCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 6] == 1)
            {
                equipIcerodCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 7] == 1)
            {
                equipBombosCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 8] == 1)
            {
                equipEtherCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 9] == 1)
            {
                equipQuakeCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 10] == 1)
            {
                equipLanternCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 11] == 1)
            {
                equipHammerCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 12] == 1)
            {
                equipShovelCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 12] == 2)
            {
                equipFluteCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 12] == 3)
            {
                equipFluteactiveCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 13] == 1)
            {
                equipNetCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 14] == 1)
            {
                equipBookCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 16] == 1)
            {
                equipSomariaCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 17] == 1)
            {
                equipByrnaCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 18] == 1)
            {
                equipCapeCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 19] == 1)
            {
                equipMirrorCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 21] == 1)
            {
                equipBootsCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 22] == 1)
            {
                equipFlippersCheckbox.Checked = true;
            }
            if (ROM_DATA[0x271A6 + 23] == 1)
            {
                equipMoonpearlCheckbox.Checked = true;
            }
            maxarrow_updown.Value = ROM_DATA[0x6DB58];
            maxarrowUp_updown.Value = ROM_DATA[0x6DB5F];

            maxbombs_updown.Value = ROM_DATA[0x6DB48];
            maxbombsUp_updown.Value = ROM_DATA[0x6DB4F];
            racetimer_updown.Value = ROM_DATA[0x6CCC0];


            (equipGlovescomboBox.SelectedIndex) = ROM_DATA[0x271A6 + 20];
            (equipSwordcomboBox.SelectedIndex) = ROM_DATA[0x271A6 + 25];
            (equipShieldcomboBox.SelectedIndex) = ROM_DATA[0x271A6 + 26];
            (equipMailcomboBox.SelectedIndex) = ROM_DATA[0x271A6 + 27];
            (equipBottle1Combobox.SelectedIndex) = ROM_DATA[0x271A6 + 28];
            (equipBottle2Combobox.SelectedIndex) = ROM_DATA[0x271A6 + 29];
            (equipBottle3Combobox.SelectedIndex) = ROM_DATA[0x271A6 + 30];
            (equipBottle4Combobox.SelectedIndex) = ROM_DATA[0x271A6 + 31];

            byte[] rupeec = new byte[2];

            rupeec[0] = ROM_DATA[0x271A6 + 32];
            rupeec[1] = ROM_DATA[0x271A6 + 33];
            rupeeNumericupdown.Value = BitConverter.ToInt16(rupeec, 0);







            byte Data1 = ROM_DATA[0x271A6 + 36];
            byte Data2 = ROM_DATA[0x271A6 + 37];

            for (int i = 0; i < 13; i++)
            {
                if (dungeonsData[i].dungeonPos < 8)
                {
                    if (getBit(Data1, dungeonsData[i].dungeonPos))
                    {
                        dungeonsData[i].compass = true;
                    }
                }
                else
                {
                    if (getBit(Data2, (byte)(dungeonsData[i].dungeonPos - 8)))
                    {
                        dungeonsData[i].compass = true;
                    }
                }
            }


            Data1 = ROM_DATA[0x271A6 + 38];
            Data2 = ROM_DATA[0x271A6 + 39];

            for (int i = 0; i < 13; i++)
            {
                if (dungeonsData[i].dungeonPos < 8)
                {
                    if (getBit(Data1, dungeonsData[i].dungeonPos))
                    {
                        dungeonsData[i].bigKey = true;
                    }
                }
                else
                {
                    if (getBit(Data2, (byte)(dungeonsData[i].dungeonPos - 8)))
                    {
                        dungeonsData[i].bigKey = true;
                    }
                }
            }

            Data1 = ROM_DATA[0x271A6 + 40];
            Data2 = ROM_DATA[0x271A6 + 41];

            for (int i = 0; i < 13; i++)
            {
                if (dungeonsData[i].dungeonPos < 8)
                {
                    if (getBit(Data1, dungeonsData[i].dungeonPos))
                    {
                        dungeonsData[i].map = true;
                    }
                }
                else
                {
                    if (getBit(Data2, (byte)(dungeonsData[i].dungeonPos - 8)))
                    {
                        dungeonsData[i].map = true;
                    }
                }
            }

            startingheart_updown.Value = (ROM_DATA[0x271A6 + 44] / 8);
            if (ROM_DATA[0x271A6 + 47] == 50)
            {
                checkBox22.Checked = true;
            }
            //pendant
            Data1 = ROM_DATA[0x271A6 + 52];

            if (getBit(Data1, 2))
            {
                dungeonsData[2].crystal = true;
            }

            if (getBit(Data1, 1))
            {
                dungeonsData[3].crystal = true;
            }

            if (getBit(Data1, 0))
            {
                dungeonsData[4].crystal = true;
            }



            Data1 = ROM_DATA[0x271A6 + 58];
            //11,6,10,12,7,9,8
            if (getBit(Data1, 0))
            {
                dungeonsData[11].crystal = true;
            }

            if (getBit(Data1, 1))
            {
                dungeonsData[6].crystal = true;
            }

            if (getBit(Data1, 2))
            {
                dungeonsData[10].crystal = true;
            }

            if (getBit(Data1, 3))
            {
                dungeonsData[12].crystal = true;
            }

            if (getBit(Data1, 4))
            {
                dungeonsData[7].crystal = true;
            }

            if (getBit(Data1, 5))
            {
                dungeonsData[9].crystal = true;
            }
            if (getBit(Data1, 6))
            {
                dungeonsData[8].crystal = true;
            }

            Data1 = ROM_DATA[0x271A6 + 57];
            if (getBit(Data1, 2))
            {
                equipBootsCheckbox.Checked = true;
            }
            if (getBit(Data1, 1))
            {

                equipFlippersCheckbox.Checked = true;
            }

        }

        private void saveasButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = file;
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            /*if (e.Cancel == false)
            {
                file = saveFileDialog1.FileName;
                save_fairy();
                save_medaillons();
                FileStream fs = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(ROM_DATA, 0, 2097152);
                fs.Close();
            }*/
        }

        private void importspoilerButton_Click(object sender, EventArgs e)
        {
            if (file != "")
            {
                List<string> lines = new List<string>();
                for (int i = 0; i < dungeonComboBox.Items.Count; i++)
                {
                    lines.Add("[Region : " + (dungeonComboBox.Items[i]) + "]");
                    foreach (Chest c in chest_list)
                    {
                        if (c.region == i)
                        {
                            for (int j = 0; j < ItemsList.Count; j++)
                            {
                                if (ItemsList[j].address == ROM_DATA[c.address])
                                {
                                    lines.Add(c.name.PadRight(60, '.') + ":" + ItemsList[j].Name + "");
                                    break;
                                }
                                else
                                {
                                    if (ROM_DATA[c.address] == 0x01)//MS
                                    {
                                        lines.Add(c.name.PadRight(60, '.') + ":" + "L2Sword");
                                        break;

                                    }
                                }
                            }
                        }
                    }
                    lines.Add(" ");
                }
                //medaillons
                lines.Add("Medaillon Required");
                lines.Add("Turtle Rock".PadRight(60, '.') + ":" + comboBox4.Items[comboBox4.SelectedIndex] + "");
                lines.Add("Misery Mire".PadRight(60, '.') + ":" + comboBox5.Items[comboBox5.SelectedIndex] + "");
                //fairy
                lines.Add(" ");
                lines.Add("Fairy Fountains");
                lines.Add("Pyramid".PadRight(60, '.') + ":" + comboBox6.Items[comboBox6.SelectedIndex] + "");
                lines.Add("Waterfall".PadRight(60, '.') + ":" + comboBox7.Items[comboBox7.SelectedIndex] + "");
                //Swamp?
                lines.Add(" ");
                lines.Add("Extras");
                //lines.Add("Half Magic Item".PadRight(60, '.') + ":" + comboBox9.Items[comboBox9.SelectedIndex] + "");
                File.WriteAllLines("spoilers.txt", lines.ToArray());
                formItems.richTextBox1.Text = "";
                formItems.richTextBox1.Lines = lines.ToArray();
            }
        }

        private void exportspoilerButton_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {

            if (openFileDialog2.FileName != "")
            {
                string[] item = new string[1];
                string its = "";
                string[] lines = File.ReadAllLines(openFileDialog2.FileName);
                int lnbr = 0;
                for (int i = 0; i < dungeonComboBox.Items.Count; i++)
                {
                    lnbr++;
                    foreach (Chest c in chest_list)
                    {
                        if (c.region == i)
                        {
                            //Console.WriteLine(lnbr);
                            //Console.WriteLine(c.name);
                            item = lines[lnbr].Split(':');
                            its = item[1];
                            if (item[1][0] == ' ')
                            {
                                its = item[1].Remove(0, 1);
                            }
                            for (int j = 0; j < ItemsList.Count; j++)
                            {

                                if (ItemsList[j].Name == its)
                                {
                                    //lines.Add(c.name + " : " + ItemsList[j].Name + "");

                                    ROM_DATA[c.address] = (byte)ItemsList[j].address;
                                    lnbr++;
                                    break;
                                }

                            }
                        }
                    }
                    //lines.Add(" ");
                    lnbr++;
                }
                lnbr++;
                item = lines[lnbr].Split(':'); its = item[1];
                if (item[1][0] == ' ') { its = item[1].Remove(0, 1); }
                //medaillons
                for (int j = 0; j < comboBox4.Items.Count; j++)
                {
                    if ((comboBox4.Items[j] as string) == its)
                    {
                        comboBox4.SelectedIndex = j;
                        break;
                    }
                }
                lnbr++;
                item = lines[lnbr].Split(':'); its = item[1];
                if (item[1][0] == ' ') { its = item[1].Remove(0, 1); }
                for (int j = 0; j < comboBox5.Items.Count; j++)
                {
                    if ((comboBox5.Items[j] as string) == its)
                    {
                        comboBox5.SelectedIndex = j;
                    }
                }

                lnbr++;
                lnbr++;
                lnbr++;
                item = lines[lnbr].Split(':'); its = item[1];
                if (item[1][0] == ' ') { its = item[1].Remove(0, 1); }
                for (int j = 0; j < comboBox6.Items.Count; j++)
                {
                    if ((comboBox6.Items[j] as string) == its)
                    {
                        comboBox6.SelectedIndex = j;
                    }
                }
                lnbr++;
                item = lines[lnbr].Split(':'); its = item[1];
                if (item[1][0] == ' ') { its = item[1].Remove(0, 1); }
                for (int j = 0; j < comboBox7.Items.Count; j++)
                {
                    if ((comboBox7.Items[j] as string) == its)
                    {
                        comboBox7.SelectedIndex = j;
                    }
                }
                lnbr++;
                lnbr++;
                lnbr++;
                //item = lines[lnbr].Split(':'); its = item[1];
                //if (item[1][0] == ' ') { its = item[1].Remove(0, 1); }
                /*for (int j = 0; j < comboBox9.Items.Count; j++)
                {
                    if ((comboBox9.Items[j] as string) == its)
                    {
                        comboBox9.SelectedIndex = j;
                    }
                }*/
                lnbr++;
                //File.WriteAllLines("spoilers.txt", lines.ToArray());
            }
        }

        private void itemComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ROM_DATA[(chestListBox.SelectedItem as Chest).address] = (byte)(itemComboBox.SelectedItem as Item).address;
            (chestListBox.SelectedItem as Chest).itemin = (itemComboBox.SelectedItem as Item);
            if ((chestListBox.SelectedItem as Chest).address == 0x48B7C)
            {
                ROM_DATA[0x44AA9] = (byte)(itemComboBox.SelectedItem as Item).address;
            }
            if ((chestListBox.SelectedItem as Chest).address == 0x48B81)
            {
                ROM_DATA[0x44AAE] = (byte)(itemComboBox.SelectedItem as Item).address;
            }
            userChanged = true;
        }

        public static Palette[] palettes = new Palette[80];

        public void loadPalettes()
        {
            palettes[0] = new Palette(0x0DD308, 15);
            palettes[1] = new Palette(0x0DD326, 15);
            palettes[2] = new Palette(0x0DD344, 15);
            palettes[3] = new Palette(0x0DD648, 4);
            palettes[4] = new Palette(0x0DD650, 4);
            palettes[5] = new Palette(0x0DD658, 4);
            palettes[6] = new Palette(0x0DD630, 3);
            palettes[7] = new Palette(0x0DD636, 3);
            palettes[8] = new Palette(0x0DD63C, 3);
            palettes[9] = new Palette(0x0DD642, 3);
            palettes[10] = new Palette(0xF4CCD, 8); //cry
            palettes[11] = new Palette(0x643F3, 8); //tri
            for (int i = 0; i < 24; i++)
            {
                palettes[12 + i] = new Palette((0xDD4E0) + (i * 14), 7);
            }
            for (int i = 0; i < 16; i++)
            {
                palettes[36 + i] = new Palette((0xDD39E) + (i * 14), 7);
            }
            for (int i = 0; i < 18; i++)
            {
                palettes[52 + i] = new Palette((0xDD446) + (i * 14), 7);
            }
        }

        Random rand = new Random();
        //Convert Snes Address in PC Address - Mirrored Format
        public int snestopc(int addr)
        {
            int temp = (addr & 0x7FFF) + ((addr / 2) & 0xFF8000);
            return (temp);
        }

        //Convert PC Address to Snes Address
        public int pctosnes(int addr)
        {
            byte[] b = BitConverter.GetBytes(addr);
            b[2] = (byte)(b[2] * 2);
            if (b[1] >= 0x80)
                b[2] += 1;
            else
                b[1] += 0x80;

            return BitConverter.ToInt32(b, 0);
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        ListItem formItems = new ListItem();
        private void viewAllItemsAsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
            //
            //
            //
            //
            //
            formItems.ShowDialog();
        } //Unused code

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatePalettes();
        }

        private void potsRandomizeButton_Click(object sender, EventArgs e)
        {
            PotListing.randomizePots();
        }

        private void triforceMaincolorButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will set all palette colors are you sure you want to continue?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                generateMidColor(palettes[11]);
            }
        }

        private void generateMidColor(Palette p)
        {
            colorDialog1.ShowDialog();
            float dark = 1;
            float light = 0;
            for (int i = 0; i < 8; i++)
            {
                if (i < 4)
                    p.setColor((byte)i, ControlPaint.Dark(colorDialog1.Color, dark));
                if (i == 4)
                    p.setColor((byte)i, colorDialog1.Color);
                if (i > 4)
                {
                    light += 0.20f;
                    p.setColor((byte)i, ControlPaint.Light(colorDialog1.Color, light));

                }
                updatePalettes();
                dark -= 0.30f;
            }
        }

        private void crystalsMaincolorButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This will set all palette colors are you sure you want to continue?", "Warning", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                generateMidColor(palettes[10]);
            }
        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Page 0 - Main Flags - no need to show anything else

            if (tabControl3.SelectedIndex != 0)
            {
                panel1.Parent = tabControl3.SelectedTab;
                crystalCheckBox.Checked = false;
                bigkeyCheckBox.Checked = false;
                mapCheckBox.Checked = false;
                compassCheckBox.Checked = false;
                alreadyopenCheckBox.Checked = false;
                bossaiCombobox.Enabled = false;
                if (dungeonsData[tabControl3.SelectedIndex - 1].bigKey)
                {
                    bigkeyCheckBox.Checked = true;
                }
                if (dungeonsData[tabControl3.SelectedIndex - 1].crystal)
                {
                    crystalCheckBox.Checked = true;
                }
                if (dungeonsData[tabControl3.SelectedIndex - 1].map)
                {
                    mapCheckBox.Checked = true;
                }
                if (dungeonsData[tabControl3.SelectedIndex - 1].compass)
                {
                    compassCheckBox.Checked = true;
                }
                if (dungeonsData[tabControl3.SelectedIndex - 1].entrance)
                {
                    alreadyopenCheckBox.Checked = true;
                }
            }
        }

        private void crystalCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void compassCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void crystalCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (crystalCheckBox.Checked == true)
            {
                dungeonsData[tabControl3.SelectedIndex - 1].crystal = true;
            }
            else
            {
                dungeonsData[tabControl3.SelectedIndex - 1].crystal = false;
            }
        }

        private void bigkeyCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (bigkeyCheckBox.Checked == true)
            {
                dungeonsData[tabControl3.SelectedIndex - 1].bigKey = true;
            }
            else
            {
                dungeonsData[tabControl3.SelectedIndex - 1].bigKey = false;
            }
        }

        private void compassCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (compassCheckBox.Checked == true)
            {
                dungeonsData[tabControl3.SelectedIndex - 1].compass = true;
            }
            else
            {
                dungeonsData[tabControl3.SelectedIndex - 1].compass = false;
            }
        }

        private void mapCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (mapCheckBox.Checked == true)
            {
                dungeonsData[tabControl3.SelectedIndex - 1].map = true;
            }
            else
            {
                dungeonsData[tabControl3.SelectedIndex - 1].map = false;
            }
        }

        private void alreadyopenCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (alreadyopenCheckBox.Checked == true)
            {
                dungeonsData[tabControl3.SelectedIndex - 1].entrance = true;
            }
            else
            {
                dungeonsData[tabControl3.SelectedIndex - 1].entrance = false;
            }
        }

        private void spritesListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Sprite Table Used : 0x6B266 + sprite
            //Damage Table : 0x37427 (armor based * 3)
            //HP Table/Value? : 0x06B173 + sprite
            sprite_dmgtable_updown.Value = (ROM_DATA[0x6B266 + spritesListbox.SelectedIndex] & 15);
            sprite_hp_updown.Value = ROM_DATA[0x6B173 + spritesListbox.SelectedIndex];
            dmg_greenmail_updown.Value = ROM_DATA[0x3742D + (int)(((int)sprite_dmgtable_updown.Value & 15) * 3) + 0];
            dmg_bluemail_updown.Value = ROM_DATA[0x3742D + (int)(((int)sprite_dmgtable_updown.Value & 15) * 3) + 1];
            dmg_redmail_updown.Value = ROM_DATA[0x3742D + (int)(((int)sprite_dmgtable_updown.Value & 15) * 3) + 2];
        }

        private void sprite_hp_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x6B173 + spritesListbox.SelectedIndex] = (byte)sprite_hp_updown.Value;

        }

        private void sprite_dmgtable_updown_ValueChanged(object sender, EventArgs e)
        {
            dmg_greenmail_updown.Value = ROM_DATA[0x3742D + (int)(((int)sprite_dmgtable_updown.Value & 15) * 3) + 0];
            dmg_bluemail_updown.Value = ROM_DATA[0x3742D + (int)(((int)sprite_dmgtable_updown.Value & 15) * 3) + 1];
            dmg_redmail_updown.Value = ROM_DATA[0x3742D + (int)(((int)sprite_dmgtable_updown.Value & 15) * 3) + 2];
            ROM_DATA[0x6B266 + spritesListbox.SelectedIndex] = (byte)((ROM_DATA[0x6B266 + spritesListbox.SelectedIndex] & 240) + (int)sprite_dmgtable_updown.Value);
        }

        private void dmg_greenmail_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x3742D + ((int)((int)sprite_dmgtable_updown.Value & 15) * 3) + 0] = (byte)dmg_greenmail_updown.Value;
        }

        private void dmg_bluemail_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x3742D + ((int)((int)sprite_dmgtable_updown.Value & 15) * 3) + 1] = (byte)dmg_bluemail_updown.Value;
        }

        private void dmg_redmail_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x3742D + ((int)((int)sprite_dmgtable_updown.Value & 15) * 3) + 2] = (byte)dmg_redmail_updown.Value;
        }

        private void dmg_sword1_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x6B8FA] = (byte)dmg_sword1_updown.Value;



        }

        private void dmg_sword2_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x6B902] = (byte)dmg_sword2_updown.Value;
        }

        private void dmg_sword3_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x6B90A] = (byte)dmg_sword3_updown.Value;
        }

        private void dmg_sword4_updown_ValueChanged(object sender, EventArgs e)
        {
            ROM_DATA[0x6B912] = (byte)dmg_sword4_updown.Value;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 243; i++)
            {
                ROM_DATA[0x6B359 + i] = randomizeSpritePalette(ROM_DATA[0x6B359 + i]);
            }
        }

        public byte randomizeSpritePalette(byte cv)
        {
            byte ctemp = cv;
            byte r = (byte)rand.Next(0, 15); // 1
            cv = (byte)((cv & 0xf0) | r);
            cv = (byte)((cv & 0xFE) | (ctemp & 1));
            return cv;
        }

        byte[] ROM_DATAV6 = new byte[2097152];
        private void importV6RandoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != "")
            {
                openFileDialog4.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error", "You must have a 1.0JP or V7 ROM Loaded before importing a V6");
            }
        }

        private void openFileDialog4_FileOk(object sender, CancelEventArgs e)
        {
            FileStream fs = new FileStream(openFileDialog4.FileName, FileMode.Open, FileAccess.Read);
            fs.Read(ROM_DATAV6, 0, (int)fs.Length);
            fs.Close();

            foreach (Chest c in chest_list)//chest
            {
                ROM_DATA[c.address] = ROM_DATAV6[c.address];
            }
            for (int i = 0; i < 10; i++) // boss hearts
            {
                ROM_DATA[0x180150 + i] = 0x3E;
            }
            ROM_DATA[0x180016] = ROM_DATAV6[0x48B7C]; // ether
            ROM_DATA[0x180017] = ROM_DATAV6[0x48B81]; // bombos
            ROM_DATA[0x180015] = 0x4E; // bat item

            ROM_DATA[0x180028] = 0x03; // fairy sword
            ROM_DATA[0x289B0] = 0x01; // pedestal sword
            ROM_DATA[0x3348E] = 0x03; // smith check
            ROM_DATA[0x3355C] = 0x02; //smith



        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox16.SelectedIndex = dungeonCrystal[comboBox15.SelectedIndex];
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            dungeonCrystal[comboBox15.SelectedIndex] = (byte)comboBox16.SelectedIndex;
        }

        public class Zdata
        {
            public byte[] array;
            public int offset;
            public Zdata(int offset, byte[] array)
            {
                this.offset = offset;
                this.array = array;
            }
        }

        private void entranceCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            entranceDestinationCombobox.SelectedIndex = (int)(ROM_DATA[0xDBB73 + entranceCombobox.SelectedIndex]);
            
        }

        private void exitCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            exitdestinationCombobox.SelectedIndex = new_exit_data[exitCombobox.SelectedIndex];
        }

        private void entranceDestinationCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
             ROM_DATA[0xDBB73 + entranceCombobox.SelectedIndex] = (byte)entranceDestinationCombobox.SelectedIndex;
        }

        private void exitdestinationCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            new_exit_data[exitCombobox.SelectedIndex] = (byte)exitdestinationCombobox.SelectedIndex;
            label85.ForeColor = Color.Green;
            label85.Text = "Entrances are all unique";
            for (int i = 0;i<new_exit_data.Length;i++)
            {
                byte b = new_exit_data[i];
                for (int j = 0; j < new_exit_data.Length; j++)
                {
                    if (b == new_exit_data[j])
                    {
                        if (j != i)
                        {
                            label85.ForeColor = Color.Red;
                            label85.Text = "ERROR Entrance ID : " + i.ToString() + " Is not unique";
                        }
                    }
                }
            }

        }
    }
}
