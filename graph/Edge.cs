using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphC_.graph
{
    internal class Edge
    {
        int m_weight;
        GraphNode m_direction;

        public Edge() { }
        public Edge(GraphNode root, GraphNode direction)
        {
            m_weight = (int)Math.Sqrt(Math.Pow(root.GetX() - direction.GetX(), 2) +
                                  Math.Pow(root.GetY() - direction.GetY(), 2));
            m_direction = direction;
        }

        public Edge(GraphNode direction, int weight)
        {
            m_weight = weight;
            m_direction = direction;
        }

        ~Edge() { }

        public GraphNode GetDirection() { return m_direction; }
        public int GetWeight() { return m_weight; }
    }
}
