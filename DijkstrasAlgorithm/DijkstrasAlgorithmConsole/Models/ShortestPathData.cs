using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstrasAlgorithmConsole.Models
{
    public class ShortestPathData
    {
        public List<string> NodeNames { get; set; }
        public int Distance { get; }

        public ShortestPathData(List<string> nodeNames, int distance)
        {
            NodeNames = nodeNames;
            Distance = distance;
        }
    }
}
