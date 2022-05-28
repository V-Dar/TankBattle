using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public static class Animations
    {
        public static void Explose(int x, int y, Form1 form)
        {
            PictureBox pBExplose = new PictureBox();
            pBExplose.Location = new Point(x, y);
            pBExplose.Name = "pBExplose";
            pBExplose.Size = new Size(100, 100);
            pBExplose.TabStop = false;
            pBExplose.SizeMode = PictureBoxSizeMode.StretchImage;
            form.Controls.Add(pBExplose);
            pBExplose.BringToFront();

            Timer timer = new Timer();
            timer.Interval = 30;
            Bitmap bitmap = new Bitmap(Image.FromFile("Animations\\explose.png"));
            int iImage = 0, jImage = 0;
            timer.Tick += Timer_Tick;
            timer.Start();

            void Timer_Tick(object sender, EventArgs e)
            {
                if (iImage > 7)
                {
                    iImage = 0;
                    jImage++;
                }
                Image part = new Bitmap(100, 100);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(bitmap, 0, 0, new Rectangle(iImage * 100, jImage * 100, iImage * 100 + 100, jImage * 100 + 100), GraphicsUnit.Pixel);
                pBExplose.Image = part;
                
                if (iImage == 7 && jImage == 5)
                {
                    form.Controls.Remove(pBExplose);
                    timer.Stop();
                }
                iImage++;
            }
        }

        public static void Fire(int x, int y, Form1 form)
        {
            PictureBox pBExplose = new PictureBox();
            pBExplose.Location = new Point(x, y);
            pBExplose.Name = "pBExplose";
            int widthImage = 192;
            int heightImage = 192;
            pBExplose.Size = new Size(50, 50);
            pBExplose.TabStop = false;
            pBExplose.SizeMode = PictureBoxSizeMode.StretchImage;
            form.Controls.Add(pBExplose);
            pBExplose.BringToFront();

            Timer timer = new Timer();
            timer.Interval = 30;
            Bitmap bitmap = new Bitmap(Image.FromFile("Animations\\fire.png"));
            int iImage = 0, jImage = 0;
            timer.Tick += Timer_Tick;
            timer.Start();

            void Timer_Tick(object sender, EventArgs e)
            {
                if (iImage > 4)
                {
                    iImage = 0;
                    jImage++;
                }
                Image part = new Bitmap(widthImage, heightImage);
                Graphics g = Graphics.FromImage(part);
                g.DrawImage(bitmap, 0, 0, new Rectangle(iImage * widthImage, jImage * heightImage, iImage * widthImage + widthImage, jImage * heightImage + heightImage), GraphicsUnit.Pixel);
                pBExplose.Image = part;

                if (iImage == 4 && jImage == 3)
                {
                    form.Controls.Remove(pBExplose);
                    timer.Stop();
                }
                iImage++;
            }
        }


    }
}
