namespace TankBattle
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.timerEnemySpawn = new System.Windows.Forms.Timer(this.components);
            this.labelHP = new System.Windows.Forms.Label();
            this.labelPoints = new System.Windows.Forms.Label();
            this.labelShield = new System.Windows.Forms.Label();
            this.labelAmmoSplash = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timerStart
            // 
            this.timerStart.Enabled = true;
            this.timerStart.Interval = 10;
            this.timerStart.Tick += new System.EventHandler(this.Start_Tick);
            // 
            // timerEnemySpawn
            // 
            this.timerEnemySpawn.Enabled = true;
            this.timerEnemySpawn.Interval = 4000;
            this.timerEnemySpawn.Tick += new System.EventHandler(this.TimerEnemySpawn_Tick);
            // 
            // labelHP
            // 
            this.labelHP.AutoSize = true;
            this.labelHP.BackColor = System.Drawing.Color.Transparent;
            this.labelHP.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHP.ForeColor = System.Drawing.Color.Red;
            this.labelHP.Location = new System.Drawing.Point(897, 29);
            this.labelHP.Name = "labelHP";
            this.labelHP.Size = new System.Drawing.Size(62, 29);
            this.labelHP.TabIndex = 0;
            this.labelHP.Text = "5HP";
            // 
            // labelPoints
            // 
            this.labelPoints.AutoSize = true;
            this.labelPoints.BackColor = System.Drawing.Color.Transparent;
            this.labelPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPoints.ForeColor = System.Drawing.Color.Black;
            this.labelPoints.Location = new System.Drawing.Point(12, 532);
            this.labelPoints.Name = "labelPoints";
            this.labelPoints.Size = new System.Drawing.Size(51, 20);
            this.labelPoints.TabIndex = 1;
            this.labelPoints.Text = "Счёт:";
            // 
            // labelShield
            // 
            this.labelShield.AutoSize = true;
            this.labelShield.BackColor = System.Drawing.Color.Transparent;
            this.labelShield.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelShield.ForeColor = System.Drawing.Color.Blue;
            this.labelShield.Location = new System.Drawing.Point(898, 65);
            this.labelShield.Name = "labelShield";
            this.labelShield.Size = new System.Drawing.Size(0, 20);
            this.labelShield.TabIndex = 2;
            // 
            // labelAmmoSplash
            // 
            this.labelAmmoSplash.AutoSize = true;
            this.labelAmmoSplash.BackColor = System.Drawing.Color.Transparent;
            this.labelAmmoSplash.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAmmoSplash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelAmmoSplash.Location = new System.Drawing.Point(136, 532);
            this.labelAmmoSplash.Name = "labelAmmoSplash";
            this.labelAmmoSplash.Size = new System.Drawing.Size(0, 20);
            this.labelAmmoSplash.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.labelAmmoSplash);
            this.Controls.Add(this.labelShield);
            this.Controls.Add(this.labelPoints);
            this.Controls.Add(this.labelHP);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerEnemySpawn;
        public System.Windows.Forms.Timer timerStart;
        public System.Windows.Forms.Label labelHP;
        public System.Windows.Forms.Label labelPoints;
        public System.Windows.Forms.Label labelShield;
        public System.Windows.Forms.Label labelAmmoSplash;
    }
}

