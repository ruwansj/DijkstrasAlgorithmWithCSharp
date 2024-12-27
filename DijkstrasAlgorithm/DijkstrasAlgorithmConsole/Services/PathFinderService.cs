using DijkstrasAlgorithmConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstrasAlgorithmConsole.Services
{
    public class PathFinderService
    {
        public ShortestPathData ShortestPath(string fromNodeName, string toNodeName, List<Node> graphNode)
        {
            // Validate input
            if (!graphNode.Any(n => n.Name == fromNodeName))
                throw new ArgumentException($"Start node '{fromNodeName}' not found in graph");
            if (!graphNode.Any(n => n.Name == toNodeName))
                throw new ArgumentException($"End node '{toNodeName}' not found in graph");

            var distances = new Dictionary<string, int>();
            var previous = new Dictionary<string, string>();
            var unvisited = new PriorityQueue<string, int>();
            var visited = new HashSet<string>();

            // Initialize distances
            foreach (var node in graphNode)
            {
                distances[node.Name] = int.MaxValue;
            }
            distances[fromNodeName] = 0;
            unvisited.Enqueue(fromNodeName, 0);

            while (unvisited.Count > 0)
            {
                var current = unvisited.Dequeue();

                if (current == toNodeName)
                    break;

                if (visited.Contains(current))
                    continue;

                visited.Add(current);

                // Get current node
                var currentNode = graphNode.First(n => n.Name == current);

                // Check all neighbors
                foreach (var edge in currentNode.Edges)
                {
                    var neighbor = edge.Key;
                    var distance = edge.Value;

                    if (visited.Contains(neighbor))
                        continue;

                    var alt = distances[current] + distance;
                    if (alt < distances[neighbor])
                    {
                        distances[neighbor] = alt;
                        previous[neighbor] = current;
                        unvisited.Enqueue(neighbor, alt);
                    }
                }
            }

            // Build path
            if (!distances.ContainsKey(toNodeName) || distances[toNodeName] == int.MaxValue)
            {
                throw new InvalidOperationException($"No path exists between '{fromNodeName}' and '{toNodeName}'");
            }

            var path = new List<string>();
            var pathNode = toNodeName;

            while (pathNode != null)
            {
                path.Insert(0, pathNode);
                previous.TryGetValue(pathNode, out pathNode);
            }

            return new ShortestPathData(path, distances[toNodeName]);
        }
    }
}
