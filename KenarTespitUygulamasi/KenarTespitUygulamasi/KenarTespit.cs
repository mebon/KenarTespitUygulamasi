//MUHAMMED EMİN BERKAY KOCAOĞLU 201513171070

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KenarTespit
{
    public partial class KenarTespit : Form
    {
        Bitmap bmp;
        private int threshold = 128;
        public KenarTespit()
        {
            InitializeComponent();
        }
        private void KenarTespit_Load(object sender, EventArgs e)
        {
            thresholdDegeri.Text = threshold.ToString();
            if (pictureBox != null)
            {
                try
                {
                    bmp = (Bitmap)Bitmap.FromFile(@"sekiller.jpg");
                    openFileDialog1.FileName = @"sekiller.jpg";
                    pictureBox.SizeMode = PictureBoxSizeMode.Normal;
                    pictureBox.Image = bmp;
                    labelGenislik.Text = pictureBox.Width.ToString();
                    labelYukseklik.Text = pictureBox.Height.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dosya bulunamadı.");
                }
                this.Refresh();

            }

        }
        private int RGBOrtalama(int y, int x)
        {
            Color c = bmp.GetPixel(x, y);

            int r = c.R;
            int g = c.G;
            int b = c.B;
            int avg = (r + g + b) / 3;
            return avg;
        }
        private void button2GrayScale_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(RGBOrtalama(y, x), RGBOrtalama(y, x), RGBOrtalama(y, x)));
                    }
                }

                pictureBox.Image = bmp;
                this.Refresh();
            }
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {

                bmp = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
                pictureBox.Image = bmp;
                this.Refresh();
            }
        }
        private void buttonBitmap_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        Color c = bmp.GetPixel(x, y);

                        int r = c.R;
                        int g = c.G;
                        int b = c.B;

                        if ((r + g + b) / 3 > Convert.ToInt32(thresholdDegeri.Text))
                        {
                            bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                        }
                        else
                            bmp.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
                pictureBox.Image = bmp;
                this.Refresh();
            }
        }
        private void buttonResimYukleme_Click(object sender, EventArgs e)
        {
            if (pictureBox != null)
            {
                try
                {
                    openFileDialog1.ShowDialog();
                    pictureBox.ImageLocation = openFileDialog1.FileName;
                    bmp = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    labelGenislik.Text = pictureBox.Width.ToString();
                    labelYukseklik.Text = pictureBox.Height.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dosya bulunamadı.");
                }
            }
        }
        private void ImagePikselGradientHesapla(int[,] matris)
        {
            for (int y = 1; y < bmp.Height - 1; y++)
            {
                for (int x = 1; x < bmp.Width - 1; x++)
                {

                    int IpikselX = (matris[(x + 1), y] - matris[(x - 1), y]) / 2;
                    int IpikselY = (matris[x, (y + 1)] - matris[x, (y - 1)]) / 2;
                    int IpikselGradient = Convert.ToInt32(Math.Sqrt(Math.Pow(IpikselX, 2) + Math.Pow(IpikselY, 2)));

                    if (IpikselGradient >= Convert.ToInt32(thresholdDegeri.Text))
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                }
            }
        }
        private void buttonKenarTespit_Click(object sender, EventArgs e)
        {
            if (bmp != null)
            {
                int[,] IpikselXY = new int[bmp.Width, bmp.Height];       
                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        
                        IpikselXY[x, y] = RGBOrtalama(y,x);
                    }
                }
                ImagePikselGradientHesapla(IpikselXY); 
            }
            pictureBox.Image = bmp;
            this.Refresh();
        }

        
    }
}
//MUHAMMED EMİN BERKAY KOCAOĞLU 201513171070
