using Assignment1.Algorithms;
using Utilities;

var graph = Multigraph.FromStream(new StreamReader("../../../Graphs/graph3.txt"));

Console.WriteLine($"The graph has {graph.V} vertices.");
Console.WriteLine($"The graph has {graph.E} edges.");

Console.WriteLine("\n================================");

// Console.WriteLine($"The estimated min-cut is : {graph.SolveKarger()}");

// var solver = new BruteForce(graph);
// Console.WriteLine($"The min-cut is (brute-force) : {solver.Solve()}");

Console.WriteLine($"The estimated min-cut is {graph.SolveFastcut()}");