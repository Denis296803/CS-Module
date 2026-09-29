using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
public class Grapher
{
    class Node
    {
        public int parent;
        public int id;
        public int cost;
        public Node(int par, int co, int i) { parent = par; id = i; cost = co; }
    }
    public static int[] Set(int[] a, int[] b)
    {
        int[] outer = new int[a.Length];
        for (int i = 0; i < a.Length; i++) outer[i] = a[i];
        for (int c1 = 0; c1 < b.Length; c1++)
        {
            bool is_in = false;
            for (int c2 = 0; c2 < a.Length; c2++)
            {
                if (b[c1] == a[c2])
                {
                    is_in = true;
                }
            }
            if (!is_in)
            {
                int[] outertemp = new int[outer.Length + 1];
                for (int i = 0; i < outer.Length; i++) outertemp[i] = outer[i];
                outertemp[outer.Length] = b[c1];
                outer = outertemp;
            }
        }
        return outer;
    }
    public static float[] Set(float[] a, float[] b)
    {
        float[] outer = new float[a.Length];
        for (int i = 0; i < a.Length; i++) outer[i] = a[i];
        for (int c1 = 0; c1 < b.Length; c1++)
        {
            bool is_in = false;
            for (int c2 = 0; c2 < a.Length; c2++)
            {
                if (b[c1] == a[c2])
                {
                    is_in = true;
                }
            }
            if (!is_in)
            {
                float[] outertemp = new float[outer.Length + 1];
                for (int i = 0; i < outer.Length; i++) outertemp[i] = outer[i];
                outertemp[outer.Length] = b[c1];
                outer = outertemp;
            }
        }
        return outer;
    }
    public static bool is_in(int[] arr, int el)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == el) return true;
        }
        return false;
    }
    public static T[] Add<T>(T[] a, T[] b)
    {
        T[] output = new T[a.Length + b.Length];
        for (int i = 0; i < a.Length; i++) output[i] = a[i];
        for (int i = a.Length; i < b.Length + a.Length; i++) output[i] = b[i - a.Length];
        return output;
    }

    public static int Min(int[][][] queue)
    {
        int min = 0;
        for (int i = 0; i < queue.Length; i++)
        {
            if (queue[i][1][0] < queue[min][1][0])
            {
                min = i;
            }
        }
        return min;
    }
    public class Edge
    {
        public int id, direction;
        public int[] connections = new int[2];
        public float weight;
        public Edge(int Id, int[] Connections, float Weight = 1, int Direction = 0)
        {
            id = Id; connections = Connections; weight = Weight; direction = Direction;
        }
    }
    public class Vertex
    {
        public int id; public int[] connections;
        public Vertex(int Id, int[] Connections)
        {
            id = Id; connections = Connections;
        }
    }

    public class Graph
    {
        public Vertex[] vertices = new Vertex[0]; public Edge[] edges = new Edge[0];
        public void AddVertex(Vertex vert, out string error)
        {
            if (vert.id < 0) { error = "Id не может быть отрицательным."; return; }
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id == vert.id)
                {

                    error = "Вершина с этим id уже существует. Для получения списка доступных id воспользуйтесь GetAvailableVertexIds.";
                    return;
                }
            }
            Logic_AddVertex(vert);
            error = "OK.";
        }
        public void AddVertex(Vertex vert)
        {
            if (vert.id < 0) { return; }
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id == vert.id)
                    return;
            }
            Logic_AddVertex(vert);
        }
        private void Logic_AddVertex(Vertex vert)
        {
            Vertex[] verticestemp = new Vertex[vertices.Length + 1];
            for (int i = 0; i < vertices.Length; i++) verticestemp[i] = vertices[i];
            verticestemp[vertices.Length] = vert;
            vertices = verticestemp;
        }
        public void AddEdge(Edge edge)
        {
            if (edge.id < 0) { return; }
            bool exist1 = false, exist2 = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (edge.connections[0] == vertices[i].id) exist1 = true;
                if (edge.connections[1] == vertices[i].id) exist2 = true;
            }
            if (!exist1 || !exist2)
            {
                return;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i].id == edge.id)
                    return;
            }
            Logic_AddEdge(edge);
        }
        public void AddEdge(Edge edge, out string error)
        {
            if (edge.id < 0) { error = "Id не может быть отрицательным."; return; }
            bool exist1 = false, exist2 = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (edge.connections[0] == vertices[i].id) exist1 = true;
                if (edge.connections[1] == vertices[i].id) exist2 = true;
            }
            if (!exist1 || !exist2)
            {
                error = "Вершин(-ы) с указанным(-и) идентификаторам(-и) не существует.";
                return;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i].id == edge.id)
                {
                    error = "Ребро с этим id уже существует. Для получения списка доступных id воспользуйтесь GetAvailableEdgeIds.";
                    return;
                }
            }
            Logic_AddEdge(edge);
            error = "OK.";
        }
        private void Logic_AddEdge(Edge edge)
        {
            Edge[] edgestemp = new Edge[edges.Length + 1];
            for (int i = 0; i < edges.Length; i++) edgestemp[i] = edges[i];
            edgestemp[edges.Length] = edge;
            edges = edgestemp;
        }
        public int GetAvailableVertexId()
        {
            if (vertices == null || vertices.Length == 0) return 0;
            if (vertices.Length - 1 == vertices[vertices.Length - 1].id) return vertices.Length;
            bool[] availableids = new bool[vertices.Length];
            for (int i = 0; i < vertices.Length; i++) availableids[i] = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id < vertices.Length) availableids[i] = true;
            }
            for (int i = 0; i < vertices.Length; i++)
            {
                if (!availableids[i]) return i;
            }
            return -1;
        }
        public int GetAvailableEdgeId()
        {
            if (edges == null || edges.Length == 0) return 0;
            if (edges.Length - 1 == edges[edges.Length - 1].id) return edges.Length;
            bool[] availableids = new bool[edges.Length];
            for (int i = 0; i < edges.Length; i++) availableids[i] = false;
            for (int i = 0; i < edges.Length; i++)
            {
                if (edges[i].id < edges.Length) availableids[i] = true;
            }
            for (int i = 0; i < edges.Length; i++)
            {
                if (!availableids[i]) return i;
            }
            return -1;
        }
        public void DelVertex(int id, out string error)
        {
            for (int i = 0; i < vertices.Length; i++)
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
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id == id)
                {
                    Logic_DelVertex(id);
                    return;
                }
            }
        }
        private void Logic_DelVertex(int index)
        {
            Vertex[] temp = new Vertex[vertices.Length - 1];
            for (int i = 0; i < index; i++) temp[i] = vertices[i];
            for (int i = index + 1; i < vertices.Length; i++) temp[i] = vertices[i];
            vertices = temp;

        }
        public void Deledge(int id, out string error)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id == id)
                {
                    Logic_DelEdge(i);
                    error = "OK.";
                    return;
                }
            }
            error = "Такого ребра не существует.";
        }
        public void Deledge(int id)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id == id)
                {
                    Logic_DelEdge(id);
                    return;
                }
            }
        }
        private void Logic_DelEdge(int index)
        {
            Edge[] temp = new Edge[vertices.Length - 1];
            for (int i = 0; i < index; i++) temp[i] = edges[i];
            for (int i = index + 1; i < vertices.Length; i++) temp[i] = edges[i];
            edges = temp;

        }
        public Vertex GetVertex(int id, out string error)
        {
            for (int i = 0; i < vertices.Length; i++)
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
            for (int i = 0; i < vertices.Length; i++)
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
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id == id)
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
            for (int i = 0; i < vertices.Length; i++)
            {
                if (vertices[i].id == id)
                {
                    return edges[i];

                }
            }
            return null;
        }
        public Vertex[] GetNeighbours(int id)
        {
            Vertex[] nei = new Vertex[0];
            for (int a = 0; a < edges.Length; a++)
            {
                if (edges[a].connections[0] == id)
                {
                    for (int b = 0; b < vertices.Length; b++)
                    {
                        if (vertices[b].id == edges[a].connections[1])
                            nei = Add(nei, new Vertex[] { vertices[b] });
                    }
                }
                else if (edges[a].connections[1] == id)
                {
                    for (int b = 0; b < vertices.Length; b++)
                    {
                        if (vertices[b].id == edges[a].connections[0])
                            nei = Add(nei, new Vertex[] { vertices[b] });
                    }

                }
            }
            return nei;
        }
        public static Graph operator +(Graph graph1, Graph graph2)
        {
            Graph graph3 = new Graph();
            for (int a = 0; a < graph2.vertices.Length; a++)
            {
                for (int b = 0; b < graph1.vertices.Length; b++)
                {
                    if (graph1.vertices[a].id == graph2.vertices[b].id)
                    {
                        int[] v = Set(graph1.vertices[a].connections, graph2.vertices[b].connections);
                        graph3.AddVertex(new Vertex(graph1.vertices[a].id, v));
                    }
                    else
                    {
                        graph3.AddVertex(graph1.vertices[a]);
                        graph3.AddVertex(graph2.vertices[b]);
                    }
                }
            }
            for (int a = 0; a < graph2.edges.Length; a++)
            {
                for (int b = 0; b < graph1.edges.Length; b++)
                {
                    if (graph1.edges[a].id == graph2.edges[b].id)
                    {
                        int[] v = Set(graph1.edges[a].connections, graph2.edges[b].connections);
                        graph3.AddEdge(new Edge(graph1.edges[a].id, v));
                    }
                    else
                    {
                        graph3.AddEdge(graph1.edges[a]);
                        graph3.AddEdge(graph2.edges[b]);
                    }
                }
            }
            return graph3;
        }
        public int[] A(int start, int goal)
        {
            List<Node> nodes = new List<Node>();
            List<Node> visnode = new List<Node>();
            HashSet<int> visid = new HashSet<int>();

            nodes.Add(new Node(-1, 0, start));
            int index = 0, min = 0;

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

                Vertex[] neighbours = GetNeighbours(nodes[index].id);
                for (int i = 0; i < neighbours.Length; i++)
                {
                    if (!visid.Contains(neighbours[i].id))
                    {
                        int cost = 0;
                        for (int j = 0; j < edges.Length; j++)
                        {
                            if (neighbours[i].id == edges[j].connections[0] && nodes[index].id == edges[j].connections[1] ||
                            neighbours[i].id == edges[j].connections[1] && nodes[index].id == edges[j].connections[0])
                            {
                                cost = (int)edges[j].weight;
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
class Program
{
    static void Main(string[] args)
    {
        Grapher.Graph graph = new Grapher.Graph();

        // ===== Компонента 1: вершины 0, 1, 2, 3 =====
        // Вершины (connections можно указать для наглядности, но алгоритм их не использует)
        graph.AddVertex(new Grapher.Vertex(0, new int[] { 1, 2 }));
        graph.AddVertex(new Grapher.Vertex(1, new int[] { 0, 2 }));
        graph.AddVertex(new Grapher.Vertex(2, new int[] { 0, 1, 3 }));
        graph.AddVertex(new Grapher.Vertex(3, new int[] { 2 }));

        // Рёбра (id, connections[2], weight)
        graph.AddEdge(new Grapher.Edge(0, new int[] { 0, 1 }, 4)); // 0-1 вес 4
        graph.AddEdge(new Grapher.Edge(1, new int[] { 0, 2 }, 2)); // 0-2 вес 2
        graph.AddEdge(new Grapher.Edge(2, new int[] { 1, 2 }, 5)); // 1-2 вес 5
        graph.AddEdge(new Grapher.Edge(3, new int[] { 2, 3 }, 3)); // 2-3 вес 3

        // ===== Компонента 2: вершины 10, 11, 12 =====
        graph.AddVertex(new Grapher.Vertex(10, new int[] { 11 }));
        graph.AddVertex(new Grapher.Vertex(11, new int[] { 10, 12 }));
        graph.AddVertex(new Grapher.Vertex(12, new int[] { 11 }));

        graph.AddEdge(new Grapher.Edge(10, new int[] { 10, 11 }, 1)); // 10-11 вес 1
        graph.AddEdge(new Grapher.Edge(11, new int[] { 11, 12 }, 2)); // 11-12 вес 2

        // ===== Компонента 3: одиночная вершина 20 =====
        graph.AddVertex(new Grapher.Vertex(20, new int[0]));

        // Теперь можно вызывать, например:
        // int[] path = graph.A(0, 3);  // должно вернуть {0, 2, 3}
        // int[] noPath = graph.A(0, 20); // вернёт {-1} (пути нет)
    }
}