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
        Button[,] button_array = new Button[1000,1000];
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
            //this.Width = (distance_x * width) + 40;
            //this.Height = (distance_y * height) + 140;
            this.Width = panel2.Width +40;
            this.Height =panel2.Height + 140;
            statusBar1.Text = "Выберите стартовую позицию";
            button_array[1, 1].BackColor = Color.Orange;

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
            //тут будет много кода 
            Console.WriteLine("good");
            Console.WriteLine(button.Location);
            Console.WriteLine(button.Size);
            Console.WriteLine(this.Width);
            Console.WriteLine(this.Height);
        }
    }

}
