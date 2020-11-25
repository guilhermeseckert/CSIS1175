﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string []fields;
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

        static void SelectionSortByName(string[] movie, double[] sale )
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

          


            listBox1.Items.Clear();

            // if (radioButton1.Checked == true)
            // {
            //  string[] result = movie.Where(x => x.ToLower().Contains(textBox1.Text.Trim().ToLower())).ToArray();

            //  foreach (string i in result)
            //  {
            //int index = Array.IndexOf(movie, i);
            //      listBox1.Items.Add(i + ">>>>>" + movieSales[index]);
            //      }



            //here numbers
            SelectionSortByRevenue();
            foreach (double i in movieSales)
            {
                listBox1.Items.Add(i);
            }

           // BinSrch(movieSales, double.Parse(textBox1.Text));

            MessageBox.Show(BinSrch(movieSales, 7452893.21).ToString());

    
            
         
        }


        public  double BinSrch(double[] arr, double key)
        {
            int min = 0;
            int N = arr.Length;
            int max = N - 1;
            do
            {
                int mid = (min + max) / 2;
                if (key > arr[mid])
                    min = mid + 1;
                else
                    max = mid - 1;
                if (arr[mid] == key)
                    return mid;
            } while (min <= max);
            return -1;
        }

        }


    }

    
