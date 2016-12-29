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
    public class Palette
    {
        public byte[] colorBytes;
        public int address;
        public Palette(int address, int size)
        {
            colorBytes = new byte[size * 2];
            this.address = address;
            for (int i = 0; i < size * 2; i++)
            {
                colorBytes[i] = Form1.ROM_DATA[address + i];
            }
        }
        public Color getColor(byte c)
        {
            short bc = BitConverter.ToInt16(colorBytes, c * 2);
            return Color.FromArgb((bc & 31) * 8, ((bc >> 5) & 31) * 8, ((bc >> 10) & 31) * 8);
        }

        public void save()
        {
            for (int i = 0; i < colorBytes.Length; i++)
            {
                Form1.ROM_DATA[address + i] = colorBytes[i];
            }
        }

        public void setColor(byte c, Color col)
        {
            short s = (short)(((col.B / 8) << 10) | ((col.G / 8) << 5) | ((col.R / 8) << 0));

            byte[] bb = BitConverter.GetBytes(s);
            colorBytes[c * 2] = bb[0];
            colorBytes[(c * 2) + 1] = bb[1];
        }
        public void DrawPalette(Graphics g)
        {
            for (int i = 0; i < colorBytes.Length / 2; i++)
            {
                g.FillRectangle(new SolidBrush(getColor((byte)i)), new Rectangle(i * 16, 0, 16, 16));
            }
        }

    }

    public partial class Form1
    {
        Graphics g;
        public void updatePalettes()
        {
            greenmailPalettePicturebox.Image = new Bitmap(greenmailPalettePicturebox.Size.Width, greenmailPalettePicturebox.Size.Height); greenmailPalettePicturebox.Tag = (int)0;
            bluemailPalettePicturebox.Image = new Bitmap(bluemailPalettePicturebox.Size.Width, bluemailPalettePicturebox.Size.Height); bluemailPalettePicturebox.Tag = (int)1;
            redmailPalettePicturebox.Image = new Bitmap(redmailPalettePicturebox.Size.Width, redmailPalettePicturebox.Size.Height); redmailPalettePicturebox.Tag = (int)2;
            shield1PalettePicturebox.Image = new Bitmap(shield1PalettePicturebox.Size.Width, shield1PalettePicturebox.Size.Height); shield1PalettePicturebox.Tag = (int)3;
            shield2PalettePicturebox.Image = new Bitmap(shield2PalettePicturebox.Size.Width, shield2PalettePicturebox.Size.Height); shield2PalettePicturebox.Tag = (int)4;
            shield3PalettePicturebox.Image = new Bitmap(shield3PalettePicturebox.Size.Width, shield3PalettePicturebox.Size.Height); shield3PalettePicturebox.Tag = (int)5;
            sword1PalettePicturebox.Image = new Bitmap(sword1PalettePicturebox.Size.Width, sword1PalettePicturebox.Size.Height); sword1PalettePicturebox.Tag = (int)6;
            sword2PalettePicturebox.Image = new Bitmap(sword2PalettePicturebox.Size.Width, sword2PalettePicturebox.Size.Height); sword2PalettePicturebox.Tag = (int)7;
            sword3PalettePicturebox.Image = new Bitmap(sword3PalettePicturebox.Size.Width, sword3PalettePicturebox.Size.Height); sword3PalettePicturebox.Tag = (int)8;
            sword4PalettePicturebox.Image = new Bitmap(sword4PalettePicturebox.Size.Width, sword4PalettePicturebox.Size.Height); sword4PalettePicturebox.Tag = (int)9;
            previewPalettePicturebox.Image = new Bitmap(previewPalettePicturebox.Size.Width, previewPalettePicturebox.Size.Height); 
            triforcePalettePicturebox.Image = new Bitmap(triforcePalettePicturebox.Size.Width, triforcePalettePicturebox.Size.Height); triforcePalettePicturebox.Tag = (int)11;
            crystalsPalettePicturebox.Image = new Bitmap(crystalsPalettePicturebox.Size.Width, crystalsPalettePicturebox.Size.Height); crystalsPalettePicturebox.Tag = (int)10;

            g = Graphics.FromImage(greenmailPalettePicturebox.Image);
            Form1.palettes[0].DrawPalette(g);
            g = Graphics.FromImage(bluemailPalettePicturebox.Image);
            Form1.palettes[1].DrawPalette(g);
            g = Graphics.FromImage(redmailPalettePicturebox.Image);
            Form1.palettes[2].DrawPalette(g);
            g = Graphics.FromImage(shield1PalettePicturebox.Image);
            Form1.palettes[3].DrawPalette(g);
            g = Graphics.FromImage(shield2PalettePicturebox.Image);
            Form1.palettes[4].DrawPalette(g);
            g = Graphics.FromImage(shield3PalettePicturebox.Image);
            Form1.palettes[5].DrawPalette(g);
            g = Graphics.FromImage(sword1PalettePicturebox.Image);
            Form1.palettes[6].DrawPalette(g);
            g = Graphics.FromImage(sword2PalettePicturebox.Image);
            Form1.palettes[7].DrawPalette(g);
            g = Graphics.FromImage(sword3PalettePicturebox.Image);
            Form1.palettes[8].DrawPalette(g);
            g = Graphics.FromImage(sword4PalettePicturebox.Image);
            Form1.palettes[9].DrawPalette(g);
            g = Graphics.FromImage(triforcePalettePicturebox.Image);
            Form1.palettes[11].DrawPalette(g);
            g = Graphics.FromImage(crystalsPalettePicturebox.Image);
            Form1.palettes[10].DrawPalette(g);
            onColorChange();
        }

        public void onColorChange()
        {
            Bitmap b = new Bitmap("PreviewPalettes/l1.png");
            Bitmap b2 = new Bitmap("PreviewPalettes/l2.png");
            Bitmap b3 = new Bitmap("PreviewPalettes/l3.png");
            Bitmap b4 = new Bitmap("PreviewPalettes/l4.png");
            BitmapData bd = new BitmapData();
            
            //redraw bitmap with new palette
            ColorPalette p = b.Palette;
            ColorPalette p2 = b2.Palette;
            ColorPalette p3 = b3.Palette;
            ColorPalette p4 = b4.Palette;
            for (int i = 0; i < 15; i++)
            {

                p.Entries[i] = Form1.palettes[0].getColor((byte)i);
                p2.Entries[i] = Form1.palettes[1].getColor((byte)i);
                p3.Entries[i] = Form1.palettes[2].getColor((byte)i);
                p4.Entries[i] = Form1.palettes[0].getColor((byte)i);
            }
            for (int i = 0; i < 4; i++)
            {
                p2.Entries[i + 16] = Form1.palettes[3].getColor((byte)i);
                p3.Entries[i + 20] = Form1.palettes[4].getColor((byte)i);
                p4.Entries[i + 24] = Form1.palettes[5].getColor((byte)i);
            }
            for (int i = 0; i < 3; i++)
            {
                p.Entries[i + 28] = Form1.palettes[6].getColor((byte)i);
                p2.Entries[i + 31] = Form1.palettes[7].getColor((byte)i);
                p3.Entries[i + 34] = Form1.palettes[8].getColor((byte)i);
                p4.Entries[i + 37] = Form1.palettes[9].getColor((byte)i);
            }
            b.Palette = p;
            b2.Palette = p2;
            b3.Palette = p3;
            b4.Palette = p4;
            //pictureBox11.Image = b;

            g = Graphics.FromImage(previewPalettePicturebox.Image);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.DrawImage(b, new Rectangle(0, 0, 48, 64), 0, 0, 24, 32, GraphicsUnit.Pixel);
            g.DrawImage(b2, new Rectangle(48 * 1, 0, 48, 64), 0, 0, 24, 32, GraphicsUnit.Pixel);
            g.DrawImage(b3, new Rectangle(48 * 2, 0, 48, 64), 0, 0, 24, 32, GraphicsUnit.Pixel);
            g.DrawImage(b4, new Rectangle(48 * 3, 0, 48, 64), 0, 0, 24, 32, GraphicsUnit.Pixel);
            previewPalettePicturebox.Refresh();
        }

        private void palettepictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int cIndex = (e.X / 16);

            PictureBox p = ((PictureBox)sender);
            int v = 0;
            v = (int)(p.Tag);
            colorDialog1.Color = Form1.palettes[v].getColor((byte)cIndex);
            colorDialog1.ShowDialog();
            Form1.palettes[v].setColor((byte)cIndex, colorDialog1.Color);
            onColorChange();
            g = Graphics.FromImage(p.Image);
            Form1.palettes[v].DrawPalette(g);
            p.Refresh();
        }


    }
}
