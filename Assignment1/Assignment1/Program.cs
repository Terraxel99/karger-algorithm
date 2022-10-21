using Utilities;

var graph = Multigraph.FromStream(new StreamReader("../../../Graphs/graph2.txt"));

Console.WriteLine($"The graph has {graph.V} vertices.");
Console.WriteLine($"The graph has {graph.E} edges.");

Console.WriteLine("\n================================");

