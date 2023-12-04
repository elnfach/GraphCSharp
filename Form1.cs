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
        Graph graph = new Graph();
        Random random = new Random();
        public Form1()
        {
            int x, y;
            int count = 5;
            for (int i = 0; i < count; i++)
            {
                x = random.Next(20, 700);
                y = random.Next(20, 700);
                graph.AddNode( new GraphNode( x, y, 12, "Город " + i ) );
            }
            graph.CreateEdges();
            graph.ViewTable();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            graph.Draw(this, e);
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            graph.ShowNames( checkBox2.Checked );
            Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            graph.ShowEdges( checkBox1.Checked );
            Refresh();
        }
    }
}
