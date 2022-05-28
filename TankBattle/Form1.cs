using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//TODO: Ввести босса, дроп оружия с него, после подбора оружия на форме начинают появлятся патроны к нему.
namespace TankBattle
{
    public partial class Form1 : Form
    {

        private int points;

        public int Points
        {
            get { return points; }
            set
            {
                points = value;
                labelPoints.Text = $"Points: {Points}";
            }
        }


        public Random Random = new Random();
        public Tank tank;

        Timer timerReloadTank = new Timer();
        bool tankCanBulletFire = false;

        Timer timerBossSpawn = new Timer();

        public List<EnemyTank> enemyList = new List<EnemyTank>();
        public List<EnemyTankLogic1> enemyV2List = new List<EnemyTankLogic1>();
        

        public bool shieldOnForm = false;

        public Form1()
        {
            InitializeComponent();
            tank = new Tank(this, 3, 350, 500);
            tank.AddTankOnForm();

            timerReloadTank.Interval = 1000;
            timerReloadTank.Tick += TimerReloadTank_Tick;
            timerReloadTank.Start();

            timerBossSpawn.Interval = 5000;
            timerBossSpawn.Tick += TimerBossSpawn_Tick;
           // timerBossSpawn.Start();
        }

        private void TimerReloadTank_Tick(object sender, EventArgs e)
        {
            tankCanBulletFire = true;
            timerReloadTank.Stop();
        }

