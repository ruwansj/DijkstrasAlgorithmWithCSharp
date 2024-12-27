import { PathRequest, PathResult } from '../types/types';

const API_URL = 'https://localhost:7019/ShortestPath';

export const calculateShortestPath = async (request: PathRequest): Promise<PathResult> => {
  const response = await fetch(API_URL, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(request),
  });

  if (!response.ok) {
    throw new Error('Failed to calculate path');
  }

  return response.json();
};