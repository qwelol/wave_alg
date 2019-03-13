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
        Button[,] button_array = new Button[100,100];//массив кнопок
        int counter=0; //счетчик кликов
        int[,] matrix = new int[100, 100]; //матрица весов 
        Stack<Point> path = new Stack<Point>(); //путь
        bool counted = false; //флаг окончания расчета
        string outputMatrix = ""; //матрица для вывода в файл
        string outputPath; //путь для вывода в файл
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            statusBar.Text = "Укажите размеры платы";
        }
        //клик на кнопку Генерировать
        private void generateBtn_Click(object sender, EventArgs e)
        {
            if (widthTB.Text != "" && heightTB.Text != "" 
                && Convert.ToInt32(widthTB.Text)!=0 && Convert.ToInt32(heightTB.Text) != 0)
            {
                width = Convert.ToInt32(widthTB.Text);
                height = Convert.ToInt32(heightTB.Text); //читаем размеры матрицы из texBox'ов 
                //инициализация матрицы
                matrixInit();
                start.X = -1; start.Y = -1;
                end.X = -1; end.Y = -1;
                // вычисление размеров кнопок
                workplacePanel.Width = ((workplacePanel.Width / width) + 1) * width;
                workplacePanel.Height = ((workplacePanel.Height / height) + 1) * height;
                distance_x = (workplacePanel.Width / width);
                distance_y = (workplacePanel.Height / height);
                //Console.Write(panel2.Width + " " + panel2.Height);
                //Console.WriteLine(distance_x+" "+distance_y);
                //прогресс-бар
                progressBar.Visible = true;
                progressBar.Minimum = 0;
                progressBar.Maximum = width;
                progressBar.Value = 0;
                progressBar.Step = 1;
                for (int x = 0; (x) < width * distance_x; x += distance_x)
                {
                    for (int y = 0; (y) < height * distance_y; y += distance_y)
                    {
                        create_Cell(x, y);
                    }
                    progressBar.PerformStep();
                }
                progressBar.Visible = false;
                generateBtn.Visible = false;
                widthTB.Enabled = false;
                heightTB.Enabled = false;
                calculateBtn.Visible = true;
                this.Width = workplacePanel.Width + 40;
                this.Height = workplacePanel.Height + 160;
                statusBar.Text = "Выберите стартовую ячейку";
            }
        }
        // функция генерации ячейки
        private void create_Cell(int x, int y)
        {
            int fontSize = (distance_y / 4)-1;
            Button button = new Button();
            button.Location = new Point(x, y); //позиция кнопки 
            button.Size = new Size(distance_x, distance_y); //размеры
            button.Font = new Font("Microsoft Sans Serif",fontSize);//шрифт
            button.FlatAppearance.BorderSize = 1; 
            button.FlatAppearance.BorderColor = Color.Silver;  //рамка
            button.FlatStyle = FlatStyle.Flat; // плоская кнопка
            workplacePanel.Controls.Add(button); // добавление на форму
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
            if (!counted) //если мы уже все посчитали, то клики обрабатывать не нужно
            {
                switch (counter)
                {
                    // чтобы перемещаться по OX, мы изменем j. Чтобы двигаться по OY мы изменяем i. 
                    // Именно поэтому позиции i в матрице соответствует координата Y, а позиции j - X
                    case 0:
                        Console.WriteLine((button.Location.Y / distance_y) + " " + (button.Location.X / distance_x));
                        button_array[(button.Location.Y / distance_y), (button.Location.X / distance_x)].BackColor = Color.Orange; //красим ячейку
                        start.X = button.Location.X / distance_x; start.Y = button.Location.Y / distance_y; //запоминаем стартовую точку
                        matrix[start.Y, start.X] = 0; // помечаем стартовую ячейку
                        statusBar.Text = "Выберите конечную ячейку";
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
                            Console.WriteLine((button.Location.Y / distance_y) + " " + (button.Location.X / distance_x));
                            button_array[(button.Location.Y / distance_y), (button.Location.X / distance_x)].BackColor = Color.Orange;//красим ячейку
                            end.X = button.Location.X / distance_x; end.Y = button.Location.Y / distance_y;//запоминаем конечную точку
                            statusBar.Text = "Укажите все препядствия и нажмите Расчитать";
                            //log();
                        }
                        break;
                    default:
                        if (!((pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), start.X, start.Y))
                            || (pointCompare((button.Location.X / distance_x), (button.Location.Y / distance_y), end.X, end.Y))))
                        //стартовая и конечная точки не могут быть препядствиями
                        {
                            if (matrix[(button.Location.Y / distance_y), (button.Location.X / distance_x)] == int.MaxValue)
                                //по повторному щелчку на препядствие оно удаляется
                            {
                                Console.WriteLine((button.Location.Y / distance_y) + " " + (button.Location.X / distance_x));
                                button_array[(button.Location.Y / distance_y), (button.Location.X / distance_x)].BackColor = SystemColors.Control;//красим ячейку
                                matrix[(button.Location.Y / distance_y), (button.Location.X / distance_x)] = -1; // снимаем пометку
                                //log();
                            }
                            else
                            {
                                Console.WriteLine((button.Location.Y / distance_y) + " " + (button.Location.X / distance_x));
                                button_array[(button.Location.Y / distance_y), (button.Location.X / distance_x)].BackColor = Color.Black;//красим ячейку
                                matrix[(button.Location.Y / distance_y), (button.Location.X / distance_x)] = int.MaxValue; // помечаем ячейку
                                //log();
                            }
                        }
                        break;
                }
            }
            counter++;
            //Console.WriteLine(start);
            //Console.WriteLine(end);
            //Console.WriteLine(counter);
            //Console.WriteLine(button.Location);
            //Console.WriteLine(button.Size);
            //Console.WriteLine(this.Width);
            //Console.WriteLine(this.Height);
            //log();
            //Console.WriteLine((button.Location.X / distance_x) + " " + (button.Location.Y / distance_y));
        }
        // расчет 
        private void calculateBtn_Click(object sender, EventArgs e)
        {
            // чтобы перемещаться по OX, мы изменем j. Чтобы двигаться по OY мы изменяем i 
            // имено поэтому такая запись matrix[end.Y, end.X]
            if (!(end.X==-1 || start.X==-1)) 
            //не нужно делать расчет, если конечная или начальная точки не заданы
            {
                int d = 0; // фронт волны (в стартовой ячейке =0)
                bool emptyIteration = false; // флажок пустой итерации (отсутствие пути)
                counted = true; //мы начали считать, ничего менять уже нельзя
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
                                emptyIteration = false;
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
                    statusBar.Text = "Путь найден";
                    // отображение пути
                    Point tmp = new Point();
                    tmp = path.Pop();
                    int count = path.Count; // значение path.Count нельзя использовать в цикле т.к. оно уменьшается пры взятии из стека
                    outputPath = outputPath + "[" + (tmp.Y + 1).ToString() + "," + (tmp.X + 1).ToString() + "] ";
                    for (int i = 0; i < count - 1; i++)
                    {
                        //здесь же записываем путь для вывода в файл
                        tmp = path.Pop();
                        outputPath = outputPath + "[" + (tmp.Y + 1).ToString() + "," + (tmp.X + 1).ToString() + "] ";
                        button_array[tmp.Y, tmp.X].BackColor = Color.Yellow;
                    }
                    tmp = path.Pop();
                    outputPath = outputPath + "[" + (tmp.Y + 1).ToString() + "," + (tmp.X + 1).ToString() + "] ";
                    Console.WriteLine(outputPath);
                }
                else
                {
                    statusBar.Text = "Путь не существует";
                }
                //вывод значений на кнопки
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (matrix[i, j] != int.MaxValue && matrix[i, j] != -1) //значения препядствий и не помеченныйх ячеек не отображаем
                        {
                            button_array[i, j].Text = matrix[i, j].ToString();
                        }
                    }
                }
                calculateBtn.Visible = false;
                againBtn.Visible = true;
                saveToolStripMenuItem.Enabled = true;
            }
        }
        //процедура пометки соседних ячеек
        void wavePropagation(int i, int j)
        {
            //последовательность значения не имеет, т.к. мы помечаем все соседние ячейки
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
        //функция просмотра соседних ячеек
        Point scanNeighbors (Point currentPosition)
        {
            //тут последовательность важна
            Point newPosition = new Point();
            Console.WriteLine("Current" + currentPosition);
            newPosition.X = currentPosition.X;newPosition.Y = currentPosition.Y;
            //если такая ячейка существует, она заполнена и ее значение меньше текущего на 1
            if ((currentPosition.X-1 >= 0) && 
                (matrix[currentPosition.Y, currentPosition.X] - matrix[currentPosition.Y, (currentPosition.X - 1)]) == 1)//влево
                
            {
                newPosition.X = currentPosition.X - 1;
                Console.WriteLine("Current" + currentPosition);
                Console.WriteLine("Left"+newPosition);
                return newPosition;
            }
            if ((currentPosition.Y - 1 >= 0) && 
            (matrix[currentPosition.Y, currentPosition.X] - matrix[(currentPosition.Y - 1), currentPosition.X]) == 1)//вверх
            {
                newPosition.Y = currentPosition.Y - 1;
                Console.WriteLine("Current" + currentPosition);
                Console.WriteLine("Up" + newPosition);
                return newPosition;
            }
            if ((currentPosition.X+1 <= width) && 
            (matrix[currentPosition.Y, currentPosition.X] - matrix[currentPosition.Y, (currentPosition.X + 1)]) == 1)//вправо
            {
                newPosition.X = currentPosition.X + 1;
                Console.WriteLine("Current" + currentPosition);
                Console.WriteLine("Right" + newPosition);
                return newPosition;
            }
            if ((currentPosition.Y+1 <= height) && 
            (matrix[currentPosition.Y, currentPosition.X] - matrix[(currentPosition.Y + 1), currentPosition.X]) == 1)//вниз
            {
                newPosition.Y = currentPosition.Y + 1;
                Console.WriteLine("Current" + currentPosition);
                Console.WriteLine("Down" + newPosition);
                return newPosition;
            }
            Console.WriteLine("alarm");
            return newPosition;
        }
        //сохранение в файл
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //нужно записать матрицу в строку, потом вывести ее и outputPath в текстовый файл
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (matrix[i, j] != int.MaxValue)
                    {
                        outputMatrix = outputMatrix + matrix[i, j].ToString() + "\t";
                    }
                    else
                    {
                        outputMatrix = outputMatrix+ "inf" + "\t";
                    }
                }
                outputMatrix = outputMatrix + "\n";
            }
            using (FileStream fstream = new FileStream(@"../../output.txt", FileMode.Create))
            {
                // преобразуем строку в байты
                Console.WriteLine("Матрица: \n" + outputMatrix + "Путь: \n" +  outputPath);
                byte[] array = System.Text.Encoding.Default.GetBytes("Матрица: \n" + outputMatrix + "Путь: \n" + outputPath);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }
        //возврат к исходному состоянию
        private void againBtn_Click(object sender, EventArgs e)
        {
            //возвращаем переменные в исходное состояние
            matrixInit();
            counted = false;
            counter = 0;
            outputPath = "";
            outputMatrix = "";
            saveToolStripMenuItem.Enabled = false;
            workplacePanel.Visible = false; //скроем панель, чтобы не было видно удаления кнопок
            //for (int x = 0; x < height; x++)
            //{
            //    for (int y = 0; y < width; y++)
            //    {
            //        workplacePanel.Controls.Remove(button_array[x, y]); //удаляем кнопки
            //    }
            //}
            workplacePanel.Controls.Clear();
            Array.Clear(button_array, 0, button_array.Length);
            //ставим исходные размеры
            workplacePanel.Width = 700;
            workplacePanel.Height = 520;
            workplacePanel.Visible = true;
            this.Width = 740;
            this.Height = 680;
            againBtn.Visible = false;
            //удаляем предыдущие значения размеров, делаем доступным ввод новых
            widthTB.Text = "";
            heightTB.Text = "";
            widthTB.Enabled = true;
            heightTB.Enabled = true;
            //делаем снова доступной кнопку генерации 
            generateBtn.Visible = true;
            statusBar.Text = "Выберите стартовую ячейку";
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
                    Console.Write(matrix[i, j] + " ");
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

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void widthTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8) //Если символ, введенный с клавы - не цифра (IsDigit),
            {
                e.Handled = true;// то событие не обрабатывается. ch!=8 (8 - это Backspace)
            }
        }

        private void heightTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8) //Если символ, введенный с клавы - не цифра (IsDigit),
            {
                e.Handled = true;// то событие не обрабатывается. ch!=8 (8 - это Backspace)
            }
        }
    }
}
