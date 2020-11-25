using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Revenue
{
    public partial class Form1 : Form

    {
        int size = 16;
        string[] movie;
        double[] movieSales;
        string pathMovie = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"MovieRevenue.txt");

        public Form1()
        {
            InitializeComponent();
            ReadintoArray();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ReadintoArray()
        {



            var lines = File.ReadLines(pathMovie).ToArray();
            string[] fields;
            movie = new string[size];
            double output;
            movieSales = new double[size];

            for (int i = 0; i < lines.Length; i++)
            {
                fields = lines[i].Split('@', '}', '@', '=', '~');
                movie[i] = fields[0].Trim();
                double.TryParse(fields[1].Trim(), out output);
                movieSales[i] = output;

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.ForeColor = Color.Yellow;
            listBox1.BackColor = Color.Blue;
            SelectionSortByName(movie, movieSales);

            for (int i = 0; i < Math.Max(movieSales.Length, movie.Length); i++)
            {
                listBox1.Items.Add(movie[i] + ">>>>>>>>" + movieSales[i].ToString("c"));
            }

        }

        static void SelectionSortByName(string[] movie, double[] sale)
        {
            int pos = 1;
            while (pos < movie.Length)
            {
                if (String.Compare(movie[pos], movie[pos - 1], StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    pos++;

                }
                else
                {
                    string temp = movie[pos];
                    double Tempnumber = sale[pos];
                    movie[pos] = movie[pos - 1];
                    movie[pos - 1] = temp;
                    sale[pos] = sale[pos - 1];
                    sale[pos - 1] = Tempnumber;
                    if (pos > 1) pos--;

                }
            }

        }


        private void SelectionSortByRevenue()
        {
            double temp;
            string tempMovie;
            for (int j = 0; j <= movieSales.Length - 2; j++)
            {
                for (int i = 0; i <= movieSales.Length - 2; i++)
                {
                    if (movieSales[i] < movieSales[i + 1])
                    {
                        temp = movieSales[i + 1];
                        movieSales[i + 1] = movieSales[i];
                        movieSales[i] = temp;
                        tempMovie = movie[i + 1];
                        movie[i + 1] = movie[i];
                        movie[i] = tempMovie;
                    }
                }
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox1.BackColor = Color.Red;
            listBox1.ForeColor = Color.Black;
            SelectionSortByRevenue();
            for (int i = 0; i < Math.Max(movieSales.Length, movie.Length); i++)
            {
                listBox1.Items.Add(movie[i] + ">>>>>>>>" + movieSales[i].ToString("c"));
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if(radioButton1.Checked == true)
            {
                string[] searchmovie = movie.Where(x => x.ToLower().Contains(textBox1.Text.Trim())).ToArray();

                foreach (string movie in searchmovie)
                {
                    listBox1.Items.Add(movie + "<<<<<<<<<" );
                }
            }

            if(radioButton2.Checked == true)
            {
                listBox1.Items.Clear();
                listBox1.BackColor = Color.Red;
                listBox1.ForeColor = Color.Black;
                SelectionSortByRevenue();
                if (movieSales[BinSrch(movieSales, double.Parse(textBox1.Text.Trim()))] != double.Parse(textBox1.Text.Trim()))
                {
                    listBox1.Items.Add("no movie has this exact renenue");
                    listBox1.Items.Add("=====================================");
                }
                DisplayRevenue();
            }

            if(radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("please select within name or Revenue");
            }

        }


        public int BinSrch(double[] array, double key)
        {

            int min = 0;
            int max = array.Length - 1;
          


            while (min <= max)
            {
                int mid = (min + max) / 2;

                if (array[mid] == key)
                {

                    return mid;

                }
                else if (array[mid] < key)
                    max = mid - 1;
                else if (array[mid] > key)
                    min = mid + 1;
                    
            }

                return min;

        }

        private void DisplayRevenue()
        {
            //MessageBox.Show(BinSrch(movieSales, double.Parse(textBox1.Text)).ToString());

            if (textBox1 == null || string.IsNullOrWhiteSpace(textBox1.Text) || !Regex.IsMatch(textBox1.Text, "[ ^ 0-9]"))
            {
                MessageBox.Show("Not numeric - please re-enter");
               
            } else
            {
                 for (int i = BinSrch(movieSales, double.Parse(textBox1.Text.Trim())); i < Math.Max(movieSales.Length, movie.Length); i++)
                {
                    if (movieSales[BinSrch(movieSales, double.Parse(textBox1.Text.Trim())) ]== double.Parse(textBox1.Text.Trim())) {
                        listBox1.Items.Add(movie[i] + "<<<<<<<<<" + movieSales[i].ToString("c"));

                    } else

                        listBox1.Items.Add(movie[i] + "<<<<<<<<<" + movieSales[i].ToString("c"));
                    }

                }
            }



        }

  }




