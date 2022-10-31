using Utilities;

namespace Assignment1.Algorithms
{
    /// <summary>
    /// Class dedicated to the bruteforce solving of minimum cut problem.
    /// </summary>
    internal class BruteForceMinCutSolver
    {
        private Multigraph graph;

        private int smallestCut = int.MaxValue;

        /// <summary>
        /// Creates an instance of the bruteforce algorithm for minimum cut on the given graph.
        /// </summary>
        /// <param name="g">The graph on which bruteforce algorithm will be executed.</param>
        public BruteForceMinCutSolver(Multigraph g)
            => this.graph = g;

        /// <summary>
        /// Solves the minimum cut problem.
        /// </summary>
        /// <returns>The cut of minimum size.</returns>
        public int Solve()
        {
            EnumerateSubsets(graph.Vertices.Select(v => v.Number), new List<int>(), 0);

            int smallestCut = this.smallestCut;

            // Reset in case the algorithm is re-executed.
            this.smallestCut = int.MaxValue;
            return smallestCut;
        }

        /// <summary>
        /// Enumerates all the subsets of a set recursively.
        /// </summary>
        /// <param name="nums">All the elements of the superset.</param>
        /// <param name="output">The output.</param>
        /// <param name="index">The current index.</param>
        private void EnumerateSubsets(IEnumerable<int> nums, List<int> output, int index)
        {
            // Base Condition
            if (index == this.graph.V)
            {
                this.DetermineCutsizeOfSet(output);
                return;
            }

            EnumerateSubsets(nums, new List<int>(output), index + 1);

            output.Add(nums.ElementAt(index));
            EnumerateSubsets(nums, new List<int>(output), index + 1);
        }

        /// <summary>
        /// Determines the size of the cut for a given partition of the set of vertices.
        /// </summary>
        /// <param name="vertices">The first set of the partition.</param>
        private void DetermineCutsizeOfSet(List<int> vertices)
        {
            // A cut-set must be a PROPER NON-EMPTY subset of vertices.
            if (vertices.Count == 0 || vertices.Count == this.graph.V)
            {
                return;
            }

            int cutsize = 0;

            foreach (int vertex in vertices)
            {
                foreach (var neighbour in this.graph.AdjOf(vertex))
                {
                    if (!vertices.Contains(neighbour))
                    {
                        cutsize++;
                    }
                }
            }

            if (cutsize < this.smallestCut)
            {
                this.smallestCut = cutsize;
            }
        }
    }
}
