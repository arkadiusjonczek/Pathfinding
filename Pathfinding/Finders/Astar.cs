using Pathfinding.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Finders
{
    /// <summary>
    /// A* Algorithmn class
    /// </summary>
    public class Astar : ISearchAlgorithm
    {
        #region Members

        private Graph Graph;
        private HeuristicType heuristicType;

        #endregion

        #region Properties

        /// <summary>
        /// Search options
        /// </summary>
        public SearchOptions SearchOptions { get; private set; }

        /// <summary>
        /// Search successful?
        /// </summary>
        public bool Found { get; private set; }

        /// <summary>
        /// Contains Nodes to check
        /// </summary>
        public List<Node> OpenList { get; private set; }

        /// <summary>
        /// Contains already checked Nodes
        /// This Nodes won't be checked anymore
        /// </summary>
        public List<Node> ClosedList { get; private set; }

        #endregion

        #region Events

        /// <summary>
        /// Path was found
        /// </summary>
        public event PathEventHandler FoundPath;

        /// <summary>
        /// Search is complete
        /// </summary>
        public event AlgorithmEventHandler SearchCompleted;

        /// <summary>
        /// Search was started
        /// </summary>
        public event AlgorithmEventHandler SearchStarted;

        /// <summary>
        /// Checking one Node for Adjacents
        /// </summary>
        public event AlgorithmEventHandler CheckingNode;

        /// <summary>
        /// Checking one Adjacent of a Node
        /// </summary>
        public event AdjacentEventHandler CheckingNodeAdjacent;

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="heuristicType"></param>
        public Astar(Graph graph, HeuristicType heuristicType = HeuristicType.Manhattan)
        {
            this.Graph = graph;
            this.heuristicType = heuristicType;
            this.SearchOptions = new SearchOptions();
            this.Found = false;
            this.OpenList = new List<Node>();
            this.ClosedList = new List<Node>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Search for the Path
        /// </summary>
        /// <returns></returns>
        public SearchResult Search(SearchOptions searchOptions = null)
        {
            if (searchOptions != null)
                this.SearchOptions = searchOptions;

            this.Found = false;
            this.OpenList.Clear();
            this.ClosedList.Clear();

            if (this.Graph.Start == null || this.Graph.End == null)
                return new SearchResult();

            if (this.SearchStarted != null)
                this.SearchStarted(this, new NodeEventArgs());

            this.OpenList.Add(this.Graph.Start);

            while (this.Found == false && this.OpenList.Count > 0)
            {
                Node node = null;

                foreach (Node openNode in this.OpenList)
                {
                    if (node == null || openNode.Function < node.Function)
                    {
                        node = openNode;
                    }
                }

                this.OpenList.Remove(node);
                this.ClosedList.Add(node);

                // check each adjacent
                this.CheckAdjacents(node);
            }

            if (this.SearchCompleted != null)
                this.SearchCompleted(this, new NodeEventArgs());

            Stack<Node> path = this.GetPath();

            return new SearchResult(this.Found, path);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check Adjacents of the the given Node
        /// </summary>
        /// <param name="node"></param>
        private void CheckAdjacents(Node node)
        {
            if (this.CheckingNode != null)
                this.CheckingNode(this, new NodeEventArgs() { Node = node });

            List<Edge> edges = this.Graph.GetNeighbors(node, 
                this.SearchOptions.AllowDiagonal, this.SearchOptions.AllowCrossCorners);

            foreach (Edge edge in edges)
            {
                this.CheckAdjacent(node, edge);
            }
        }

        /// <summary>
        /// Check the Adjacent of the Node using the Edge
        /// </summary>
        /// <param name="node"></param>
        /// <param name="edge"></param>
        private void CheckAdjacent(Node node, Edge edge)
        {
            if (edge == null || edge.Destination == null)
            {
                return;
            }

            if (this.Found)
            {
                return;
            }

            Node adjacent = edge.Destination;

            if (this.ClosedList.Contains(adjacent))
            {
                return;
            }

            if (adjacent.Type == NodeType.Wall || 
                (edge.Distance > 10 && 
                (adjacent.X < this.Graph.Width-1 && adjacent.Y < this.Graph.Height-1 && this.Graph.Map[adjacent.X+1, adjacent.Y+1].Type == NodeType.Wall ||
                 adjacent.X > 0 && adjacent.Y > 0 && this.Graph.Map[adjacent.X-1, adjacent.Y-1].Type == NodeType.Wall ||
                 adjacent.X < this.Graph.Width-1 && this.Graph.Map[adjacent.X+1, adjacent.Y].Type == NodeType.Wall ||
                 adjacent.Y < this.Graph.Height-1 && this.Graph.Map[adjacent.X, adjacent.Y+1].Type == NodeType.Wall
                ))
               )
            {
                return;
            }

            if (this.CheckingNodeAdjacent != null)
                this.CheckingNodeAdjacent(this, new AdjacentNodeEventArgs() { Parent = node, Adjacent = adjacent });

            if (this.OpenList.Contains(adjacent))
            {
                if ((node.Cost + edge.Distance) < adjacent.Cost)
                {
                    adjacent.Parent = node;
                    adjacent.Cost = node.Cost + edge.Distance;

                    int absX = Math.Abs(adjacent.X - this.Graph.End.X);
                    int absY = Math.Abs(adjacent.Y - this.Graph.End.Y);
                    if (heuristicType == HeuristicType.None)
                        adjacent.Heuristic = 0;
                    else if (heuristicType == HeuristicType.Manhattan)
                        adjacent.Heuristic = 10 * Heuristics.Manhattan(absX, absY);
                    else if (heuristicType == HeuristicType.Euclidean)
                        adjacent.Heuristic = 10 * Heuristics.Euclidean(absX, absY);
                    else if (heuristicType == HeuristicType.Chebyshev)
                        adjacent.Heuristic = 10 * Heuristics.Chebyshev(absX, absY);

                    adjacent.Function = adjacent.Cost + adjacent.Heuristic;
                }
            }
            else
            {
                adjacent.Parent = node;
                adjacent.Cost = node.Cost + edge.Distance;

                int absX = Math.Abs(adjacent.X - this.Graph.End.X);
                int absY = Math.Abs(adjacent.Y - this.Graph.End.Y);
                if (heuristicType == HeuristicType.None)
                    adjacent.Heuristic = 0;
                else if (heuristicType == HeuristicType.Manhattan)
                    adjacent.Heuristic = 10 * Heuristics.Manhattan(absX, absY);
                else if (heuristicType == HeuristicType.Euclidean)
                    adjacent.Heuristic = 10 * Heuristics.Euclidean(absX, absY);
                else if (heuristicType == HeuristicType.Chebyshev)
                    adjacent.Heuristic = 10 * Heuristics.Chebyshev(absX, absY);

                adjacent.Function = adjacent.Cost + adjacent.Heuristic;

                this.OpenList.Add(adjacent);

                if (adjacent == this.Graph.End)
                {
                    this.Found = true;

                    if (this.FoundPath != null)
                        this.FoundPath(this, new GraphPathEventArgs() { Path = this.GetPath() });
                }
            }
        }

        private Stack<Node> GetPath()
        {
            Stack<Node> path = new Stack<Node>();

            if (this.Graph.End == null)
            {
                return path;
            }

            Node currentNode = this.Graph.End;
            while (currentNode != null && currentNode != this.Graph.Start)
            {
                path.Push(currentNode);
                currentNode = currentNode.Parent;
            }

            return path;
        }

        #endregion
    }
}
