export interface PathRequest {
    fromNode: string;
    toNode: string;
  }
  
  export interface PathResult {
    nodeNames: string[];
    distance: number;
  }