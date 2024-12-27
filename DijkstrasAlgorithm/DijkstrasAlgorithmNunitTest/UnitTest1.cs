using DijkstrasAlgorithmConsole.Models;
using DijkstrasAlgorithmConsole.Services;
using NUnit.Framework;
namespace DijkstrasAlgorithmNunitTest
{


    [TestFixture]
    public class PathFinderServiceTests
    {
        private PathFinderService _pathFinder;
        private List<Node> _testGraph;

        [SetUp]
        public void Setup()
        {
            _pathFinder = new PathFinderService();
            // Initialize test graph before each test
            _testGraph = new List<Node>
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

        [Test]
        public void ShortestPath_FromAToD_ReturnsCorrectPathAndDistance()
        {
            // Arrange
            var expectedPath = new List<string> { "A", "B", "F", "G", "D" };
            var expectedDistance = 11; // 4 + 2 + 4 + 1 = 11

            // Act 
            var result = _pathFinder.ShortestPath("A", "D", _testGraph);

            // Assert
            Assert.That(result.NodeNames, Is.EqualTo(expectedPath));
            Assert.That(result.Distance, Is.EqualTo(expectedDistance));
        }

        [Test]
        public void ShortestPath_FromAToI_ReturnsCorrectPathAndDistance()
        {
            // Arrange
            var expectedPath = new List<string> { "A", "B", "F", "G", "I" };
            var expectedDistance = 15; // 4 + 2 + 4 + 5 = 15

            // Act
            var result = _pathFinder.ShortestPath("A", "I", _testGraph);

            // Assert
            Assert.That(result.NodeNames, Is.EqualTo(expectedPath));
            Assert.That(result.Distance, Is.EqualTo(expectedDistance));
        }

        [Test]
        public void ShortestPath_FromEToB_ReturnsCorrectDirectPath()
        {
            // Arrange
            var expectedPath = new List<string> { "E", "B" };
            var expectedDistance = 2;

            // Act
            var result = _pathFinder.ShortestPath("E", "B", _testGraph);

            // Assert
            Assert.That(result.NodeNames, Is.EqualTo(expectedPath));
            Assert.That(result.Distance, Is.EqualTo(expectedDistance));
        }

        [Test]
        public void ShortestPath_FromBToE_ThrowsException_AsPathDoesNotExist()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() =>
                _pathFinder.ShortestPath("B", "E", _testGraph));
        }

        [Test]
        public void ShortestPath_WithInvalidStartNode_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
                _pathFinder.ShortestPath("X", "A", _testGraph));
        }

        [Test]
        public void ShortestPath_WithInvalidEndNode_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
                _pathFinder.ShortestPath("A", "X", _testGraph));
        }

        [Test]
        public void ShortestPath_WithEmptyGraph_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
                _pathFinder.ShortestPath("A", "B", new List<Node>()));
        }

        [Test]
        public void ShortestPath_FromNodeToItself_ReturnsPathWithSingleNode()
        {
            // Arrange
            var expectedPath = new List<string> { "A" };
            var expectedDistance = 0;

            // Act
            var result = _pathFinder.ShortestPath("A", "A", _testGraph);

            // Assert
            Assert.That(result.NodeNames, Is.EqualTo(expectedPath));
            Assert.That(result.Distance, Is.EqualTo(expectedDistance));
        }

        [Test]
        public void ShortestPath_VerifyDirectionalEdges()
        {
            // Test cases for verifying directional edges
            var testCases = new[]
            {
            new { Start = "E", End = "B", ExpectedDistance = 2, ShouldExist = true },
            new { Start = "B", End = "E", ExpectedDistance = 0, ShouldExist = false },
            new { Start = "F", End = "E", ExpectedDistance = 3, ShouldExist = true },
            new { Start = "E", End = "F", ExpectedDistance = 0, ShouldExist = false }
        };

            foreach (var testCase in testCases)
            {
                if (testCase.ShouldExist)
                {
                    var result = _pathFinder.ShortestPath(testCase.Start, testCase.End, _testGraph);
                    Assert.That(result.Distance, Is.EqualTo(testCase.ExpectedDistance),
                        $"Path from {testCase.Start} to {testCase.End} should have distance {testCase.ExpectedDistance}");
                }
                else
                {
                    Assert.Throws<InvalidOperationException>(() =>
                        _pathFinder.ShortestPath(testCase.Start, testCase.End, _testGraph),
                        $"Path from {testCase.Start} to {testCase.End} should not exist");
                }
            }
        }

        [Test]
        public void ShortestPath_MultiplePathsExist_ReturnsShortestOne()
        {
            // Arrange
            var graphWithMultiplePaths = new List<Node>
        {
            new Node { Name = "A", Edges = new Dictionary<string, int> { { "B", 1 }, { "C", 2 } } },
            new Node { Name = "B", Edges = new Dictionary<string, int> { { "D", 4 } } },
            new Node { Name = "C", Edges = new Dictionary<string, int> { { "D", 1 } } },
            new Node { Name = "D", Edges = new Dictionary<string, int>() }
        };

            var expectedPath = new List<string> { "A", "C", "D" };
            var expectedDistance = 3; // A->C->D (2+1) instead of A->B->D (1+4)

            // Act
            var result = _pathFinder.ShortestPath("A", "D", graphWithMultiplePaths);

            // Assert
            Assert.That(result.NodeNames, Is.EqualTo(expectedPath));
            Assert.That(result.Distance, Is.EqualTo(expectedDistance));
        }
    }
}