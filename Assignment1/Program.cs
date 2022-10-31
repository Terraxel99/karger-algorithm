using Assignment1;
using Assignment1.Algorithms;
using System.Diagnostics;
using Utilities;

HandleArguments();

static void HandleArguments()
{
    var args = Environment.GetCommandLineArgs();

    try
    {
        var graphFilePath = args[1];
        var nbSeconds = int.Parse(args[2]);
        var algorithm = (AvailableAlgorithms)int.Parse(args[3]);
        var expectedResult = int.Parse(args[4]);

        var graph = Multigraph.FromStream(new StreamReader(graphFilePath));

        Console.WriteLine($"Executing algorithm for {nbSeconds} seconds (finishes last run after time elapsed) :\n");

        int nbRuns = 0;
        int nbCorrectResults = 0;

        var timeBudget = TimeSpan.FromSeconds(nbSeconds);
        var stopWatch = new Stopwatch();
        stopWatch.Start();

        while(stopWatch.Elapsed < timeBudget)
        {
            if (ExecuteAlgorithm(graph, algorithm) == expectedResult)
            {
                nbCorrectResults++;
            }

            nbRuns++;
        }

        var totalTime = stopWatch.Elapsed;
        stopWatch.Stop();

        Console.WriteLine($"\nRuns = {nbRuns}, Correct results = {nbCorrectResults}, Success probability = {(double)nbCorrectResults/nbRuns} (Total runtime = {totalTime})");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        throw new InvalidOperationException("Program arguments are invalid");
    }
}

static int ExecuteAlgorithm(Multigraph g, AvailableAlgorithms algo)
{
    var graphCopy = new Multigraph(g);

    var nbVertices = graphCopy.V;
    var nbEdges = graphCopy.E;

    int result = int.MaxValue;

    switch (algo)
    {
        case AvailableAlgorithms.Karger:
            result = graphCopy.SolveKarger();
            Console.WriteLine($"Karger Contraction algorithm estimated min-cut : {result} ({nbVertices} vertices, {nbEdges} edges).");
            break;

        case AvailableAlgorithms.Fastcut:
            result = graphCopy.SolveFastcut();
            Console.WriteLine($"Fastcut algorithm estimated min-cut : {result} ({nbVertices} vertices, {nbEdges} edges).");
            break;

        default:
            throw new Exception("Unknown algorithm");
    }

    return result;
}