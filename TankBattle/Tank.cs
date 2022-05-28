using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Stop
    }
    public class Tank : PictureBox
    {
        private int hp = 10;

        public virtual int HP
        {
            get { return hp; }
            set
            {
                hp = value;
                form.labelHP.Text = $"{value}HP".ToString();
            }
        }
        private bool isShielded;
        public bool HaveSplashGun { get; set; }
        private int tankPower;

        public int TankPower
        {
            get { return tankPower; }
            set
            {
                tankPower = value;
                IsShielded = IsShielded; // для вызова перерисовки танка
            }
        }
        private int ammoSplash = 0;
        public int AmmoSplash
        {
            get { return ammoSplash; }
            set
            {
                ammoSplash = value;
                form.labelAmmoSplash.Text= $"Splash (Z): {value}".ToString();
            }
        }


        public bool IsShielded
        {
            get { return isShielded; }
            set
            {
                if (value == false)
                {
                    form.labelShield.Text = "";
                    string imageShieldOff = "";
                    if (TankPower == 0) imageShieldOff = "Tank\\greenTank.bmp";
                    if (TankPower == 1) imageShieldOff = "Tank\\V2greenTank.bmp";
                    TankUpImage = (Bitmap)Image.FromFile(imageShieldOff);
                    TankRightImage = (Bitmap)TankUpImage.Clone();
                    TankRightImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    TankDownImage = (Bitmap)TankUpImage.Clone();
                    TankDownImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    TankLeftImage = (Bitmap)TankUpImage.Clone();
                    TankLeftImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                else
                {
                    form.labelShield.Text = "Shield ON";
                    string imageShieldOn = "";
                    if (TankPower == 0) imageShieldOn = "Tank\\greenTankShield.bmp";
                    if (TankPower == 1) imageShieldOn = "Tank\\V2greenTankShield.bmp";
                    TankUpImage = (Bitmap)Image.FromFile(imageShieldOn);
                    TankRightImage = (Bitmap)TankUpImage.Clone();
                    TankRightImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    TankDownImage = (Bitmap)TankUpImage.Clone();
                    TankDownImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    TankLeftImage = (Bitmap)TankUpImage.Clone();
                    TankLeftImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }
                isShielded = value;
            }
        }


        protected Form1 form;
        protected int Speed { get; set; }
        public Bitmap TankUpImage { get; set; }
        public Bitmap TankDownImage { get; set; }
        public Bitmap TankLeftImage { get; set; }
        public Bitmap TankRightImage { get; set; }
        public bool isLeft, isRight, isUp, isDown;
        public Bullet bullet { get; set; }
        //public List<Bullet> splashBullet = new List<Bullet>();
        protected Direction CurrentDirection { get; set; }


        public Tank(Form1 form, int speed, int xLocation, int yLocation)
        {
            this.form = form;
            Speed = speed;
            Location = new Point(xLocation, yLocation);      
            SizeMode = PictureBoxSizeMode.StretchImage;
            Size = new Size(50, 50);
            TabStop = false;
        }

        public virtual void AddTankOnForm()
        {
            Tag = "Tank";
            TankUpImage = (Bitmap)Image.FromFile("Tank\\greenTank.bmp");
            TankRightImage = (Bitmap)TankUpImage.Clone();
            TankRightImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            TankDownImage = (Bitmap)TankUpImage.Clone();
            TankDownImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            TankLeftImage = (Bitmap)TankUpImage.Clone();
            TankLeftImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Image = TankUpImage;
            form.labelHP.Text = $"{HP}HP".ToString();
            form.Controls.Add(this);
        }

        public virtual void MoveTank()
        {
            if (isLeft)
            {
                Image = TankLeftImage;
                Location = new Point(Location.X - Speed, Location.Y);
            }
            if (isRight)
            {
                Image = TankRightImage;
                Location = new Point(Location.X + Speed, Location.Y);
            }
            if (isUp)
            {
                Image = TankUpImage;
                Location = new Point(Location.X, Location.Y - Speed);
            }
            if (isDown)
            {
                Image = TankDownImage;
                Location = new Point(Location.X, Location.Y + Speed);
            }
        }    

        public virtual void Fire(Bullet bullet)
        {
            this.bullet = bullet;
            bullet.StartFlyStraight();     
        }
        public virtual void FireSplash(BulletSplash bulletSplash)
        {
            bulletSplash.StartFly();        
        }
        public virtual void FireSin(Bullet bullet)
        {
            this.bullet = bullet;    
            bullet.StartFlySin();
        }
    }
}
