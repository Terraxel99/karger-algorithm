using Utilities;

namespace Assignment1.KargerAlgorithm
{
    internal class KargerAlgorithmSolver
    {
        public KargerAlgorithmSolver(Multigraph g)
        {
            throw new NotImplementedException();
        }

        public int SolveMultiple(int n)
        {
            if (n < 1)
            {
                throw new ArgumentException("Algorithm should be executed at least once.");
            }

            int finalAnswer = this.Solve();

            // Run (n-1) times because we already ran it once just above.
            for (int i = 0; i < n - 1; i++)
            {
                var answer = this.Solve();
                
                if (answer < finalAnswer)
                {
                    finalAnswer = answer;
                }
            }

            return finalAnswer;
        }

        public int Solve()
            => 0;
    }
}
