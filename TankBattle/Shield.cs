using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public class Shield : PictureBox
    {
        public Shield(Form1 form, int xLocation, int yLocation)
        {
            Tag = "Shield";
            Image = Image.FromFile("Items\\shield.png");
            Location = new Point(xLocation, yLocation);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Size = new Size(20, 20);
            TabStop = false;
            form.Controls.Add(this);
            form.shieldOnForm = true;
        }
    }
}
