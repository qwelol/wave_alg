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
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    matrix[i, j] = -1; //ячейка не помечена
                }
            }
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
            this.Width = panel2.Width + 40;
            this.Height =panel2.Height + 140;
            statusBar1.Text = "Выберите стартовую ячейку";
        }
        // функция генерации ячейки
        private void create_Cell(int x, int y)
        {
            Button button = new Button();
            button.Location = new Point(x, y); //позиция кнопки 
            button.Size = new Size(distance_x, distance_y); //размеры
            button.FlatAppearance.BorderSize = 1; 
            button.FlatAppearance.BorderColor = Color.Silver;  //рамка
            button.FlatStyle = FlatStyle.Flat; // плоская кнопка
            panel2.Controls.Add(button); // добавление на форму
            button.Click += new EventHandler(onCellClick); //добавляем обработчик клика
            button_array[(x / distance_x), (y / distance_y)] = button; // заполняем массив кнопками, 
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
                case 0:
                    Console.WriteLine((button.Location.X / distance_x) + " " + (button.Location.Y / distance_y));
                    button_array[(button.Location.X / distance_x), (button.Location.Y / distance_y)].BackColor = Color.Orange; //красим ячейку
                    start.X = button.Location.X / distance_x; start.Y = button.Location.Y / distance_y; //запоминаем стартовую точку
                    matrix[start.X, start.Y] = 0; // помечаем стартовую ячейку
                    statusBar1.Text = "Выберите конечную ячейку";
                    break;
                case 1:
                    if (pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), start.X, start.Y)) 
                        //стартовая точка не может быть конечной
                    {
                        counter--;
                    }
                    else
                    {
                        Console.WriteLine((button.Location.X / distance_x) + " " + (button.Location.Y / distance_y));
                        button_array[(button.Location.X / distance_x), (button.Location.Y / distance_y)].BackColor = Color.Orange;//красим ячейку
                        end.X = button.Location.X / distance_x; end.Y = button.Location.Y / distance_y;//запоминаем конечную точку
                        statusBar1.Text = "Укажите все препядствия и нажмите Расчитать";
                    }
                    break;
                default:
                    if (!((pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), start.X, start.Y)) 
                        || (pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), end.X, end.Y))))
                        //стартовая и конечная точки не могут быть препядствиями
                    {
                        Console.WriteLine((button.Location.X / distance_x) + " " + (button.Location.Y / distance_y));
                        button_array[(button.Location.X / distance_x), (button.Location.Y / distance_y)].BackColor = Color.Black;//красим ячейку
                        matrix[(button.Location.X / distance_x), (button.Location.Y / distance_y)] = int.MaxValue; // помечаем ячейку
                    }
                    break;
            }
            counter++;
            //тут будет много кода 
            Console.WriteLine(counter);
            Console.WriteLine(button.Location);
            Console.WriteLine(button.Size);
            Console.WriteLine(this.Width);
            Console.WriteLine(this.Height);
        }
        //функция сравнения 2 точек
        bool pointCompare(int x1, int y1, int x2, int y2)
        {
            return ((x1 == x2) && (y1 == y2));
        }
        void log()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.Write(matrix[i,j]);
                }
                Console.WriteLine();
            }
        }
    }

}
