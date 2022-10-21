using Utilities.Exceptions;

namespace Utilities
{
    public class Multigraph
    {
        private List<Vertex> vertices;

        private List<List<Vertex>> adjacencyMatrix;

        public int E { get; private set; }

        public int V
        {
            get => this.vertices.Count;
        }

        public Multigraph(int v)
        {
            if (v < 0)
            {
                throw new ArgumentException("There must be a non-negative number of vertices");
            }

            this.E = 0;
            this.vertices = new List<Vertex>(v);
            this.adjacencyMatrix = new List<List<Vertex>>(v);

            for (int i = 0; i < v; i++)
            {
                this.vertices.Add(new Vertex(i));
                this.adjacencyMatrix.Add(new List<Vertex>());
            }
        }

        public void Test()
        {
            vertices[0].Number = 32000;
        }

        public Multigraph(Multigraph g)
            : this(g.V)
        {
            this.E = g.E;

            for (int v = 0; v < g.V; v++)
            {
                Stack<int> reverse = new Stack<int>();

                foreach (var neighbour in g.AdjOf(v))
                {
                    reverse.Push(neighbour);
                }

                foreach (var vertex in reverse)
                {
                    this.adjacencyMatrix[v].Add(this.vertices[vertex]);
                }
            }
        }

        public static Multigraph FromStream(StreamReader stream)
        {
            if (stream == null)
            {
                throw new ArgumentException("The provided stream should be non-null");
            }

            try
            {
                var nbVertices = int.Parse(stream.ReadLine());
                var nbEdges = int.Parse(stream.ReadLine());

                var graph = new Multigraph(nbVertices);

                for (int i = 0; i < nbEdges; i++)
                {
                    var pair = stream.ReadLine()?.Split(" ");
                    graph.AddEdge(int.Parse(pair[0]), int.Parse(pair[1]));
                }

                return graph;
            }
            catch (Exception e)
            {
                throw new InvalidGraphDefinitionException("An error occured while parsing the graph.", e);
            }
        }
        
        public void ContractRandomEdge(bool enableDisplay = true)
        {
            if (this.E < 1)
            {
                throw new InvalidOperationException("There must be an edge to perform contraction.");
            }

            var randomGenerator = new Random();
            var rndOrigin = randomGenerator.Next(0, this.V);

            var rndTemp = randomGenerator.Next(0, this.adjacencyMatrix[rndOrigin].Count);
            var rndEnd = this.vertices[this.adjacencyMatrix[rndOrigin][rndTemp].Number].Number;

            if (enableDisplay)
            {
                Console.WriteLine($"Contracting ({rndOrigin}, {rndEnd}).");
            }

            this.adjacencyMatrix[rndOrigin].RemoveAll(v => v == this.vertices[rndEnd]);
            this.adjacencyMatrix[rndEnd].RemoveAll(v => v == this.vertices[rndOrigin]);

            this.adjacencyMatrix[rndEnd].ForEach(v => this.AddEdge(this.vertices[rndOrigin], v));

            this.RemoveVertex(rndEnd);
        }

        public void AddEdge(int v, int w)
        {
            this.ValidateVertex(v);
            this.ValidateVertex(w);

            this.adjacencyMatrix[v].Add(this.vertices[w]);
            this.adjacencyMatrix[w].Add(this.vertices[v]);

            this.E++;
        }

        public IEnumerable<int> AdjOf(int vertex)
        {
            this.ValidateVertex(vertex);
            return this.adjacencyMatrix[vertex].Select(v => v.Number);
        }

        public int DegreeOf(int vertex)
        {
            this.ValidateVertex(vertex);
            return this.adjacencyMatrix[vertex].Count;
        }

        private void AddEdge(Vertex v, Vertex w)
            => this.AddEdge(v.Number, w.Number);

        private void RemoveVertex(int vertex)
        {
            foreach (var neighbor in this.adjacencyMatrix[vertex])
            {
                for (int i = 0; i < this.adjacencyMatrix[neighbor.Number].Count; i++)
                {
                    if (this.adjacencyMatrix[neighbor.Number][i] == vertex)
                    {
                        this.adjacencyMatrix[neighbor.Number].RemoveAt(i);
                    }
                }
            }

            this.adjacencyMatrix.RemoveAt(vertex);
            this.vertices.RemoveAt(vertex);

            // We rename the vertices that had a number higher that the one that has been removed.
            for (int i = vertex; i < this.V; i++)
            {
                this.vertices[i].Number--;
            }
        }

        private void ValidateVertex(int vertex)
        {
            if (vertex < 0 || vertex >= this.V)
            {
                throw new ArgumentException($"The vertex {vertex} is not in the graph.");
            }
        }
    }
}
