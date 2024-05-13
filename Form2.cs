using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Тетрис;


namespace Тетрис
{
    public partial class Form2 : Form
    {
        string playerName;
        public Form2()
        {
            InitializeComponent();       
            playerName = Microsoft.VisualBasic.Interaction.InputBox("Введите имя игрока", "Настройка игрока", "");
            if (playerName == "")
            {
                playerName = "Новый игрок";
            }
            this.KeyUp += new KeyEventHandler(KeyFunc);
            Init();
        }
        public void Init()
        {
            this.DoubleBuffered = true;
            this.Text = "Тетрис: Текущий игрок - " + playerName;
            MapController.size = 25;
            MapController.score = 0;
            MapController.linesRemoved = 0;
            MapController.currentShape = new Figure(3, 0);
            MapController.Interval = 500;
            label1.Text = "Очки: " + MapController.score;
            label2.Text = "Линии: " + MapController.linesRemoved;
            timer1.Interval = MapController.Interval;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
            Invalidate();
            MapController.ClearMap();
        }

        private void KeyFunc(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (!MapController.IsIntersects())
                    {
                        MapController.ResetArea();
                        MapController.currentShape.RotateShape();
                        MapController.Merge();
                        Invalidate();
                    }
                    break;
                case Keys.Down:
                    timer1.Interval = 10;
                    break;
                case Keys.Right:
                    if (!MapController.CollideHor(1))
                    {
                        MapController.ResetArea();
                        MapController.currentShape.MoveRight();
                        MapController.Merge();
                        Invalidate();
                    }
                    break;
                case Keys.Left:
                    if (!MapController.CollideHor(-1))
                    {
                        MapController.ResetArea();
                        MapController.currentShape.MoveLeft();
                        MapController.Merge();
                        Invalidate();
                    }
                    break;
            }
        }   
        private void update(object sender, EventArgs e)
        {
            MapController.ResetArea();
            if(!MapController.Collide())
            {
                MapController.currentShape.MoveDown();
            }
            else
            {
                MapController.Merge();
                MapController.SliceMap(label1, label2);
                timer1.Interval = MapController.Interval;
                MapController.currentShape.ResetShape(3, 0);
                if(MapController.Collide())
                {
                    
                    MapController.ClearMap();
                    timer1.Tick -= new EventHandler(update);
                    timer1.Stop();
                    int gameResult = MapController.score;
                    Form3 coinsForm = new Form3();
                    Data.money = gameResult / 5;
                    this.Close();
                    MessageBox.Show("Ваш результат: " + MapController.score);
                }
            }
            MapController.Merge();
            Invalidate();
        }
        private void OnPaint(object sender, PaintEventArgs e)
        {
            MapController.DrawGrid(e.Graphics);
            MapController.DrawMap(e.Graphics);
            MapController.ShowNextShape(e.Graphics);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }
        private void OnPauseButton(object sender, EventArgs e)
        {
            var pressedButton = sender as ToolStripMenuItem;
            if (timer1.Enabled)
            {
                pressedButton.Text = "Продолжить";
                timer1.Stop();
            }
            else
            {
                pressedButton.Text = "Пауза";
                timer1.Start();
            }
        }
        private void OnAgainButton(object sender, EventArgs e)
        {
            timer1.Tick -= new EventHandler(update);
            timer1.Stop();
            MapController.ClearMap();
            Init();
        }
        private void OnInfoPressed(object sender, EventArgs e)
        {
            string infoString = "";
            infoString = "Для управления фигурами используйте стрелочку 'Влево/Вправо'.\n";
            infoString += "Чтобы ускорить падение фигуры - нажмите 'Вниз'.\n";
            infoString += "Для поворота фигуры используйте 'Вверх'.\n";
            MessageBox.Show(infoString, "Справка");
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void OnClose(object sender, EventArgs e)
        {
            var pressedButton = sender as ToolStripMenuItem;
            if (timer1.Enabled)
            {
                pressedButton.Text = "Выйти";
                timer1.Stop();
                this.Close();
            }
        }
    }
}
