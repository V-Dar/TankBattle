using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public class EnemyTank : Tank, IEnemy
    {
        public Timer TimerDirectionGenerate { get; set; } = new Timer();
        public Timer TimerTankAutoFire { get; set; } = new Timer();
        public Timer TimerTankMove { get; set; } = new Timer();
        readonly Random random = new Random();
        private int hp = 1;
        public override int HP { get => hp; set => hp = value; }
        public EnemyTank(Form1 form, int speed, int xLocation, int yLocation) : base(form, speed, xLocation, yLocation)
        {
            TimerDirectionGenerate.Interval = 1500;
            TimerDirectionGenerate.Tick += TimerDirectionGenerate_Tick;

            TimerTankAutoFire.Interval = 1800;
            TimerTankAutoFire.Tick += TimerTankAutoFire_Tick;

            TimerTankMove.Interval = speed;
            TimerTankMove.Tick += TimerTankMove_Tick;
        }
        public override void AddTankOnForm()
        {
            Tag = "EnemyTank";
            TankUpImage = (Bitmap)Image.FromFile("EnemyTank\\blueTank1.bmp");
            TankRightImage = (Bitmap)TankUpImage.Clone();
            TankRightImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            TankDownImage = (Bitmap)TankUpImage.Clone();
            TankDownImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            TankLeftImage = (Bitmap)TankUpImage.Clone();
            TankLeftImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
            Image = TankUpImage;
            form.Controls.Add(this);
            TimerTankMove.Start();
            TimerTankAutoFire.Start();
            TimerDirectionGenerate.Start();
        }
        public void TimerTankMove_Tick(object sender, EventArgs e)
        {
            MoveTank();
        }

        public void TimerTankAutoFire_Tick(object sender, EventArgs e)
        {
            if (form.tank == null || this.HP < 1)
            {
                TimerTankAutoFire.Stop();
                TimerTankMove.Stop();
                TimerDirectionGenerate.Stop();
                return;
            }
            Fire(new Bullet(form, this, 17, 17, 6, "Bullet\\bullet.png"));
        }

        public void TimerDirectionGenerate_Tick(object sender, EventArgs e)
        {
            if (form.enemyList.Contains(this) == false)
            {
                TimerDirectionGenerate.Stop();
                return;
            }
            CurrentDirection = (Direction)random.Next(4);
        }
        public override void MoveTank()
        {
            if (form.tank != null)
            {
                if (Location.X <= 0) CurrentDirection = Direction.Right;
                if (Location.X >= form.Width - Width) CurrentDirection = Direction.Left;
                if (Location.Y <= 0) CurrentDirection = Direction.Down;
                if (Location.Y >= form.Height - 2 * Height) CurrentDirection = Direction.Up;

                if (CurrentDirection == Direction.Left)
                {
                    Image = TankLeftImage;
                    Location = new Point(Location.X - 3, Location.Y);
                }
                if (CurrentDirection == Direction.Right)
                {
                    Image = TankRightImage;
                    Location = new Point(Location.X + 3, Location.Y);
                }
                if (CurrentDirection == Direction.Up)
                {
                    Image = TankUpImage;
                    Location = new Point(Location.X, Location.Y - 3);
                }
                if (CurrentDirection == Direction.Down)
                {
                    Image = TankDownImage;
                    Location = new Point(Location.X, Location.Y + 3);
                }
            }
        }
        public void Destroy()
        {
            form.Controls.Remove(this);
            form.enemyList.Remove(this);
            Animations.Explose(Location.X - Width / 2, Location.Y - Height / 2, form);

            int item = random.Next(2);
            if (item == 1 && form.tank.HaveSplashGun)
            {
                new AmmoSplash(form, Location.X - Width / 2, Location.Y - Height / 2);
            }
        }
    }
}
