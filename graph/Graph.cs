using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

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

        List<GraphNode> m_nodes;
        Edge[,] m_edges;

        int[] m_path;
        int[] m_best_path;
        int m_best_cost;

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

        public Graph(int range, int chance, bool complete_graph)
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
                Console.WriteLine( "{0}\t{1}", item.GetX(), item.GetY() );
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

        public void Update()
        {
            m_path = new int[m_nodes.Count + 1];
            m_best_path = new int[m_nodes.Count + 1];
            m_best_cost = int.MaxValue;
        }

        public void CreateEdges()
        {
            int i_edge = 0;
            int j_edge;
            m_edges = new Edge[m_nodes.Count, m_nodes.Count];
            
            foreach (var i in m_nodes)
            {
                j_edge = 0;
                foreach (var j in m_nodes)
                {
                    if (i.GetID() != j.GetID())
                    {
                        if (m_complete_graph)
                        {
                            i.CreateEdge( i, j );
                            j.CreateEdge( j, i );
                            m_edges[i_edge, j_edge] = new Edge( i, j );
                            m_edges[j_edge, i_edge] = new Edge( j, i );
                        }
                        else
                        {
                            if (random.Next( 0, m_range ) > m_chance)
                            {
                                i.CreateEdge( i, j );
                                j.CreateEdge( j, i );
                                m_edges[i_edge, j_edge] = new Edge( i, j );
                                m_edges[j_edge, i_edge] = new Edge( j, i );
                            }
                        }
                    }
                    j_edge++;
                }
                i_edge++;
            }
        }

        private void GreedyStart()
        {
            int[] visited = new int[m_nodes.Count];
            visited[0] = 1;
            int current_vertex = 0;
            int path_length = 0;

            for (int i = 0; i < m_nodes.Count - 1; i++)
            {
                int minEdge = int.MaxValue;
                int nextVertex = -1;

                for (int j = 0; j < m_nodes.Count; j++)
                {
                    if (visited[j] == 0 && m_edges[current_vertex, j].GetWeight() < minEdge)
                    {
                        minEdge = m_edges[current_vertex, j].GetWeight();
                        nextVertex = j;
                    }
                }

                visited[nextVertex] = 1;
                current_vertex = nextVertex;
                path_length += minEdge;
            }
            path_length += m_edges[current_vertex, 0].GetWeight();
            Console.WriteLine( "Длина пути: {0}", path_length );
        }


        public void Greedy(int start_node)
        {
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine( "Жадный перебор графа:\n" );
            Console.WriteLine( "Матрица смежности:" );
            ViewTable();
            stopwatch.Start();
            GreedyStart();
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;
            Console.WriteLine( $"Время выполнения алгоритма: {elapsedTime}" );
        }

        private void HeuristicAlgorithmStart()
        {

        }
        public void HeuristicAlgorithm()
        {
            Console.WriteLine( "Приближенный перебор графа (Деревянный алгоритм):\n" );
            HeuristicAlgorithmStart();
            Console.WriteLine();
        }

        private void BruteForceStart(int start_node, int level, int cost)
        {
            m_path[level] = start_node;

            if (level == m_nodes.Count)
            {
                cost += m_edges[start_node, 0].GetWeight();

                if (cost < m_best_cost)
                {
                    Array.Copy( m_path, m_best_path, m_nodes.Count + 1 );
                    m_best_cost = cost;
                }
            }
            else
            {
                for (int i = 1; i < m_nodes.Count; i++)
                {
                    if (IsValidNextNode( i ))
                    {
                        BruteForceStart( i, level + 1, cost + m_edges[start_node, i].GetWeight() );
                    }
                }
            }
        }

        public void BruteForce(int start_node, int level, int cost)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            BruteForceStart(start_node, level, cost);
            stopwatch.Stop();
            TimeSpan elapsedTime = stopwatch.Elapsed;

            Console.WriteLine( "Полный перебор графа:\n" );
            Console.WriteLine( "Матрица смежности:" );
            ViewTable();
            Console.WriteLine( "Лучший путь:" );
            for (int i = 0; i < m_nodes.Count; i++)
            {
                Console.Write( m_best_path[i + 1] + " -> " );
            }
            Console.Write( m_best_path[0] );
            Console.WriteLine();
            Console.WriteLine( "Стоимость пути: " + m_best_cost );
            Console.WriteLine( $"Время выполнения алгоритма: {elapsedTime}" );
        }

        private bool IsValidNextNode(int next_node)
        {
            for (int i = 1; i < m_nodes.Count; i++)
            {
                if (m_path[i] == next_node)
                {
                    return false;
                }
            }
            return true;
        }

        public string[] GetNodes() {
            string[] temp = new string[m_nodes.Count];
            int i = 0;
            foreach (var item in m_nodes)
            {
                temp[i] = item.GetName();
                i++;
            }
            return temp;
        }

        public void ShowEdges(bool enabled)
        {
            m_show_weight = enabled;
        }

        public void ShowNames(bool enabled)
        {
            m_show_names = enabled;
        }
    }
}
