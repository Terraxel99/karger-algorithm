using Utilities.Exceptions;

namespace Utilities
{
    /// <summary>
    /// Toolbox-class used for representing an undirected multigraph.
    /// </summary>
    public class Multigraph
    {
        /// <summary>
        /// The number of vertices.
        /// </summary>
        public int V { get; private set; }
        
        /// <summary>
        /// The number of edges.
        /// </summary>
        public int E { get; private set; }

        /// <summary>
        /// The list of edges in the graph.
        /// </summary>
        private List<int>[] adjacencyMatrix;

        /// <summary>
        /// Creates a multigraph with the given amount of vertices.
        /// </summary>
        /// <param name="v">The amount of vertices</param>
        /// <exception cref="ArgumentException">Thrown if the number of vertices is negative.</exception>
        public Multigraph(int v)
        {
            if (v < 0)
            {
                throw new ArgumentException("There must be a non-negative number of vertices");
            }

            this.V = v;
            this.adjacencyMatrix = new List<int>[v];

            for (int i = 0; i < this.V; i++)
            {
                this.adjacencyMatrix[i] = new List<int>();
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

        public void AddEdge(int v1, int v2)
        {
            this.ValidateVertex(v1);
            this.ValidateVertex(v2);

            this.adjacencyMatrix[v1].Add(v2);
            this.adjacencyMatrix[v2].Add(v1);

            this.E++;
        }

        public void ContractEdge()
        {
            
        }

        public IEnumerable<int> AdjOf(int vertex)
        {
            this.ValidateVertex(vertex);
            return this.adjacencyMatrix[vertex];
        }

        public int DegreeOf(int vertex)
        {
            this.ValidateVertex(vertex);
            return this.adjacencyMatrix[vertex].Count;
        }

        private void ValidateVertex(int number)
        {
            if (number < 0 || number >= this.V)
            {
                throw new ArgumentException($"The vertex {number} is not in the graph.");
            }
        }
    }
}