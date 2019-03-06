using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wave_alg
{
    public partial class Form1 : Form
    {
        int width;
        int height;//размеры матрицы MxN
        int distance_x;
        int distance_y;//расстояние между кнопками
        Point start = new Point(); // стартовая точка 
        Point end = new Point(); // конечная точка
        Button[,] button_array = new Button[100,100];
        int counter=0; //счетчик кликов
        int[,] matrix = new int[100, 100]; //матрица весов 
        Stack<Point> path = new Stack<Point>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            statusBar1.Text = "Укажите размеры платы";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            width = Convert.ToInt32(textBox1.Text);
            height = Convert.ToInt32(textBox2.Text); //читаем размеры матрицы из texBox'ов 
            //инициализация матрицы
            matrixInit();
            // вычисление размеров кнопок
            panel2.Width = ((panel2.Width / width) + 1) * width;
            panel2.Height = ((panel2.Height / height) + 1) * height;
            distance_x = (panel2.Width/width);
            distance_y = (panel2.Height/height); 
            Console.Write(panel2.Width + " " + panel2.Height);
            Console.WriteLine(distance_x+" "+distance_y);
            //прогресс-бар
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = width;
            progressBar1.Value = 0;
            progressBar1.Step = 1;
            for (int x = 0;(x)<width*distance_x; x += distance_x)
            {
                for (int y = 0; (y ) < height * distance_y; y += distance_y)
                {
                    create_Cell(x, y);
                }
                progressBar1.PerformStep();
            }
            progressBar1.Visible=false;
            button1.Visible = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            button4.Visible = true;
            this.Width = panel2.Width + 40;
            this.Height =panel2.Height + 140;
            statusBar1.Text = "Выберите стартовую ячейку";
        }
        // функция генерации ячейки
        private void create_Cell(int x, int y)
        {
            int fontSize = (distance_y / 4)-1;
            Button button = new Button();
            button.Location = new Point(x, y); //позиция кнопки 
            button.Size = new Size(distance_x, distance_y); //размеры
            button.Font = new Font("Microsoft Sans Serif",fontSize);
            button.FlatAppearance.BorderSize = 1; 
            button.FlatAppearance.BorderColor = Color.Silver;  //рамка
            button.FlatStyle = FlatStyle.Flat; // плоская кнопка
            panel2.Controls.Add(button); // добавление на форму
            button.Click += new EventHandler(onCellClick); //добавляем обработчик клика
            button_array[(y / distance_y), (x / distance_x)] = button; // заполняем массив кнопками, 
                                                                   // чтобы к ним можно было получить доступ в любое время           
        }
        //функция-обработчик клика на ячейку
        void onCellClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            //Первый клик - стартовая ячейка
            //Второй клик - конечная ячейка
            //Последующие - препятствия
            switch (counter)
            {
                // чтобы перемещаться по OX, мы изменем j. Чтобы двигаться по OY мы изменяем i. 
                // Именно поэтому позиции i в матрице соответствует координата Y, а позиции j - X
                case 0:
                    Console.WriteLine((button.Location.Y / distance_y) + " " + (button.Location.X / distance_x) );
                    button_array[(button.Location.Y / distance_y), (button.Location.X / distance_x)].BackColor = Color.Orange; //красим ячейку
                    start.X = button.Location.X / distance_x; start.Y = button.Location.Y / distance_y; //запоминаем стартовую точку
                    matrix[start.Y, start.X] = 0; // помечаем стартовую ячейку
                    statusBar1.Text = "Выберите конечную ячейку";
                    log();
                    break;
                case 1:
                    if (pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), start.X, start.Y)) 
                        //стартовая точка не может быть конечной
                    {
                        counter--;
                    }
                    else
                    {
                        Console.WriteLine((button.Location.Y / distance_y) + " " + (button.Location.X / distance_x) );
                        button_array[ (button.Location.Y / distance_y), (button.Location.X / distance_x)].BackColor = Color.Orange;//красим ячейку
                        end.X = button.Location.X / distance_x; end.Y = button.Location.Y / distance_y;//запоминаем конечную точку
                        statusBar1.Text = "Укажите все препядствия и нажмите Расчитать";
                        log();
                    }
                    break;
                default:
                    if (!((pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), start.X, start.Y)) 
                        || (pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), end.X, end.Y))))
                        //стартовая и конечная точки не могут быть препядствиями
                    {
                        Console.WriteLine((button.Location.Y / distance_y) + " " + (button.Location.X / distance_x) );
                        button_array[(button.Location.Y / distance_y), (button.Location.X / distance_x)].BackColor = Color.Black;//красим ячейку
                        matrix[(button.Location.Y / distance_y), (button.Location.X / distance_x)] = int.MaxValue;//int.MaxValue; // помечаем ячейку
                        log();
                    }
                    break;
            }
            counter++;
            Console.WriteLine(start);
            //Console.WriteLine(end);
            //Console.WriteLine(counter);
            //Console.WriteLine(button.Location);
            Console.WriteLine(button.Size);
            //Console.WriteLine(this.Width);
            //Console.WriteLine(this.Height);
            //log();
            //Console.WriteLine((button.Location.X / distance_x) + " " + (button.Location.Y / distance_y));
        }
        //функция сравнения 2 точек
        bool pointCompare(int x1, int y1, int x2, int y2)
        {
            return ((x1 == x2) && (y1 == y2));
        }
        // процедура инициализации матрицы
        void matrixInit()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    matrix[i, j] = -1; //ячейка не помечена
                }
            }
        }
        void log()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.Write(matrix[i,j]+" ");
                }
                Console.WriteLine();
            }
        }
        void logStack(Stack<Point> stack)
        {
            int count = stack.Count;
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(stack.Pop());
            }
        }
        // расчет 
        private void button4_Click(object sender, EventArgs e)
        {
            // чтобы перемещаться по OX, мы изменем j. Чтобы двигаться по OY мы изменяем i 
            // имено поэтому такая запись matrix[end.Y, end.X]
            int d = 0; // фронт волны (в стартовой ячейке =0)
            bool emptyIteration = false; // флажок пустой итерации (отсутствие пути)
            //распространение волны
            while (matrix[end.Y, end.X] == -1 && !emptyIteration)
            // если конечная ячейка не помечена и последняя итерация не была пустой (волне некуда распространяться)
            {
                emptyIteration = true;
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (matrix[i, j] == d) //если метка ячейки совпадает с фронтом волны
                        {
                            wavePropagation(i, j);
                            emptyIteration= false;
                        }
                    }
                }
                d++;
            }
            if (matrix[end.Y, end.X] != -1) // если конечная ячейка не заполнена, то пути нет
            {
                // восстановление пути
                path.Push(end);
                while (matrix[path.Peek().Y, path.Peek().X] != 0)
                {
                    path.Push(scanNeighbors(path.Peek()));
                }
                statusBar1.Text = "Путь найден";
                // отображение пути
                path.Pop();
                int count = path.Count;
                Point tmp = new Point();
                for (int i = 0; i < count-1; i++)
                {
                    tmp = path.Pop();
                    button_array[tmp.Y, tmp.X].BackColor = Color.Yellow;
                }
            }
            else
            {
                statusBar1.Text = "Путь не существует";
            }
            Console.WriteLine("stack");
            logStack(path);
            //log();
            //вывод значений на кнопки
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (matrix[i, j] != int.MaxValue && matrix[i, j] != -1)
                    {
                        button_array[i, j].Text =matrix[i, j].ToString();
                    }
                }
            }
        }
        //процедура пометки соседних ячеек
        void wavePropagation(int i, int j)
        {
            if ((j - 1) >= 0 && matrix[i,j-1] == -1)
            //если такая ячейка существует и она не помечена
            {
                matrix[i, j - 1] = matrix[i, j] + 1;
            }
            if ((i - 1) >= 0 && matrix[i-1, j] == -1)
            {
                matrix[i-1, j] = matrix[i, j] + 1;
            }
            if ((j + 1) < width && matrix[i, j + 1] == -1)
            {
                matrix[i, j + 1] = matrix[i, j] + 1;
            }
            if ((i+1) < height  && matrix[i+1, j] == -1)
            {
                matrix[i+1, j] = matrix[i, j] + 1;
            }
        }
        Point scanNeighbors (Point currentPosition)
        {
            Point newPosition = new Point();
            newPosition.X = currentPosition.X;newPosition.Y = currentPosition.Y;
            if ((currentPosition.X-1 >= 0) && (matrix[currentPosition.Y, (currentPosition.X - 1)] != -1) &&
                (matrix[currentPosition.Y, currentPosition.X] - matrix[currentPosition.Y, (currentPosition.X - 1)]) == 1)
            {
                newPosition.X = currentPosition.X - 1;
                Console.WriteLine(newPosition);
                return newPosition;
            }
            if ((currentPosition.Y - 1 >= 0) && (matrix[(currentPosition.Y-1), currentPosition.X] != -1) &&
            (matrix[currentPosition.Y, currentPosition.X] - matrix[(currentPosition.Y - 1), currentPosition.X]) == 1)
            {
                newPosition.Y = currentPosition.Y - 1;
                Console.WriteLine(newPosition);
                return newPosition;
            }
            if ((currentPosition.X+1 >= height) && (matrix[currentPosition.Y, (currentPosition.X + 1)] != -1) &&
            (matrix[currentPosition.Y, currentPosition.X] - matrix[currentPosition.Y, (currentPosition.X + 1)]) == 1)
            {
                newPosition.X = currentPosition.X + 1;
                Console.WriteLine(newPosition);
                return newPosition;
            }
            if ((currentPosition.Y+1 >= width) && (matrix[(currentPosition.Y + 1), currentPosition.X] != -1) &&
            (matrix[currentPosition.Y, currentPosition.X] - matrix[(currentPosition.Y + 1), currentPosition.X]) == 1)
            {
                newPosition.Y = currentPosition.Y + 1;
                Console.WriteLine(newPosition);
                return newPosition;
            }
            return new Point();
            
        }
    }

}
