// See https://aka.ms/new-console-template for more information
using DijkstrasAlgorithmConsole.Models;
using DijkstrasAlgorithmConsole.Services;

class Program
{
    static void Main(string[] args)
    {
        var pathFinder = new PathFinderService();
        var graphNodes = new List<Node>
        {
            new Node { Name = "A", Edges = new Dictionary<string, int> { { "B", 4 }, { "C", 6 } } },
            new Node { Name = "B", Edges = new Dictionary<string, int> { { "F", 2 } } },
            new Node { Name = "C", Edges = new Dictionary<string, int> { { "D", 8 } } },
            new Node { Name = "D", Edges = new Dictionary<string, int> { { "G", 1 }, { "E", 4 } } },
            new Node { Name = "E", Edges = new Dictionary<string, int> { { "B", 2 }, { "D", 4 },{ "F", 3 } } },
            new Node { Name = "F", Edges = new Dictionary<string, int> { { "H", 6 }, { "E", 3 }, { "G", 4 } } },
            new Node { Name = "G", Edges = new Dictionary<string, int> { { "I", 5 }, { "D", 1 }, { "F", 4 }, { "H", 5 } } },
            new Node { Name = "H", Edges = new Dictionary<string, int> { { "F", 6 }, { "G", 5 } } },
            new Node { Name = "I", Edges = new Dictionary<string, int>() }
        };

        while (true)
        {
            Console.Write("Enter start node (or 'exit' to quit): ");
            var fromNode = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrEmpty(fromNode) || fromNode.ToLower() == "exit")
                break;

            Console.Write("Enter end node: ");
            var toNode = Console.ReadLine()?.ToUpper();

            if (string.IsNullOrEmpty(toNode))
                break;

            try
            {
                var result = pathFinder.ShortestPath(fromNode, toNode, graphNodes);
                Console.WriteLine($"> fromNodeName = \"{fromNode}\", toNodeName = \"{toNode}\": {string.Join(", ", result.NodeNames)}");
                Console.WriteLine($"> Total Distance: {result.Distance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();
        }
    }
}