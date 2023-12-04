using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Linq;

namespace GraphC_.graph
{
    internal class Graph
    {
        Random random = new Random();
        bool m_show_names;
        bool m_show_weight;
        bool m_complete_graph;

        int m_range;
        int m_chance;

        float[,] array;

        List<GraphNode> m_nodes;
        Edge[,] m_edges;

        public Graph() 
        {
            m_range = 10;
            m_chance = 5;
            m_show_names = true;
            m_show_weight = true;
            m_complete_graph = true;
            m_nodes = new List<GraphNode>();
        }

        public Graph(int range, int chance)
        {
            m_range = range;
            m_chance = chance;
            m_show_names = true;
            m_show_weight = true;
            m_complete_graph = true;
            m_nodes = new List<GraphNode>();
        }

        public Graph(int range, int chance, bool complete_graph = true)
        {
            m_range = range;
            m_chance = chance;
            m_show_names = true;
            m_show_weight = true;
            m_nodes = new List<GraphNode>();
            m_complete_graph = complete_graph;
        }
        ~Graph() { }

        public void Draw(Form1 form, PaintEventArgs e)
        {
            foreach (var i in m_nodes)
            {
                i.Draw(form, e, m_show_names);
                i.DrawEdges(form, e, m_show_weight);
            }
        }

        public void DrawRed(Form1 form, PaintEventArgs e)
        {
            foreach (var i in m_nodes)
            {
                i.DrawRedEdge(form, e);
            }
        }

        public void ViewTable()
        {
            foreach (GraphNode item in m_nodes)
            {
                Console.WriteLine("{0}\t{1}", item.GetX(), item.GetY());
            }
            Console.WriteLine();

            int rows = m_edges.GetUpperBound( 0 ) + 1; 
            int columns = m_edges.Length / rows;

            for (int i = 0; i < rows; i++)
            {
                Console.Write("Город {0}: ", i );
                for (int j = 0; j < columns; j++)
                {
                    if (m_edges[i, j] != null) 
                    {
                        Console.Write( "{0}\t", m_edges[i, j].GetWeight() );
                    }
                    else
                    {
                        Console.Write( "{0}\t", "MAX" );
                    }
                }
                Console.WriteLine();
            }
        }
        public void AddNode(GraphNode node)
        {
            m_nodes.Add(node);
        }

        public void AddNode(int x, int y, string name)
        {
            m_nodes.Add(new GraphNode(x, y, name));
        }
        public void AddNode(int x, int y, int radius, string name)
        {
            m_nodes.Add(new GraphNode(x, y, radius, name));
        }

        public void CreateEdges()
        {
            int i_edge = 0;
            int j_edge = 0;
            m_edges = new Edge[m_nodes.Count, m_nodes.Count];
            foreach (var i in m_nodes)
            {
                j_edge = 0;
                foreach (var j in m_nodes)
                {
                    if (!i.HasEdge( j ) && 
                        i.GetID() != j.GetID() &&
                        random.Next( 0, m_range ) > m_chance)
                    {
                        i.CreateEdge( i, j );
                        j.CreateEdge( j, i );
                        m_edges[i_edge, j_edge] = new Edge( i, j );
                    }
                    else
                    {
                        m_edges[i_edge, j_edge] = null;
                    }
                    j_edge++;
                }
                i_edge++;
            }
        }

        public void HideEdges()
        {
            m_show_weight = false;
        }

        public void ShowEdges()
        {
            m_show_weight = true;
        }

        public void HideNames()
        {
            m_show_names = false;
        }

        public void ShowNames()
        {
            m_show_names = true;
        }
    }
}
