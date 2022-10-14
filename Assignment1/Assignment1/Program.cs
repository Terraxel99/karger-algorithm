using Assignment1.KargerAlgorithm;
using Utilities;

var graph = Multigraph.FromStream(new StreamReader("../../../Graphs/graph1.txt"));

var solver = new KargerAlgorithmSolver(graph);
var answer = solver.Solve();

Console.WriteLine($"The minimum cut is {answer}");