        private void Start_Tick(object sender, EventArgs e)
        {
            if (tank is null)
            {
                timerEnemySpawn.Stop();
                timerStart.Stop();
                MessageBox.Show("GAME OVER!");
                Close();
                return;
            }
            tank.MoveTank();

            foreach (var pb1 in Controls)
            {
                PictureBox t = pb1 as PictureBox; //отсечение всего, кроме pictureBox
                if (t == null) continue;

                foreach (var pb2 in Controls)
                {
                    PictureBox b = pb2 as PictureBox; //отсечение всего, кроме pictureBox
                    if (b == null) continue;

                    if (t.Tag == "EnemyTank" && b.Tag == "Bullet" && (b as Bullet).CurrentTank == tank && t.Bounds.IntersectsWith(b.Bounds))
                    {
                        Controls.Remove(b);
                        (t as Tank).HP -= 1;
                        if ((t as Tank).HP == 0)
                        {
                            //Controls.Remove(t);
                            //enemyList.Remove(t as EnemyTank);
                            //enemyV2List.Remove(t as EnemyTankLogic1);
                            (t as EnemyTank)?.Destroy();
                            (t as EnemyTankLogic1)?.Destroy();
                            if ((b as Bullet).CurrentTank == tank) Points++;
                        }
                    }
                    if (t.Tag == "EnemyBoss1" && b.Tag == "Bullet" && (b as Bullet).CurrentTank == tank && t.Bounds.IntersectsWith(b.Bounds))
                    {
                        Controls.Remove(b);
                        (t as Tank).HP -= 1;
                        if ((t as Tank).HP == 0)
                        {
                            (t as EnemyBoss1)?.Destroy();
                            if ((b as Bullet).CurrentTank == tank) Points+=50;
                            new SplashGun(this, (t as Tank).Location.X, (t as Tank).Location.Y);
                            timerEnemySpawn.Start();
                        }
                    }
                    if (t.Tag == "Tank" && b.Tag == "Bullet" && t.Bounds.IntersectsWith(b.Bounds))
                    {
                        Controls.Remove(b);
                        if ((t as Tank).IsShielded)
                        {
                            (t as Tank).IsShielded = false;
                        }
                        else
                            (t as Tank).HP -= 1;

                        if ((t as Tank).HP == 0)
                        {
                            tank = null;
                        }
                    }
                    if (t.Tag == "Tank" && b.Tag == "Shield" && t.Bounds.IntersectsWith(b.Bounds))
                    {
                        (t as Tank).IsShielded = true;
                        Controls.Remove(b as Shield);
                        shieldOnForm = false;
                    }
                    if (t.Tag == "Tank" && b.Tag == "AmmoSplash" && t.Bounds.IntersectsWith(b.Bounds))
                    {
                        Controls.Remove(b as AmmoSplash);
                        (t as Tank).AmmoSplash += 1;
                    }
                    if (t.Tag == "Tank" && b.Tag == "SplashGun" && t.Bounds.IntersectsWith(b.Bounds))
                    {
                        (t as Tank).TankPower++;
                        (t as Tank).HaveSplashGun = true;
                        Controls.Remove(b as SplashGun);
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (tank == null)
            {
                return;
            }
            if (e.KeyCode == Keys.Right)
            {
                tank.isRight = true;
                tank.isDown = false;
                tank.isLeft = false;
                tank.isUp = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                tank.isRight = false;
                tank.isDown = false;
                tank.isLeft = true;
                tank.isUp = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                tank.isRight = false;
                tank.isDown = false;
                tank.isLeft = false;
                tank.isUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                tank.isRight = false;
                tank.isDown = true;
                tank.isLeft = false;
                tank.isUp = false;
            }
            if (e.KeyCode == Keys.Space && tankCanBulletFire)
            {
                Bullet bullet = new Bullet(this, tank, 7, 30, 5, "Bullet\\yellowBullet.png");
                tank?.Fire(bullet);
                Animations.Fire(bullet.Location.X - 25 + bullet.Width / 2, bullet.Location.Y - 25 + bullet.Height / 2, this);
                tankCanBulletFire = false;
                timerReloadTank.Start();
            }
            if (e.KeyCode == Keys.Z && tank.HaveSplashGun && tank.AmmoSplash > 0)
            {
                tank.FireSplash(new BulletSplash(this, tank, 7, 30, 5, "Bullet\\bulletSplash.png"));
                tank.AmmoSplash--;
                timerReloadTank.Start();
            }
            if (e.KeyCode == Keys.X /*&& tank.HaveSplasGun*/)
            {
                tank.FireSin(new Bullet(this, tank, 8, 23, 5, "Bullet\\bulletSin.png"));
                tankCanBulletFire = false;
                timerReloadTank.Start();
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) tank.isRight = false;
            if (e.KeyCode == Keys.Left) tank.isLeft = false;
            if (e.KeyCode == Keys.Up) tank.isUp = false;
            if (e.KeyCode == Keys.Down) tank.isDown = false;
        }

        private void TimerEnemySpawn_Tick(object sender, EventArgs e)
        {
            if (enemyList.Count < 1)
            {
                EnemyTank enemy = new EnemyTank(this, 30, Random.Next(this.Width - 100), Random.Next(this.Height - 100));
                enemy.AddTankOnForm();
                enemyList.Add(enemy);
            }
            if (Points > 0)
            {
                if (enemyV2List.Count == 0 && Points % 5 == 0)
                {
                    EnemyTankLogic1 enemyV2 = new EnemyTankLogic1(this, 30, Random.Next(this.Width - 100), Random.Next(this.Height - 100));
                    enemyV2.AddTankOnForm();
                    enemyV2List.Add(enemyV2);
                }
                if (tank.IsShielded == false && shieldOnForm == false && Points % 3 == 0)
                {
                    new Shield(this, Random.Next(this.Width - 100), Random.Next(this.Height - 100));
                }
            }
            if (points > 9 && points < 20)
            {
                timerBossSpawn.Start();
                timerEnemySpawn.Stop();
            }
        }
        private void TimerBossSpawn_Tick(object sender, EventArgs e)
        {
            EnemyBoss1 enemyBoss1 = new EnemyBoss1(this, 13, 20, 20);
            enemyBoss1.AddTankOnForm();
            timerBossSpawn.Stop();
        }
    }
}
