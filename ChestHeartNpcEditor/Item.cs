using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChestHeartNpcEditor
{
    public class Item
    {
        string name;
        public int address;
        public Item(string name,int address)
        {
            this.name = name;
            this.address = address;
        }
        public string Name
        {
            get { return name; }
        }
    }
    public partial class Form1 : Form
    {
        public void itemlistcreate()
        {
            ItemsList.Add(new Item("Nothing", 0xFF));
            ItemsList.Add(new Item("1 Arrow", 0x43));
            ItemsList.Add(new Item("1 Rupee", 0x34));
            ItemsList.Add(new Item("3 Bomb", 0x28));
            ItemsList.Add(new Item("5 Rupees", 0x35));
            ItemsList.Add(new Item("10 Arrows", 0x44));
            ItemsList.Add(new Item("10 Bombs", 0x31));
            ItemsList.Add(new Item("20 Rupees", 0x36));
            ItemsList.Add(new Item("20 Rupee2", 0x47));
            ItemsList.Add(new Item("50 Rupees", 0x41));
            ItemsList.Add(new Item("100 Rupees", 0x40));
            ItemsList.Add(new Item("300 Rupees", 0x46));
            ItemsList.Add(new Item("Bee for bottle (no bottle)", 0x0E));
            ItemsList.Add(new Item("Big Key", 0x32)); //13
            ItemsList.Add(new Item("Blue Mail", 0x22));
            ItemsList.Add(new Item("Blue Shield", 0x04));//15
            ItemsList.Add(new Item("Blue Potion - does nothing if no bottle", 0x30));
            ItemsList.Add(new Item("Book of Mudora", 0x1D));//17
            ItemsList.Add(new Item("Boomerang", 0x0C));//18
            ItemsList.Add(new Item("Bombos", 0x0F));//19
            ItemsList.Add(new Item("Bomb", 0x27));//20
            ItemsList.Add(new Item("Bottle", 0x16));//21
            ItemsList.Add(new Item("Bottle with Red Potion", 0x2B));
            ItemsList.Add(new Item("Bottle with Green Potion", 0x2C));
            ItemsList.Add(new Item("Bottle with Blue Potion", 0x2D));
            ItemsList.Add(new Item("Bottle With Bee", 0x3C));
            ItemsList.Add(new Item("Bottle With Fairy", 0x3D));
            ItemsList.Add(new Item("Bottle with Gold Bee", 0x48));
            ItemsList.Add(new Item("Bow And Arrows", 0x3A));//28
            ItemsList.Add(new Item("Bow And Silver Arrows", 0x3B));//29
            ItemsList.Add(new Item("Bow", 0x0B));//30
            ItemsList.Add(new Item("Bug Net", 0x21));//31
            ItemsList.Add(new Item("Cane of Somaria", 0x15));//32
            ItemsList.Add(new Item("Cane of Byrna", 0x18));//33
            ItemsList.Add(new Item("Compass", 0x25));//34
            ItemsList.Add(new Item("Crystal - Crash Game", 0x20));//35
            ItemsList.Add(new Item("Ether", 0x10));//36
            ItemsList.Add(new Item("Fire Rod", 0x07));//37
            ItemsList.Add(new Item("Flippers", 0x1E));//38
            ItemsList.Add(new Item("Green Potion - does nothing if no bottle", 0x2F));
            ItemsList.Add(new Item("Hammer", 0x09));//40
            ItemsList.Add(new Item("Heart Container no dialog", 0x3E));
            ItemsList.Add(new Item("Heart Container", 0x3F));
            ItemsList.Add(new Item("Heart", 0x42));
            ItemsList.Add(new Item("Heart Container (No Animation)", 0x26));
            ItemsList.Add(new Item("Hookshot", 0x0A));//45
            ItemsList.Add(new Item("Ice Rod", 0x08));//46
            ItemsList.Add(new Item("Key", 0x24));//47
            ItemsList.Add(new Item("L1SwordAndShield", 0x00));//48
            ItemsList.Add(new Item("L1 Sword", 0x49));//49
            ItemsList.Add(new Item("L2Sword", 0x50));//50
            ItemsList.Add(new Item("L3Sword", 0x02));
            ItemsList.Add(new Item("L4Sword", 0x03));
            ItemsList.Add(new Item("Lamp", 0x12));
            ItemsList.Add(new Item("Magic Cape", 0x19));
            ItemsList.Add(new Item("Magic Mirror", 0x1A));
            ItemsList.Add(new Item("Map", 0x33));
            ItemsList.Add(new Item("Mirror Shield", 0x06));
            ItemsList.Add(new Item("Mushroom", 0x29));
            ItemsList.Add(new Item("Moon Pearl", 0x1F));
            ItemsList.Add(new Item("Ocarina Active", 0x4A));
            ItemsList.Add(new Item("Ocarina Inactive", 0x14));
            ItemsList.Add(new Item("Pegasus Boots", 0x4B));//62
            ItemsList.Add(new Item("Piece of Heart", 0x17));
            ItemsList.Add(new Item("Pendant of Courage", 0x37));//64
            ItemsList.Add(new Item("Pendant of Wisdom", 0x38));
            ItemsList.Add(new Item("Pendant of Power", 0x39));
            ItemsList.Add(new Item("Powder", 0x0D));
            ItemsList.Add(new Item("Power Glove", 0x1B));
            ItemsList.Add(new Item("Quake", 0x11));
            ItemsList.Add(new Item("Red Shield", 0x05));
            ItemsList.Add(new Item("Red Boomerang", 0x2A));
            ItemsList.Add(new Item("Red Mail", 0x23));
            ItemsList.Add(new Item("Red Potion - does nothing if no bottle", 0x2E));
            ItemsList.Add(new Item("Shovel", 0x13));
            ItemsList.Add(new Item("Small Magic", 0x45));
            ItemsList.Add(new Item("Titans Mitt", 0x1C));//76
            ItemsList.Add(new Item("Max Bombs", 0x4C));//50
            ItemsList.Add(new Item("Max Arrows", 0x4D));//50
            ItemsList.Add(new Item("Half Magic", 0x4E));//50
            ItemsList.Add(new Item("Quarter Magic", 0x4F));//50
            ItemsList.Add(new Item("+5 max bombs", 0x51));//55
            ItemsList.Add(new Item("+10 max bombs", 0x52));//55
            ItemsList.Add(new Item("+5 max arrows", 0x53));//55
            ItemsList.Add(new Item("+10 max arrows", 0x54));//55
            ItemsList.Add(new Item("Trap", 0x55));//55
        }
    }

}
