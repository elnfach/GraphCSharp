using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphC_.graph;

namespace GraphC_
{
    public partial class Form1 : Form
    {
        Graph graph;
        Random random = new Random();
        string[] algorithms = { "Полный перебор", "Деревянный", "Жадный" };

        public Form1()
        {
            InitializeComponent();
            comboBox3.Items.AddRange( algorithms );
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (graph != null) {
                e.Graphics.Clear( Color.White );
                graph.Draw( this, e );
            }
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            if (graph != null)
            {
                graph.ShowNames( checkBox2.Checked );
                Refresh();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (graph != null)
            {
                graph.ShowEdges( checkBox1.Checked );
                Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graph.Update();
            if (comboBox3.SelectedIndex == 0)
                graph.BruteForce( 0, 1, 0 );
            if (comboBox3.SelectedIndex == 1)
                graph.HeuristicAlgorithm();
            if (comboBox3.SelectedIndex == 2)
                graph.Greedy(0);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = !checkBox3.Checked;
            textBox2.Enabled = !checkBox3.Checked;
        }

        // Initialization graph
        private void button3_Click(object sender, EventArgs e) 
        {
            try
            {
                if (graph == null)
                    graph = null;
                if (!checkBox3.Checked)
                    graph = new Graph( Convert.ToInt32( textBox1.Text ), Convert.ToInt32( textBox2.Text ), checkBox3.Checked );
                else
                    graph = new Graph();
                int x, y;
                int count = Convert.ToInt32( textBox3.Text );
                for (int i = 0; i < count; i++)
                {
                    x = random.Next( 20, 700 );
                    y = random.Next( 20, 700 );
                    graph.AddNode( new GraphNode( x, y, 12, "Город " + i ) );
                }
                graph.CreateEdges();
                Refresh();
            } catch
            {
                MessageBox.Show( "Введите верные данные!" );
            }
        }
    }
}
