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
                var bruteForceSolver = new BruteForceMinCutSolver(graph);
                return bruteForceSolver.Solve();
            }

            int t = Convert.ToInt32(Math.Ceiling(1 + (graph.V / Math.Sqrt(2))));

            var h1 = new Multigraph(graph).ContractionSequence(limitVtx: t, enableDisplay: false);
            var h2 = new Multigraph(graph).ContractionSequence(limitVtx: t, enableDisplay: false);

            var cutH1 = h1.SolveFastcut();
            var cutH2 = h2.SolveFastcut();

            return cutH1 < cutH2 ? cutH1 : cutH2;
        }
    }
}
