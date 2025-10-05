using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mayın_Tarlası
{
    
    public partial class Form2 : Form
    {
        public int kalanKare = 62;
        private int oyunbitti = 0;
        
        private int saniye = 0;
        private async void timer1_Tick(object sender, EventArgs e)
        {
            while (oyunbitti == 0)
            {
                saniye++;
                label1.Text = "SÜRE: " + saniye.ToString();
                await Task.Delay(1000);
            }
        }
        public Form2()
        {
            InitializeComponent();
            timer1_Tick(this, new EventArgs());
            for (int i = 1; i <= 72; i++)
            {
                Button b1 = new Button();
                b1.Size = new Size(32, 32);
                b1.BackColor = Color.White;

                this.flowLayoutPanel1.Controls.Add(b1);
                b1.Name = i.ToString();
               b1.MouseUp += tiklandi;


            }
            
            mayinYerlestir();
         
            
        }
        public List<int> mayinlar = new List<int>();
        public void mayinYerlestir()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                int mayin = rnd.Next(1, 72);
                if (!mayinlar.Contains(mayin))
                {
                    mayinlar.Add(mayin);
                }
                else
                {
                    i--;
                }
            }

            //foreach (Control item in flowLayoutPanel1.Controls)
            //{
            //    if (item is Button)
            //    {
            //        if (mayinlar.Contains(Convert.ToInt32(item.Name)))
            //        {
            //            item.BackColor = Color.BlueViolet;
            //        }
            //    }
            //} MAYINLARI GÖSTER
        }
        
private void tiklandi(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (e.Button == MouseButtons.Left)
            {
                if(b.BackgroundImage != null) return;
                if (mayinlar.Contains(Convert.ToInt32(b.Name)))
                {
                    b.BackgroundImage = ımageList1.Images[0];
                    b.BackgroundImageLayout = ImageLayout.Zoom;
                    b.BackColor = Color.Red;
                    Kaybettiniz();
                }
                else
                {  if(b.BackColor == Color.LightGray) return;
                    b.BackColor = Color.LightGray;
                    int sayi = sayiAta(Convert.ToInt32(b.Name));
                    b.Text = sayi.ToString();
                    kalanKare--;
                    if (kalanKare == 0)
                    {
                        Kazandiniz();
                        
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if(b.Text != "") return;
                if (b.BackgroundImage != null)
                {
                    b.BackgroundImage = null;
                    this.bayrakSayac.Text = (Convert.ToInt32(this.bayrakSayac.Text) + 1).ToString();
                    return;
                }
                if (Convert.ToInt32(this.bayrakSayac.Text) == 0)
                {
                    MessageBox.Show("Daha fazla bayrak işareti koyamazsınız.");
                    return;
                }
                b.BackgroundImage = ımageList1.Images[1];
                b.BackgroundImageLayout = ImageLayout.Zoom;
                this.bayrakSayac.Text = (Convert.ToInt32(this.bayrakSayac.Text) - 1).ToString();
            }
        }
        public int sayiAta(int a)
        {
            int x = 0;
            if(a == 1)
            {
                if (mayinlar.Contains(2)) x++;
                if (mayinlar.Contains(7)) x++;
                if (mayinlar.Contains(8)) x++;
            }
            if(a == 6)
            {
                if (mayinlar.Contains(5)) x++;
                if (mayinlar.Contains(11)) x++;
                if (mayinlar.Contains(12)) x++;
            }
            else if (a == 67)
            {
                if (mayinlar.Contains(61)) x++;
                if (mayinlar.Contains(62)) x++;
                if (mayinlar.Contains(68)) x++;
            }
            else if (a == 72)
            {
                if (mayinlar.Contains(71)) x++;
                if (mayinlar.Contains(65)) x++;
                if (mayinlar.Contains(66)) x++;
            }
            else if (a % 6 == 1)
            {
                if (mayinlar.Contains(a - 6)) x++;
                if (mayinlar.Contains(a - 5)) x++;
                if (mayinlar.Contains(a + 1)) x++;
                if (mayinlar.Contains(a + 6)) x++;
                if (mayinlar.Contains(a + 7)) x++;
            }
            else if (a % 6 == 0)
            {
                if (mayinlar.Contains(a - 7)) x++;
                if (mayinlar.Contains(a - 6)) x++;
                if (mayinlar.Contains(a - 1)) x++;
                if (mayinlar.Contains(a + 5)) x++;
                if (mayinlar.Contains(a + 6)) x++;
            }
            else
            {
                if (mayinlar.Contains(a - 7)) x++;
                if (mayinlar.Contains(a - 6)) x++;
                if (mayinlar.Contains(a - 5)) x++;
                if (mayinlar.Contains(a - 1)) x++;
                if (mayinlar.Contains(a + 1)) x++;
                if (mayinlar.Contains(a + 5)) x++;
                if (mayinlar.Contains(a + 6)) x++;
                if (mayinlar.Contains(a + 7)) x++;
            }

            return x;

        }
        public async void Kazandiniz()
        {
            await Task.Delay(500);
            this.flowLayoutPanel1.Enabled = false;
            oyunbitti = 1;
            sureData.sureList.Add(saniye);
            for (int i= 0; i<mayinlar.Count; i++)
            {

                foreach (Control item in flowLayoutPanel1.Controls)
                {
                    if (item is Button)
                    {
                        if (mayinlar[i] == Convert.ToInt32(item.Name))
                        {
                            item.BackgroundImage = ımageList1.Images[0];
                            item.BackgroundImageLayout = ImageLayout.Zoom;
                            await Task.Delay(300);
                        }
                    }
                }
            }
            for (int i = 0; i < sureData.sureList.Count; i++)
            {
                if (sureData.sureList[i] <= sureData.sureList.Min())
                {
                    Application.OpenForms["Form1"].Controls["label2"].Text = "En iyi süre " + (sureData.sureList[i]).ToString() + "s";
                }
            }
            
            await Task.Delay(1500);
            MessageBox.Show("Tebrikler Oyunu Kazandınız");

            this.Close();
        }
        public async void Kaybettiniz()
        {
            this.flowLayoutPanel1.Enabled = false;
            oyunbitti = 1;
            for (int i = 0; i < mayinlar.Count; i++)
            {
                foreach (Control item in flowLayoutPanel1.Controls)
                {
                    if (item is Button)
                    {
                        if (mayinlar[i] == Convert.ToInt32(item.Name))
                        {
                            item.BackgroundImage = ımageList1.Images[0];
                            item.BackgroundImageLayout = ImageLayout.Zoom;
                            await Task.Delay(300);
                        }
                    }
                }
            }
            await Task.Delay(1500);
            MessageBox.Show("Oyunu Kaybettiniz");
            this.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        
    }
}
