using Utilities;

namespace Assignment1.Algorithms
{
    internal static class FastCut
    {
        private const int bruteForceBound = 6;

        public static int SolveFastcut(this Multigraph graph)
        {
            if (graph.V <= bruteForceBound)
            {
                return graph.SolveBruteForce();
            }

            int t = Convert.ToInt32(Math.Ceiling(1 + (graph.V / Math.Sqrt(2))));
            int mincut = int.MaxValue;

            for (int v = graph.V; v > t; v--)
            {
                var h1 = new Multigraph(graph);
                var h2 = new Multigraph(graph);

                h1.ContractRandomEdge();
                h2.ContractRandomEdge();

                int h1Answer = h1.SolveFastcut();
                int h2Answer = h2.SolveFastcut();

                mincut = h1Answer < h2Answer ? h1Answer : h2Answer;
            }

            return mincut;
        }
    }
}
