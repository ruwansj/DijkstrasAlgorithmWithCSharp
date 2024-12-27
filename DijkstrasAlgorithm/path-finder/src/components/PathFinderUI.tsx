import React, { useState } from 'react';
import { calculateShortestPath } from '../services/api';
import { PathResult } from '../types/types';

const PathFinderUI = () => {
  const [fromNode, setFromNode] = useState<string>('');
  const [toNode, setToNode] = useState<string>('');
  const [result, setResult] = useState<PathResult | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const nodes = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I'];

  const handleCalculate = async () => {
    if (!fromNode || !toNode) {
      setError('Please select both nodes');
      return;
    }

    setLoading(true);
    setError(null);

    try {
      const data = await calculateShortestPath({ fromNode, toNode });
      setResult(data);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'An error occurred');
    } finally {
      setLoading(false);
    }
  };

  const handleClear = () => {
    setFromNode('');
    setToNode('');
    setResult(null);
    setError(null);
  };

  return (
    <div className="min-h-screen bg-gradient-to-b from-blue-600 to-blue-50">
      <div className="max-w-4xl mx-auto pt-8 px-6">
        <h1 className="text-3xl font-semibold text-white text-center">
          Dijkstra's Algorithm Calculator
        </h1>
        <p className="text-white text-center mt-2 mb-8">
          Discovering Optimal Routes Through Nodes Using Dijkstra's Method
        </p>

        {/* Main Card */}
        <div className="bg-white rounded-lg shadow-lg p-6">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
            {/* Left Section */}
            <div>
              <h2 className="text-blue-900 font-medium mb-4">Select Path</h2>
              <div className="space-y-4">
                <div>
                  <label className="block text-gray-600 text-sm mb-1">
                    From Node
                  </label>
                  <select
                    value={fromNode}
                    onChange={(e) => setFromNode(e.target.value)}
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                  >
                    <option value="">Select</option>
                    {nodes.map(node => (
                      <option key={node} value={node}>{node}</option>
                    ))}
                  </select>
                </div>

                <div>
                  <label className="block text-gray-600 text-sm mb-1">
                    To Node
                  </label>
                  <select
                    value={toNode}
                    onChange={(e) => setToNode(e.target.value)}
                    className="w-full p-2 border border-gray-300 rounded focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                  >
                    <option value="">Select</option>
                    {nodes.map(node => (
                      <option key={node} value={node}>{node}</option>
                    ))}
                  </select>
                </div>

                <div className="flex space-x-3 pt-2">
                  <button
                    onClick={handleClear}
                    className="px-4 py-2 text-gray-600 bg-gray-100 rounded hover:bg-gray-200 transition-colors duration-200"
                  >
                    Clear
                  </button>
                  <button
                    onClick={handleCalculate}
                    disabled={loading}
                    className="px-4 py-2 text-white bg-orange-500 rounded hover:bg-orange-600 transition-colors duration-200 disabled:opacity-50 disabled:cursor-not-allowed"
                  >
                    {loading ? 'Calculating...' : 'Calculate'}
                  </button>
                </div>

                {error && (
                  <p className="text-red-500 text-sm mt-2">{error}</p>
                )}
              </div>
            </div>

            {/* Right Section */}
            <div>
              <h2 className="text-blue-900 font-medium mb-4">Result</h2>
              <div className="bg-gray-50 p-4 rounded-lg min-h-[200px]">
                {result ? (
                  <div className="space-y-2 text-gray-600">
                    <p>
                      From Node Name = "{fromNode}", To Node Name = "{toNode}": {result.nodeNames.join(', ')}
                    </p>
                    <p>Total Distance: {result.distance}</p>
                  </div>
                ) : (
                  <div className="h-full flex flex-col items-center justify-center">
                    {loading ? (
                      <p className="text-gray-400">Calculating...</p>
                    ) : (
                      <>
                        <img 
                          src="/family-trip.jpg" 
                          alt="Trip Planning" 
                          className="w-40 h-40 object-contain mb-4"
                        />
                        <p className="text-gray-400 text-center">
                          Select nodes to calculate the shortest path
                        </p>
                      </>
                    )}
                  </div>
                )}
              </div>
              </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default PathFinderUI;