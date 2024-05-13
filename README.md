# Тетрис
Тетрис — это компьютерная игра, которая представляет собой головоломку, построенную на использовании геометрических фигур «тетрамино» — разновидности полимино, состоящих из четырёх квадратов.

Данная игра была разработана с целью усовершенствования некоторых функций, для своеобразного оттачивания опыта в программировании и выполнения проектной работы.

## Задачи:

_При разработке проекта были поставлены следующие задачи, которые должны быть реализованы в программе:_
* Создание удобного приложения, которое будет понятно любому пользователю на интуитивном уровне;
* Разработка основных концепций игры, для её полноценной работоспособности;
* Добавление дополнительных элементов, которые могут разнообразить игру.
* Построить и подключить базу данных, в которой будет храниться статистика с результатами игр.

_Чтобы решить эти задачи нужно было разработать следующие функции:_
* Функция для понятного взаимодействия пользователя с игрой;
* Функции построения основной логики игры, при помощи которых игра работает;
* Функция для сбора статистики, чтобы пользователи могли отслеживать свои результаты и по мере игры улучшать их.
* Функции изменения цвета фона в игре, благодаря этому любой пользователь может выбрать для себя тот цвет, который ему больше всего нравится.
* Функции приятного интерфейса, которые в свою очередь будут приятными для глаз пользователя, а также для удобной навигации в приложении.

## Разработка программы:

1. Для реализации приятного дизайна в приложении были подобраны специальные фоны, изображение которых соответствуют самой игре. Это в свою очередь сразу даёт понять пользователю о том, какое приложение он сейчас использует. Также, были добавлены остальные элементы, с которыми пользователь может взаимодействовать, цвета к ним были подобраны таким образом, чтобы они выделялись на фоне всего приложения и не сливались. БЫла реализована концепция игры, при помощи которой пользователь может играть в игру и взаимодействовать с ней.

_Часть кода из игры:_
```csharp
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
```

2. В магазине приложения были добавлены приятные и лёгкие цвета, которые не нагружают глаза и в то же время не сливаются с цветом самих фигур. Благодаря этому пользователь точно сможет найти приятный для себя цвет, тем самым разнообразить цветовой фон игры.

_Часть кода из магазина:_
```csharp
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
```

3. На отдельной форме со статистикой были реализованы все инструменты, которые требуются для работы с базой данных в которой хранятся результаты игр пользователя. За счёт этого он может отслеживать свои рекорды и стремиться улучшать их.

_Часть кода с подключением к базе данных:_
```csharp
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
```
