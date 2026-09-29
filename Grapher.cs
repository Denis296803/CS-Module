using System;
using System.Collections;
public class Grapher
{
    class Node
    {
        public int parent;
        public int id;
        public double cost;
        public Node(int par, double co, int i) { parent = par; id = i; cost = co; }
    }
    public class Edge
    {
        public int id, direction;
        public int[] connections = new int[2];
        public double weight;
        public Edge(int Id, int[] Connections, float Weight = 1, int Direction = 0)
        {
            id = Id; connections = Connections; weight = Weight; direction = Direction;
        }
    }
    public class Vertex
    {
        public int id;
        public Vertex(int Id)
        {
            id = Id;
        }
    }

    public class Graph
    {
        public List<Vertex> vertices = new List<Vertex>(); public List<Edge> edges = new List<Edge>(); public HashSet<int> edge_ids = new HashSet<int>(); public HashSet<int> vertex_ids = new HashSet<int>();
        public void AddVertex(Vertex vert, out string error)
        {
            if (vert.id < 0) { error = "Id не может быть отрицательным."; return; }
            if (vertex_ids.Contains(vert.id))
            {
                error = "Вершина с этим id уже существует. Для получения списка доступных id воспользуйтесь GetAvailableVertexIds.";
                return;
            }
            error = "OK.";
            Logic_AddVertex(vert);
        }
        public void AddVertex(Vertex vert)
        {
            if (vert.id < 0) { return; }
            if (vertex_ids.Contains(vert.id))
                return;
            Logic_AddVertex(vert);
        }
        private void Logic_AddVertex(Vertex vert)
        {
            vertices.Add(vert);
            vertex_ids.Add(vert.id);
        }
        public void AddEdge(Edge edge)
        {
            if (edge.id < 0) { return; }
            if (!edge_ids.Contains(edge.connections[0]) || !edge_ids.Contains(edge.connections[1]) || edge_ids.Contains(edge.id))
                return;
            Logic_AddEdge(edge);
        }
        public void AddEdge(Edge edge, out string error)
        {
            if (edge.id < 0) { error = "Id не может быть отрицательным."; return; }
            if (!edge_ids.Contains(edge.connections[0]) || !edge_ids.Contains(edge.connections[1]) || edge_ids.Contains(edge.id))
            {
                error = "Вершин(-ы) с указанным(-и) идентификаторам(-и) не существует.";
                return;
            }
            
            if (edge_ids.Contains(edge.id))
            {
                error = "Ребро с этим id уже существует. Для получения списка доступных id воспользуйтесь GetAvailableEdgeIds.";
                return;
            }
            Logic_AddEdge(edge);
            error = "OK.";
        }
        private void Logic_AddEdge(Edge edge)
        {
            edges.Add(edge);
            edge_ids.Add(edge.id);
        }
        public int GetAvailableVertexId()
        {
            if (vertices == null || vertices.Count == 0) return 0;
            if (vertices.Count - 1 == vertices[vertices.Count - 1].id) return vertices.Count;
            for(int i=0;i<vertices.Count;i++){
                if (!vertex_ids.Contains(i))
                    return i;
            }
            return -1;
        }
        public int GetAvailableEdgeId()
        {
            if (edges == null || edges.Count == 0) return 0;
            if (edges.Count - 1 == edges[edges.Count - 1].id) return edges.Count;
            for(int i=0;i<edges.Count;i++){
                if (!edge_ids.Contains(i))
                    return i;
            }
            return -1;
        }
        public void DelVertex(int id, out string error)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].id == id)
                {
                    Logic_DelVertex(i);
                    error = "OK.";
                    return;
                }
            }
            error = "Такой вершины не существует.";
        }
        public void DelVertex(int id)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].id == id)
                {
                    Logic_DelVertex(i);
                    return;
                }
            }
        }
        private void Logic_DelVertex(int index)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].connections[0] == index || edges[i].connections[1] == index)
                {
                    Logic_DelEdge(i);
                    i--;
                }
            }
            vertex_ids.Remove(vertices[index].id);
            vertices.RemoveAt(index);

        }
        public void DelEdge(int id, out string error)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].id == id)
                {
                    Logic_DelEdge(i);
                    error = "OK.";
                    return;
                }
            }
            error = "Такого ребра не существует.";
        }
        public void DelEdge(int id)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].id == id)
                {
                    Logic_DelEdge(id);
                    return;
                }
            }
        }
        private void Logic_DelEdge(int index)
        {
            edge_ids.Remove(edges[index].id);
            edges.RemoveAt(index);

        }
        public Vertex GetVertex(int id, out string error)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].id == id)
                {
                    error = "OK.";
                    return vertices[i];

                }
            }
            error = "Такой вершины не существует.";
            return null;
        }
        public Vertex GetVertex(int id)
        {
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].id == id)
                {
                    return vertices[i];

                }
            }
            return null;
        }
        public Edge GetEdge(int id, out string error)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].id == id)
                {
                    error = "OK.";
                    return edges[i];

                }
            }
            error = "Такой вершины не существует.";
            return null;
        }
        public Edge GetEdge(int id)
        {
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].id == id)
                {
                    return edges[i];

                }
            }
            return null;
        }
        public List<Vertex> GetNeighbours(int id)
        {
            List<Vertex> nei = new List<Vertex>();
            for (int a = 0; a < edges.Count; a++)
            {
                if (edges[a].connections[0] == id)
                {
                    for (int b = 0; b < vertices.Count; b++)
                    {
                        if (vertices[b].id == edges[a].connections[1])
                            nei.Add(vertices[b]);
                    }
                }
                else if (edges[a].connections[1] == id)
                {
                    for (int b = 0; b < vertices.Count; b++)
                    {
                        if (vertices[b].id == edges[a].connections[0])
                            nei.Add(vertices[b]);
                    }

                }
            }
            return nei;
        }
        public int[] Dijkstra(int start, int goal)
        {
            List<Node> nodes = new List<Node>();
            List<Node> visnode = new List<Node>();
            HashSet<int> visid = new HashSet<int>();

            nodes.Add(new Node(-1, 0, start));
            int index = 0; double min = 0;

            while (true)
            {
                index = 0; min = nodes[0].cost;
                if (nodes.Count == 0) return new int[] { };
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].cost < min)
                    {
                        index = i;
                        min = nodes[i].cost;
                    }
                }
                if (nodes[index].id == goal)
                    break;

                List<Vertex> neighbours = GetNeighbours(nodes[index].id);
                for (int i = 0; i < neighbours.Count; i++)
                {
                    if (!visid.Contains(neighbours[i].id))
                    {
                        double cost = 0;
                        for (int j = 0; j < edges.Count; j++)
                        {
                            if (neighbours[i].id == edges[j].connections[0] && nodes[index].id == edges[j].connections[1] ||
                            neighbours[i].id == edges[j].connections[1] && nodes[index].id == edges[j].connections[0])
                            {
                                cost = edges[j].weight;
                                break;
                            }
                        }
                        nodes.Add(new Node(nodes[index].id, cost + nodes[index].cost, neighbours[i].id));
                    }
                }
                visnode.Add(nodes[index]);
                visid.Add(nodes[index].id);
                nodes.RemoveAt(index);
            }
            List<int> hist = new List<int>();
            Node node = nodes[index];
            while (node.parent != -1)
            {
                hist.Add(node.id);
                for (int i = 0;i<visnode.Count;i++){
                    if(visnode[i].id == node.parent)
                        node = visnode[i];
                }
            }
            int[] h = hist.ToArray();


            return h;
        }

    }
}