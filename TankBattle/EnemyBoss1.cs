using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public class EnemyBoss1 : Tank, IEnemy
    {
        public Timer TimerDirectionGenerate { get; set; } = new Timer();
        public Timer TimerTankAutoFire { get; set; } = new Timer();
        public Timer TimerTankMove { get; set; } = new Timer();
        ProgressBar progressHP;
        private int hp = 10;
        public override int HP
        {
            get => hp;
            set
            {
                hp = value;
                progressHP.Value = hp;
            }
        } 
        public EnemyBoss1(Form1 form, int speed, int xLocation, int yLocation) : base(form, speed, xLocation, yLocation)
        {
            Tag = "EnemyBoss1";
            Size = new Size(50, 50);
            progressHP = new ProgressBar();
            progressHP.Size = new Size(form.Width, 20);
            progressHP.Maximum = HP;
            progressHP.Value = HP;
            progressHP.Step = HP / 10;
            progressHP.Location = new Point(0, 0);        

            TimerDirectionGenerate = new Timer();
            TimerDirectionGenerate.Interval = 325;
            TimerDirectionGenerate.Tick += TimerDirectionGenerate_Tick;

            TimerTankAutoFire = new Timer();
            TimerTankAutoFire.Interval = 777;
            TimerTankAutoFire.Tick += TimerTankAutoFire_Tick;

            TimerTankMove = new Timer();
            TimerTankMove.Interval = speed;
            TimerTankMove.Tick += TimerTankMove_Tick;
        }
        public override void AddTankOnForm()
        {
            TankDownImage = (Bitmap)Image.FromFile("Boss\\boss1.png");
            Image = TankDownImage;
            form.Controls.Add(progressHP);
            form.Controls.Add(this);
            TimerDirectionGenerate.Start();
            TimerTankAutoFire.Start();
            TimerTankMove.Start();
        }
        public void TimerDirectionGenerate_Tick(object sender, EventArgs e)
        {
            if (form.tank == null || this.HP < 1)
            {
                TimerTankAutoFire.Stop();
                TimerDirectionGenerate.Stop();
                TimerTankMove.Stop();
                return;
            }
            if (this.Location.X + this.Width / 2 < form.tank.Location.X + form.tank.Width / 2)
            {
                CurrentDirection = Direction.Right;
            }
            else if (this.Location.X + this.Width / 2 > form.tank.Location.X + form.tank.Width / 2)
            {
                CurrentDirection = Direction.Left;
            }
            else
                CurrentDirection = Direction.Stop;
        }

        public void TimerTankAutoFire_Tick(object sender, EventArgs e)
        {
            Image = TankLeftImage;
            Fire(new Bullet(form, this, 17, 17, 10, "Bullet\\bullet.png"));
            Image = TankRightImage;
            Fire(new Bullet(form, this, 17, 17, 10, "Bullet\\bullet.png"));
            Image = TankDownImage;
            FireSplash(new BulletSplash(form, this, 8, 32, 4, "Bullet\\bulletSplash.png"));
        }

        public void TimerTankMove_Tick(object sender, EventArgs e)
        {
            MoveTank();
        }
        public override void MoveTank()
        {
            if (form.tank != null)
            {
                if (CurrentDirection == Direction.Left)
                {
                    Image = TankDownImage;
                    Location = new Point(Location.X - 5, Location.Y);
                }
                if (CurrentDirection == Direction.Right)
                {
                    Image = TankDownImage;
                    Location = new Point(Location.X + 5, Location.Y);
                }
            }
        }
        public void Destroy()
        {
            form.Controls.Remove(progressHP);
            form.Controls.Remove(this);
            Animations.Explose(Location.X - Width / 2, Location.Y - Height / 2, form);
        }
    }
}
