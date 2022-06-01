using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    class SplashGun : PictureBox
    {
        public SplashGun(Form1 form, int xLocation, int yLocation)
        {
            Tag = "SplashGun";
            Image = Image.FromFile("Items\\splashGun.png");
            Location = new Point(xLocation, yLocation);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Size = new Size(48, 48);
            TabStop = false;
            form.Controls.Add(this);
        }
    }
}
