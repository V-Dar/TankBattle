using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankBattle
{
    public class BulletSplash : PictureBox
    {
        public int Speed { get; set; }
        public string ImagePath { get; set; }
        public Bitmap BulletUpImage { get; private set; }
        public Bitmap BulletDownImage { get; private set; }
        public Bitmap BulletLeftImage { get; private set; }
        public Bitmap BulletRightImage { get; private set; }
        readonly Form1 form;
        public Tank CurrentTank { get; set; }
        List<Bullet> bulletSplashes = new List<Bullet>();
        Timer timerFlyBullet = new Timer();
        public BulletSplash(Form1 form, Tank currentTank, int bulletWidth, int bulletHeight, int speed, string imageBullet)
        {
            this.form = form;
            CurrentTank = currentTank;
            Speed = speed;
            bulletSplashes.Add(new Bullet(form, CurrentTank, bulletWidth, bulletHeight, Speed, imageBullet));
            bulletSplashes.Add(new Bullet(form, CurrentTank, bulletWidth, bulletHeight, Speed, imageBullet));
            bulletSplashes.Add(new Bullet(form, CurrentTank, bulletWidth, bulletHeight, Speed, imageBullet));
        }
        public void StartFly()
        {
            foreach (var bullet in bulletSplashes)
            {
                form.Controls.Add(bullet);
            }

            timerFlyBullet.Interval = 5;
            timerFlyBullet.Tick += BulletFlyTick;
            timerFlyBullet.Start();
        }
        private void BulletFlyTick(object sender, EventArgs e)
        {
            if (bulletSplashes[0].Image == bulletSplashes[0].BulletLeftImage) bulletSplashes[0].Left -= Speed;
            else if (bulletSplashes[0].Image == bulletSplashes[0].BulletRightImage) bulletSplashes[0].Left += Speed;
            else if (bulletSplashes[0].Image == bulletSplashes[0].BulletUpImage) bulletSplashes[0].Top -= Speed;
            else if (bulletSplashes[0].Image == bulletSplashes[0].BulletDownImage) bulletSplashes[0].Top += Speed;
            if (bulletSplashes[0].Left < 0 || bulletSplashes[0].Left > form.Width || bulletSplashes[0].Top < 0 || bulletSplashes[0].Top > form.Height)
            {
                form.Controls.Remove(bulletSplashes[0]);
            }
            if (bulletSplashes[1].Image == bulletSplashes[1].BulletLeftImage)
            {
                bulletSplashes[1].Location = new Point(bulletSplashes[1].Location.X - Speed, bulletSplashes[1].Location.Y - Speed/2);
            }
            else if (bulletSplashes[1].Image == bulletSplashes[1].BulletRightImage)
            {
                bulletSplashes[1].Location = new Point(bulletSplashes[1].Location.X + Speed, bulletSplashes[1].Location.Y - Speed/2);
            }
            else if (bulletSplashes[1].Image == bulletSplashes[1].BulletUpImage)
            {
                bulletSplashes[1].Location = new Point(bulletSplashes[1].Location.X + Speed/2, bulletSplashes[1].Location.Y - Speed);
            }
            else if (bulletSplashes[1].Image == bulletSplashes[1].BulletDownImage)
            {
                bulletSplashes[1].Location = new Point(bulletSplashes[1].Location.X + Speed/2, bulletSplashes[1].Location.Y + Speed);
            }
            if (bulletSplashes[1].Left < 0 || bulletSplashes[1].Left > form.Width || bulletSplashes[1].Top < 0 || bulletSplashes[1].Top > form.Height)
            {
                form.Controls.Remove(bulletSplashes[1]);
            }

            if (bulletSplashes[2].Image == bulletSplashes[2].BulletLeftImage)
            {
                bulletSplashes[2].Location = new Point(bulletSplashes[2].Location.X - Speed, bulletSplashes[2].Location.Y + Speed/2);
            }
            else if (bulletSplashes[2].Image == bulletSplashes[2].BulletRightImage)
            {
                bulletSplashes[2].Location = new Point(bulletSplashes[2].Location.X + Speed, bulletSplashes[2].Location.Y + Speed/2);
            }
            else if (bulletSplashes[2].Image == bulletSplashes[2].BulletUpImage)
            {
                bulletSplashes[2].Location = new Point(bulletSplashes[2].Location.X - Speed/2, bulletSplashes[2].Location.Y - Speed);
            }
            else if (bulletSplashes[2].Image == bulletSplashes[2].BulletDownImage)
            {
                bulletSplashes[2].Location = new Point(bulletSplashes[2].Location.X - Speed/2, bulletSplashes[2].Location.Y + Speed);
            }
            if (bulletSplashes[2].Left < 0 || bulletSplashes[2].Left > form.Width || bulletSplashes[2].Top < 0 || bulletSplashes[2].Top > form.Height)
            {
                form.Controls.Remove(bulletSplashes[2]);
            }
            if ((form.Controls.Contains(bulletSplashes[0]) || form.Controls.Contains(bulletSplashes[1]) || form.Controls.Contains(bulletSplashes[2])) == false)
            {
                timerFlyBullet.Stop();
            }
        }
    }
}
