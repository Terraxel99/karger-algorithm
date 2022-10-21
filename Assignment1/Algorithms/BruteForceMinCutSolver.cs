using Utilities;

namespace Assignment1.Algorithms
{
    internal class BruteForceMinCutSolver
    {
        private Multigraph graph;

        private int smallestCut = int.MaxValue;

        public BruteForceMinCutSolver(Multigraph g)
            => this.graph = g;

        public int Solve()
        {
            EnumerateSubsets(graph.Vertices.Select(v => v.Number), new List<int>(), 0);

            int smallestCut = this.smallestCut;

            // Reset in case the algorithm is re-executed.
            this.smallestCut = int.MaxValue;
            return smallestCut;
        }

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
