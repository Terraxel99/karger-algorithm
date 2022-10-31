using Utilities.Exceptions;

namespace Utilities
{
    /// <summary>
    /// Class representing a multigraph.
    /// </summary>
    public class Multigraph
    {
        private List<Vertex> vertices;

        private List<List<Vertex>> adjacencyMatrix;

        /// <summary>
        /// The amount of edges.
        /// </summary>
        public int E { get; private set; }

        /// <summary>
        /// The amount of vertices.
        /// </summary>
        public int V
        {
            get => this.vertices.Count;
        }

        /// <summary>
        /// The vertices.
        /// </summary>
        public IEnumerable<Vertex> Vertices
        {
            get => this.vertices;
        }

        /// <summary>
        /// Creates a multigraph with a given amount of vertices.
        /// </summary>
        /// <param name="v">The amount of vertices.</param>
        /// <exception cref="ArgumentException">When the amount of vertices is invalid.</exception>
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

        /// <summary>
        /// Creates a copy of a given multigraph.
        /// </summary>
        /// <param name="g">The multigraph to replicate.</param>
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

        /// <summary>
        /// Creates a multigraph from an input stream.
        /// </summary>
        /// <param name="stream">The input stream.</param>
        /// <returns>The multigraph.</returns>
        /// <exception cref="ArgumentException">When the stream is invalid.</exception>
        /// <exception cref="InvalidGraphDefinitionException">When the graph structure is invalid.</exception>
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

        /// <summary>
        /// Determines if the graph has isolated vertices.
        /// </summary>
        /// <returns>Whether the graph has isolated vertices.</returns>
        public bool HasIsolatedVertices()
            => this.adjacencyMatrix.Any(m => m.Count == 0);
        
        /// <summary>
        /// Performs a contraction on a random edge of the graph.
        /// </summary>
        /// <param name="enableDisplay">Whether the operation should be outputted to standard output or not.</param>
        /// <exception cref="InvalidOperationException">If the operation cannot be performed.</exception>
        public void ContractRandomEdge(bool enableDisplay = true)
        {
            if (this.E < 1)
            {
                throw new InvalidOperationException("There must be an edge to perform contraction.");
            }

            var randomGenerator = new Random();
            var rndOrigin = randomGenerator.Next(0, this.V);

            var rndTemp = randomGenerator.Next(0, this.DegreeOf(rndOrigin));
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

        /// <summary>
        /// Adds an edge between two vertices.
        /// </summary>
        /// <param name="v">The first vertex.</param>
        /// <param name="w">The second vertex.</param>
        public void AddEdge(int v, int w)
        {
            this.ValidateVertex(v);
            this.ValidateVertex(w);

            this.adjacencyMatrix[v].Add(this.vertices[w]);
            this.adjacencyMatrix[w].Add(this.vertices[v]);

            this.E++;
        }

        /// <summary>
        /// Gets the neighbors of the given vertex.
        /// </summary>
        /// <param name="vertex">The vertex to retrieve the neighbors of.</param>
        /// <returns>The enumeration of neighbors.</returns>
        public IEnumerable<int> AdjOf(int vertex)
        {
            this.ValidateVertex(vertex);
            return this.adjacencyMatrix[vertex].Select(v => v.Number);
        }

        /// <summary>
        /// Gets the degree of a vertex.
        /// </summary>
        /// <param name="vertex">The vertex to compute the degree of.</param>
        /// <returns>The degree.</returns>
        public int DegreeOf(int vertex)
        {
            this.ValidateVertex(vertex);
            return this.adjacencyMatrix[vertex].Count;
        }

        /// <summary>
        /// Adds an edge between two vertices.
        /// </summary>
        /// <param name="v">The first vertex.</param>
        /// <param name="w">The second vertex.</param>
        private void AddEdge(Vertex v, Vertex w)
            => this.AddEdge(v.Number, w.Number);

        /// <summary>
        /// Removes a vertex and recalculates the data structure accordingly.
        /// </summary>
        /// <param name="vertex">The vertex that should be removed.</param>
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

        /// <summary>
        /// Ensures a given vertex is in the graph.
        /// </summary>
        /// <param name="vertex">The vertex to check.</param>
        /// <exception cref="ArgumentException">If the vertex is not in the graph.</exception>
        private void ValidateVertex(int vertex)
        {
            if (vertex < 0 || vertex >= this.V)
            {
                throw new ArgumentException($"The vertex {vertex} is not in the graph.");
            }
        }
    }
}
