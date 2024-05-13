using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Тетрис
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            UpdateCoinsLabel();
        }      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(Data.money >= 3)
            {
                Data.money -= 3;
                this.Close();
                Form2 form2 = new Form2();
                form2.BackColor = Color.Pink;
                form2.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Data.money >= 2)
            {
                Data.money -= 2;
                this.Close();
                Form2 form2 = new Form2();
                form2.BackColor = Color.FromArgb(255, 224, 192);
                form2.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Data.money >= 2)
            {
                Data.money -= 2;
                this.Close();
                Form2 form2 = new Form2();
                form2.BackColor = Color.FromArgb(192, 255, 255);
                form2.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }            
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.BackColor = Color.White;
            form2.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 form2 = new Form2();
            form2.BackColor = Color.Gray;
            form2.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (Data.money >= 3)
            {
                Data.money -= 3;
                this.Close();
                Form2 form2 = new Form2();
                form2.BackColor = Color.PaleGreen;
                form2.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Data.money >= 4)
            {
                Data.money -= 4;
                this.Close();
                Form2 form2 = new Form2();
                form2.BackColor = Color.FromArgb(255, 192, 255);
                form2.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }       
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (Data.money >= 4)
            {
                Data.money -= 4;
                this.Close();
                Form2 form2 = new Form2();
                form2.BackColor = Color.MediumPurple;
                form2.Show();
            }
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }
        }
        private int _gameResult;
        public int GameResult
        {
            get { return _gameResult; }
            set
            {
                _gameResult = value;
                UpdateCoinsLabel();
            }
        }
        private void UpdateCoinsLabel()
        {
            label1.Text = $"Монеты: {Data.money}";          
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }        
    }
}
