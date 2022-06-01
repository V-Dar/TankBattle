using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public class EnemyTankLogic1 : Tank, IEnemy
    {
        public Timer TimerDirectionGenerate { get; set; }
        public Timer TimerTankAutoFire { get; set; }
        public Timer TimerTankMove { get; set; }
        private Random random { get; set; } = new Random();
        private int hp = 2;
        public override int HP { get => hp; set => hp = value; }
        public EnemyTankLogic1(Form1 form, int speed, int xLocation, int yLocation) : base(form, speed, xLocation, yLocation)
        {
            TimerDirectionGenerate = new Timer();
            TimerDirectionGenerate.Interval = 100;
            TimerDirectionGenerate.Tick += TimerDirectionGenerate_Tick;

            TimerTankAutoFire = new Timer();
            TimerTankAutoFire.Interval = 500;
            TimerTankAutoFire.Tick += TimerTankAutoFire_Tick;

            TimerTankMove = new Timer();
            TimerTankMove.Interval = speed;
            TimerTankMove.Tick += TimerTankMove_Tick;
        }
        public override void AddTankOnForm()
        {
            Tag = "EnemyTank";
            TankUpImage = (Bitmap)Image.FromFile("EnemyTank\\redTank1.png");
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
            if (form.enemyV2List.Contains(this) == false || form.tank == null)
            {
                TimerTankAutoFire.Stop();
                TimerDirectionGenerate.Stop();
                TimerTankMove.Stop();
                return;
            }
            if ((this.Location.X > form.tank.Location.X - form.tank.Width / 2 && this.Location.X < form.tank.Location.X + form.tank.Width / 2) ||
                (this.Location.Y > form.tank.Location.Y - form.tank.Width / 2 && this.Location.Y < form.tank.Location.Y + form.tank.Width / 2))
            {
                Fire(new Bullet(form, this, 17, 17, 6, "Bullet\\bullet.png"));
            }
        }
        public void TimerDirectionGenerate_Tick(object sender, EventArgs e)
        {
            if (form.tank == null || this.HP < 1)
            {
                TimerTankMove.Stop();
                TimerDirectionGenerate.Stop();
                TimerTankAutoFire.Stop();
                return;
            }
            int diffX = Math.Abs(form.tank.Location.X - this.Location.X);
            int diffY = Math.Abs(form.tank.Location.Y - this.Location.Y);
            if (diffX > diffY)
            {
                if (this.Location.X < form.tank.Location.X - Width)
                {
                    CurrentDirection = Direction.Right;
                }
                else
                {
                    CurrentDirection = Direction.Left;
                }
            }
            else
            {
                if (this.Location.Y < form.tank.Location.Y - Width)
                {
                    CurrentDirection = Direction.Down;
                }
                else
                {
                    CurrentDirection = Direction.Up;
                }
            }
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
                    Location = new Point(Location.X - 1, Location.Y);
                }
                if (CurrentDirection == Direction.Right)
                {
                    Image = TankRightImage;
                    Location = new Point(Location.X + 1, Location.Y);
                }
                if (CurrentDirection == Direction.Up)
                {
                    Image = TankUpImage;
                    Location = new Point(Location.X, Location.Y - 1);
                }
                if (CurrentDirection == Direction.Down)
                {
                    Image = TankDownImage;
                    Location = new Point(Location.X, Location.Y + 1);
                }
            }
        }
        public void Destroy()
        {
            form.Controls.Remove(this);
            form.enemyV2List.Remove(this);
            Animations.Explose(Location.X - Width / 2, Location.Y - Height / 2, form);

            int item = random.Next(2);
            if (item == 1 && form.tank.HaveSplashGun)
            {
                new AmmoSplash(form, Location.X - Width / 2, Location.Y - Height / 2);
            }
        }
    }
}
