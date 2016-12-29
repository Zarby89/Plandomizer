using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChestHeartNpcEditor
{
    public partial class BlockDesigner
    {

    }
    public partial class ListItem : Form
    {
        public ListItem()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

            timer1.Enabled = true;
            timer1.Stop();
            timer1.Start();
        }
        int valuetest = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            checkItemsWrong();
        }



        public void checkItemsWrong()
        {
            string[] item = new string[0];
            string its = "";
            string[] lines = richTextBox1.Lines;
            int lnbr = 0;
            for (int i = 0; i < 15; i++) //dungeons count
            {
                lnbr++;
                foreach (Chest c in Form1.chest_list)
                {
                    if (c.region == i)
                    {
                        item = lines[lnbr].Split(':');
                        its = item[1];
                        if (item[1][0] == ' ')
                        {
                            its = item[1].Remove(0, 1);
                        }
                        for (int j = 0; j < Form1.ItemsList.Count; j++)
                        {

                            if (Form1.ItemsList[j].Name == its)
                            {
                                //No Error Found
                                richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(lnbr) + 62, richTextBox1.GetFirstCharIndexFromLine(lnbr + 1) - 1);
                                richTextBox1.SelectionColor = Color.Black;
                                lnbr++;
                                break;
                            }
                            if (j == Form1.ItemsList.Count)
                            {
                                if (Form1.ItemsList[j].Name != its)
                                {
                                    //error found
                                    //Select col 62 of line lnbr
                                    richTextBox1.Select(richTextBox1.GetFirstCharIndexFromLine(lnbr)+62, richTextBox1.GetFirstCharIndexFromLine(lnbr+1)-1);
                                    richTextBox1.SelectionColor = Color.Red;
                                }
                            }
                        }
                    }
                }
                //lines.Add(" ");
                lnbr++;
            }
        }

    }
}
