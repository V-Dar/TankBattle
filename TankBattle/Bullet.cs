using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public class Bullet : PictureBox
    {
        public int Speed { get; set; }
        public string ImagePath { get; set; }
        public Bitmap BulletUpImage { get; private set; }
        public Bitmap BulletDownImage { get; private set; }
        public Bitmap BulletLeftImage { get; private set; }
        public Bitmap BulletRightImage { get; private set; }
        readonly Form1 form;
        public Tank CurrentTank { get; set; }
        
        public Bullet(Form1 form, Tank currentTank, int bulletWidth, int bulletHeight, int speed, string imagePath)
        {
            ImagePath = imagePath;
            this.form = form;
            CurrentTank = currentTank;
            Speed = speed;
            Tag = "Bullet";
            BulletUpImage = (Bitmap)Image.FromFile(imagePath);
            BulletRightImage = (Bitmap)BulletUpImage.Clone();
            BulletRightImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            BulletDownImage = (Bitmap)BulletUpImage.Clone();
            BulletDownImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            BulletLeftImage = (Bitmap)BulletUpImage.Clone();
            BulletLeftImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            if (CurrentTank.Image == CurrentTank.TankUpImage)
            {
                Location = new Point(CurrentTank.Location.X + CurrentTank.Width / 2 - bulletWidth / 2, CurrentTank.Location.Y - bulletHeight - 10);
                Image = BulletUpImage;
                Size = new Size(bulletWidth, bulletHeight);
            }
            if (CurrentTank.Image == CurrentTank.TankDownImage)
            {
                Location = new Point(CurrentTank.Location.X + CurrentTank.Width / 2 - bulletWidth / 2, CurrentTank.Location.Y + CurrentTank.Height + 10);
                Image = BulletDownImage;
                Size = new Size(bulletWidth, bulletHeight);
            }
            if (CurrentTank.Image == CurrentTank.TankLeftImage)
            {
                Location = new Point(CurrentTank.Location.X - bulletHeight - 10, CurrentTank.Location.Y + CurrentTank.Height / 2 - bulletWidth / 2);
                Image = BulletLeftImage;
                Size = new Size(bulletHeight, bulletWidth);
            }
            if (CurrentTank.Image == CurrentTank.TankRightImage)
            {
                Location = new Point(CurrentTank.Location.X + CurrentTank.Width + 10, CurrentTank.Location.Y + CurrentTank.Height / 2 - bulletWidth / 2);
                Image = BulletRightImage;
                Size = new Size(bulletHeight, bulletWidth);
            }

            SizeMode = PictureBoxSizeMode.StretchImage;         
            TabStop = false;
        }
        public void StartFlyStraight()
        {
            Timer timerFlyBullet = new Timer();
            form.Controls.Add(this);
            timerFlyBullet.Interval = 5;
            timerFlyBullet.Tick += BulletFlyTick;
            timerFlyBullet.Start();
            void BulletFlyTick(object sender, EventArgs e)
            {
                if (Image == BulletLeftImage) Left -= Speed;
                else if (Image == BulletRightImage) Left += Speed;
                else if (Image == BulletUpImage) Top -= Speed;
                else if (Image == BulletDownImage) Top += Speed;
                if (Left < 0 || Left > form.Width || Top < 0 || Top > form.Height)
                {
                    form.Controls.Remove(this);
                    timerFlyBullet.Stop();
                }
            }
        }
        public void StartFlySin()
        {
            Timer timerFlyBullet = new Timer();
            form.Controls.Add(this);
            int topStartFire = Top;
            int leftStartFire = Left;
            timerFlyBullet.Interval = 8;
            timerFlyBullet.Tick += BulletFlyTick;
            timerFlyBullet.Start();

            void BulletFlyTick(object sender, EventArgs e)
            {
                if (Image == BulletLeftImage)
                {
                    Left -= Speed;
                    Top = topStartFire + (int)(40 * Math.Sin(2 * Left * Math.PI / 180));
                }
                else if (Image == BulletRightImage)
                {
                    Left += Speed;
                    Top = topStartFire + (int)(40 * Math.Sin(2 * Left * Math.PI / 180));
                }
                else if (Image == BulletUpImage)
                {
                    Top -= Speed;
                    Left = leftStartFire + (int)(40 * Math.Sin(2 * Top * Math.PI / 180));
                }
                else if (Image == BulletDownImage)
                {
                    Top += Speed;
                    Left = leftStartFire + (int)(40 * Math.Sin(2 * Top * Math.PI / 180));
                }
                if (Left < 0 || Left > form.Width || Top < 0 || Top > form.Height)
                {
                    form.Controls.Remove(this);
                    timerFlyBullet.Stop();
                }
            }
        }

    }
}
