using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GraphC_.unitlity;

namespace GraphC_.graph
{
    internal class GraphNode
    {
        static int id;
        int m_id;
        int m_x;
        int m_y;
        int m_radius;
        int m_diameter;

        Vector2f[,] m_bound;
        int m_factor;

        string m_name;

        List<Edge> m_edges;

        public GraphNode()
        {
            m_id = id++;
            m_x = 0;
            m_y = 0;
            m_radius = 5;
            m_diameter = m_radius * 2;
            m_factor = m_radius + 5;
            m_name = "Default name";
            m_edges = new List<Edge>();
        }
        public GraphNode(int x, int y, string name)
        {
            m_id = id++;
            m_x = x;
            m_y = y;
            m_radius = 5;
            m_diameter = m_radius * 2;
            m_factor = m_radius + 5;
            m_name = name;
            m_edges = new List<Edge>();
        }

        public GraphNode(int x, int y, int radius, string name) : this(x, y, name)
        {
            m_id = id++;
            m_radius = radius;
            m_diameter = radius * 2;
            m_edges = new List<Edge>();
            m_name = name;
        }

        ~GraphNode() { }

        public int GetX() { return m_x; }
        public int GetY() { return m_y; }
        public int GetID() { return m_id; }
        public string GetName() { return m_name;}

        private void CalcaluateBound()
        {
             
        }

        public void Draw(Form1 form, PaintEventArgs e, bool enabled)
        {
            Graphics gr = form.CreateGraphics();
            Brush brush = new SolidBrush(Color.Black);
            Pen pen = new Pen(brush, 1);
            gr.DrawEllipse(pen, m_x - m_radius, m_y - m_radius, m_diameter, m_diameter);
            if(enabled)
            {
                using (Font font = new Font("Times New Roman", 16, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    Point point1 = new Point(m_x, m_y - m_radius * 2);
                    TextRenderer.DrawText(e.Graphics, m_name, font, point1, Color.Black);
                }
            }
        }

        public void DrawEdges(Form1 form, PaintEventArgs e, bool enabled)
        {
            foreach (var i in m_edges)
            {
                if (i.GetWeight() != int.MaxValue)
                {
                    Pen pen = new Pen( Color.Black );
                    e.Graphics.DrawLine( pen, m_x, m_y, i.GetDirection().m_x, i.GetDirection().m_y );

                    if (i.GetWeight() > 0 && enabled)
                    {
                        using (Font font = new Font( "Times New Roman", 16, FontStyle.Bold, GraphicsUnit.Pixel ))
                        {
                            Point point1 = new Point( (m_x + i.GetDirection().m_x) / 2, (m_y + i.GetDirection().m_y) / 2 );
                            TextRenderer.DrawText( e.Graphics, i.GetWeight().ToString(), font, point1, Color.Black );
                        }
                    }
                }
            }
        }

        public void DrawRedEdge(Form1 form, PaintEventArgs e)
        {
            foreach (var i in m_edges)
            {
                Pen pen = new Pen(Color.Red);
                e.Graphics.DrawLine(pen, m_x, m_y, i.GetDirection().m_x, i.GetDirection().m_y);
            }
        }

        public void CreateEdge(GraphNode root, GraphNode destination)
        {
            Edge edge = new Edge( root, destination);
            m_edges.Add(edge);
        }

        public bool HasEdge(GraphNode direction)
        {
            foreach (var i in m_edges)
            {
                if (i.GetDirection() == direction)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
