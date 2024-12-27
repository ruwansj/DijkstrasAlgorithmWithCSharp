using DijkstrasAlgorithmApi.Models;

namespace DijkstrasAlgorithmApi.Services
{
    public class PathFinderService
    {
        private readonly List<Node> _graphNodes;

        public PathFinderService()
        {
            // Initialize the graph with the nodes from the diagram
            _graphNodes = new List<Node>
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
        }

        public ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNodes = null)
        {
            var nodes = graphNodes ?? _graphNodes;
            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var unvisited = new HashSet<string>();


            foreach (var node in nodes)
            {
                distances[node.Name] = int.MaxValue;
                unvisited.Add(node.Name);
            }
            distances[fromNodeName] = 0;

            while (unvisited.Count > 0)
            {
                // Find minimum distance
                var current = unvisited.MinBy(node => distances[node]);

                if (current == toNodeName)
                    break;

                unvisited.Remove(current);


                var currentNode = nodes.First(n => n.Name == current);

   
                foreach (var edge in currentNode.Edges)
                {
                    var neighbor = edge.Key;
                    var distance = edge.Value;

                    if (!unvisited.Contains(neighbor))
                        continue;

                    var alt = distances[current] + distance;
                    if (alt < distances[neighbor])
                    {
                        distances[neighbor] = alt;
                        previous[neighbor] = current;
                    }
                }
            }

     
            var path = new List<string>();
            var pathNode = toNodeName; 

            while (pathNode != null)
            {
                path.Insert(0, pathNode);
                previous.TryGetValue(pathNode, out pathNode);
            }

  
            if (path.Count == 1 && path[0] != fromNodeName)
            {
                throw new InvalidOperationException("No path exists between the specified nodes.");
            }

            return new ShortestPathData
            {
                NodeNames = path,
                Distance = distances[toNodeName]
            };
        }
    }
}
