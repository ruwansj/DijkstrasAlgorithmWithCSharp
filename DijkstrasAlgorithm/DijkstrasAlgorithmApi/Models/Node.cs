namespace DijkstrasAlgorithmApi.Models
{
    public class Node
    {
        public string Name { get; set; }
        public Dictionary<string, int> Edges { get; set; } = new();
    }
}
