using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;


namespace Dz_12_Millioner
{
    public partial class Form1 : Form
    {
        public Button[] b;
        public Button[] bA;

        public int x { get; set; }
        public int y { get; set; } 
        public int stop { get; set; }
        SoundPlayer soundAudio;

        public Form1()
        {
            InitializeComponent();

            b = new Button[15] { b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, b11, b12, b13, b14, b15 };
            bA = new Button[4] { bAnswer1, bAnswer2, bAnswer3, bAnswer4};

            for (int i = 0; i < b.Length; i++) b[i].Hide();

            y = -1;
            x = 0;
            stop = 0;

            bGalkin.Hide();
            bGalkinText.Hide();  
            bZnotok.Hide();
            bZnotokText.Hide(); 
            bQustion.Hide();
            bAnswer1.Hide();
            bAnswer2.Hide(); 
            bAnswer3.Hide();
            bAnswer4.Hide();
            bNew.Enabled = false;
            bp1.Enabled = false;
        }

        public void start()
        {
            soundAudio = new SoundPlayer(Resource1.begin);
            soundAudio.Play();

            bGalkin.Show();
            bGalkinText.Text = "СТАРТ";
            bGalkinText.Show();

            Task.Delay(3000).Wait();

            bQustion.Show();
            bAnswer1.Show();
            bAnswer2.Show();
            bAnswer3.Show();
            bAnswer4.Show();
              
            bGalkinText.Hide();
            bZnotok.Show();
            bp1.Enabled = true;
        }
          
        public void Game()
        {
            for (int j = 0; j < bA.Length; j++) bA[j].Enabled = true; 

            StreamReader sr = new StreamReader("question.txt", Encoding.UTF8);
            string line;
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            {
                if (i == x) bQustion.Text = line;
                if (i == x + 1) bAnswer1.Text = line;
                if (i == x + 2) bAnswer2.Text = line;
                if (i == x + 3) bAnswer3.Text = line;
                else if (i == x + 4)
                {
                    bAnswer4.Text = line;
                    x += 5; y++;
                    break;
                }
                i++;
            }
            sr.Close();
        }

        public int Check(int d)
        {
            StreamReader sr = new StreamReader("Answers.txt", Encoding.UTF8);
            string line;
            int i = 0;
            while ((line = sr.ReadLine()) != null)
            { 
                if (i == y)
                {
                    if (d == Convert.ToInt32(line)) d = 1;
                    else d = 0;
                    break;
                }
                i++;
            }
            sr.Close();
            return d;
        }

        public void WinOrNo(int f)
        {
            if (f == 1)
            {
                bGalkinText.Show();
                bGalkinText.Text = "Правильно";
                b[y].Show();
                soundAudio = new SoundPlayer(Resource1._true);
                soundAudio.Play();
                Task.Delay(4000).Wait();

                if (y == 14)
                {
                    soundAudio = new SoundPlayer(Resource1.winner);
                    soundAudio.Play();
                    bGalkinText.Text = "Вы выиграли " + listBox1.Items[16 - y];
                    Task.Delay(3000).Wait();
                    bGalkin.Hide();
                    bGalkinText.Hide();
                    bZnotok.Hide();
                    bZnotokText.Hide();
                    bQustion.Hide();
                    bAnswer1.Hide();
                    bAnswer2.Hide();
                    bAnswer3.Hide();
                    bAnswer4.Hide();
                    for (int i = 0; i < b.Length; i++) b[i].Hide();
                }
            } 
            else
            {
                bGalkinText.Show();
                bGalkinText.Text = "Не Правильно";
                soundAudio = new SoundPlayer(Resource1._false);
                soundAudio.Play();
                Task.Delay(3000).Wait(); 
                bGalkinText.Text = "Вы выиграли " + listBox1.Items[15 - y]; 
                Task.Delay(3000).Wait();
                bGalkin.Hide();
                bGalkinText.Hide();
                bZnotok.Hide();
                bZnotokText.Hide();
                bQustion.Hide();
                bAnswer1.Hide();
                bAnswer2.Hide();
                bAnswer3.Hide();
                bAnswer4.Hide();
                for (int i = 0; i < b.Length; i++) b[i].Hide();
            }
        }

        private void играToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (x == 0) start(); 
            Game();
            играToolStripMenuItem.Enabled = false;
            bNew.Enabled = true;
        } 

        private void bAnswer1_Click(object sender, EventArgs e)
        {
            int d = 1;
            bZnotokText.Show();
            bZnotokText.Text = "Я думаю это " + bAnswer1.Text;
            int f = Check(d);
            WinOrNo(f);
            Game();
        }

        private void bAnswer2_Click(object sender, EventArgs e)
        {
            int d = 2;
            bZnotokText.Show();
            bZnotokText.Text = "Кажется " + bAnswer2.Text;
            int f = Check(d);
            WinOrNo(f);
            Game();
        }

        private void bAnswer3_Click(object sender, EventArgs e)
        {
            int d = 3;
            bZnotokText.Show();
            bZnotokText.Text = "Наверно " + bAnswer3.Text;
            int f = Check(d);
            WinOrNo(f);
            Game();
        }

        private void bAnswer4_Click(object sender, EventArgs e)
        {
            int d = 4;
            bZnotokText.Show();
            bZnotokText.Text = "Уверен " + bAnswer4.Text;
            int f = Check(d);
            WinOrNo(f);
            Game();
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bNew_Click(object sender, EventArgs e)
        {
            x = 0; 
            y = -1;
            bZnotokText.Hide();
            for (int i = 0; i < b.Length; i++) b[i].Hide();
            if (x == 0) start();
            Game();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            if (stop == 0)
            {
                bAnswer1.Enabled = false;
                bAnswer2.Enabled = false;
                bAnswer3.Enabled = false;
                bAnswer4.Enabled = false;
                stop++;
                bStop.Text = "СТАРТ";
            }
            else
            {
                bAnswer1.Enabled = true;
                bAnswer2.Enabled = true;
                bAnswer3.Enabled = true;
                bAnswer4.Enabled = true;
                stop = 0;
                bStop.Text = "СТОП";
            } 
        }

        private void bp1_Click(object sender, EventArgs e)
        {
            int[] chek1 = new int[4] { 0, 0, 0, 0 };
            int[] chek2 = new int[4] { 1, 2, 3, 4 };

            for (int i = 0; i < chek1.Length; i++)
                chek1[i] = Check(chek2[i]);

            Random r = new Random();
            int t = 0;
            int flag = 0;
            for (int i = 0; i < 100; i++)
            {
                if (flag == 2) break; 

                t = r.Next(0, 3);
                if (chek1[t] == 0)
                {
                    chek1[t] = 1;
                    bA[t].Enabled = false; 
                    flag++;
                }
            }
            bp1.Enabled = false;
        }

        private void bp2_Click(object sender, EventArgs e)
        {
            soundAudio = new SoundPlayer(Resource1.zvonok1);
            soundAudio.Play();
        }

        private void bp3_Click(object sender, EventArgs e)
        {
            soundAudio = new SoundPlayer(Resource1.zal);
            soundAudio.Play();
        }

        private void админToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAdmin f = new FormAdmin();
            f.ShowDialog();
        }
    } 
}
