using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TankBattle
{
    public class AmmoSplash : PictureBox
    {
        public AmmoSplash(Form1 form, int xLocation, int yLocation)
        {
            Tag = "AmmoSplash";
            Image = Image.FromFile("Items\\ammoSplash.jpg");
            Location = new Point(xLocation, yLocation);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Size = new Size(20, 20);
            TabStop = false;
            form.Controls.Add(this);
        }
    }
}
