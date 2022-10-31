using Assignment1.Algorithms;
using Utilities;

var graph = Multigraph.FromStream(new StreamReader("../../../Graphs/graph_k{100}_.txt"));

Console.WriteLine($"The graph has {graph.V} vertices.");
Console.WriteLine($"The graph has {graph.E} edges.");

Console.WriteLine("\n================================");

//Console.WriteLine($"The estimated min-cut is : {graph.SolveKarger()}");

// var solver = new BruteForceMinCutSolver(graph);
// Console.WriteLine($"The min-cut is (brute-force) : {solver.Solve()}");

// Console.WriteLine(graph.HasIsolatedVertices());

Console.WriteLine($"The estimated min-cut is {graph.SolveFastcut()}");