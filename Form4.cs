using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Тетрис
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void button_download_Click(object sender, EventArgs e)
        {
            //Создаём соединение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            //Выполняем запрос к бд
            dbConnection.Open();//открываем соединение
            string query = "SELECT * FROM table_tetris";//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда
            OleDbDataReader dbReader = dbCommand.ExecuteReader();//считываем данные

            //проверяем данные
            if(dbReader.HasRows == false)
            {
                MessageBox.Show("Данные не найдены!","Ошибка!");
            }
            else
            {
                //запишем данные таблицы в форму
                while(dbReader.Read())
                {
                    //выводим данные
                    dataGridView1.Rows.Add(dbReader["ID"], dbReader["Игрок"], dbReader["Очки"], dbReader["Линии"]);
                }
            }
            //закрываем соединение с бд
            dbReader.Close();
            dbConnection.Close();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            //проверим количество выбранных строк
            if(dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!","Внимание!");
                return;
            }
            //запомним выбранную строку
            int index = dataGridView1.SelectedRows[0].Index;

            //проверим данные в таблице
            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null )
            {
                MessageBox.Show("Не все данные введены!","Внимание!");
                return;
            }
            //считываем данные
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string name = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string score = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string line = dataGridView1.Rows[index].Cells[3].Value.ToString();

            //Создаём соединение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            //Выполняем запрос к бд
            dbConnection.Open();//открываем соединение
            string query = "INSERT INTO table_tetris VALUES (" + id + ", '" + name + "', " + score + ", " + line + ")";//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда

            //выполняем запрос
            if(dbCommand.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            }
            else
            {
                MessageBox.Show("Данные добавлены!","Внимание!");
            }

            //закрываем соединение с бд
            dbConnection.Close();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            //проверим количество выбранных строк
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }
            //запомним выбранную строку
            int index = dataGridView1.SelectedRows[0].Index;

            //проверим данные в таблице
            if (dataGridView1.Rows[index].Cells[0].Value == null ||
                dataGridView1.Rows[index].Cells[1].Value == null ||
                dataGridView1.Rows[index].Cells[2].Value == null ||
                dataGridView1.Rows[index].Cells[3].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }
            //считываем данные
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();
            string name = dataGridView1.Rows[index].Cells[1].Value.ToString();
            string score = dataGridView1.Rows[index].Cells[2].Value.ToString();
            string line = dataGridView1.Rows[index].Cells[3].Value.ToString();

            //Создаём соединение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            //Выполняем запрос к бд
            dbConnection.Open();//открываем соединение
            string query = "UPDATE table_tetris SET Name = '" + name + "',Score=" + score + ",Line=" + line + " WHERE id = " + id;//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда

            //выполняем запрос
            if (dbCommand.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            }
            else
            {
                MessageBox.Show("Данные изменены!", "Внимание!");
            }

            //закрываем соединение с бд
            dbConnection.Close();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            //проверим количество выбранных строк
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show("Выберите одну строку!", "Внимание!");
                return;
            }
            //запомним выбранную строку
            int index = dataGridView1.SelectedRows[0].Index;

            //проверим данные в таблице
            if (dataGridView1.Rows[index].Cells[0].Value == null)
            {
                MessageBox.Show("Не все данные введены!", "Внимание!");
                return;
            }
            //считываем данные
            string id = dataGridView1.Rows[index].Cells[0].Value.ToString();

            //Создаём соединение
            string connectionString = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=Database.mdb";
            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            //Выполняем запрос к бд
            dbConnection.Open();//открываем соединение
            string query = "DELETE FROM table_tetris WHERE id = " + id;//строка запроса
            OleDbCommand dbCommand = new OleDbCommand(query, dbConnection);//команда

            //выполняем запрос
            if (dbCommand.ExecuteNonQuery() != 1)
            {
                MessageBox.Show("Ошибка выполнения запроса!", "Ошибка!");
            }
            else
            {
                MessageBox.Show("Данные удалены!", "Внимание!");
                //удаляем данные из таблицы
                dataGridView1.Rows.RemoveAt(index);
            }

            //закрываем соединение с бд
            dbConnection.Close();
        }
    }
}
