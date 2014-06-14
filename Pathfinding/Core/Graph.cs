using System;
using System.Linq;
using System.Collections.Generic;

namespace Pathfinding.Core
{
    /// <summary>
    /// Represents a 2D Graph
    /// </summary>
    public class Graph
    {
        #region Const

        /// <summary>
        /// Cost for horizontal and vertical movement
        /// </summary>
        public const int MovementCost = 10;

        /// <summary>
        /// Cost for diagonal movement
        /// </summary>
        public const int DiagonalMovementCost = 14;

        #endregion

        #region Properties

        /// <summary>
        /// Width of the Graph
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Height of the Graph
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// List of Edges between the Nodes
        /// </summary>
        public List<Edge> Edges { get; set; }

        /// <summary>
        /// The Nodes on the Graph
        /// </summary>
        public Node[,] Map { get; set; }

        /// <summary>
        /// Start Node
        /// </summary>
        public Node Start { get; set; }

        /// <summary>
        /// End Node
        /// </summary>
        public Node End { get; set; }

        /// <summary>
        /// Indexer for Map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Node this[int x, int y] 
        { 
            get { return this.Map[x, y]; }
        }

        #endregion

        #region Delegates

        /// <summary>
        /// Event Handler for Graph Events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void GraphEventHandler(object sender, EventArgs e);

        #endregion

        #region Events

        /// <summary>
        /// Fires when the Graph before the Reset 
        /// of the Graph startet
        /// </summary>
        public event GraphEventHandler GraphReset;

        /// <summary>
        /// Fires when the Graph was reset
        /// </summary>
        public event GraphEventHandler GraphResetComplete;

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Graph(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Edges = new List<Edge>();

            this.Initialize(width, height);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initializes the Graph
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void Initialize(int width, int height)
        {
            this.Map = new Node[width, height];
            this.Reset();
        }

        /// <summary>
        /// Resets the Graph
        /// </summary>
        public void Reset()
        {
            if (this.GraphReset != null)
                this.GraphReset(this, new EventArgs());

            this.Start = null;
            this.End = null;

            this.Edges.Clear();

            // Add nodes to map
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    Node node = new Node() { X = x, Y = y };

                    this.Map[x, y] = node;
                }
            }

            // Add edges of all nodes
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    Node node = this.Map[x, y];

                    // Top
                    if (y > 0)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x, y - 1], 10)
                        );
                    }

                    // Top right
                    if (x < this.Width - 1 && y > 0)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x + 1, y - 1], 14)
                        );
                    }

                    // Right
                    if (x < this.Width - 1)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x + 1, y], 10)
                        );
                    }

                    // Bottom right
                    if (x < this.Width - 1 && y < this.Height - 1)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x + 1, y + 1], 14)
                        );
                    }

                    // Bottom
                    if (y < this.Height - 1)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x, y + 1], 10)
                        );
                    }

                    // Bottom left
                    if (x > 0 && y < this.Height - 1)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x - 1, y + 1], 14)
                        );
                    }

                    // Left
                    if (x > 0)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x - 1, y], 10)
                        );
                    }

                    // Top left
                    if (x > 0 && y > 0)
                    {
                        this.Edges.Add(
                            new Edge(node, this.Map[x - 1, y - 1], 14)
                        );
                    }
                }
            }

            if (this.GraphResetComplete != null)
                this.GraphResetComplete(this, new EventArgs());
        }

        /// <summary>
        /// Get the neighbors of the given Node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public List<Edge> GetNeighbors(Node node, bool allowDiagonal = true, bool allowCrossCorners = true)
        {
            List<Edge> neighborEdges = null;
            
            if (allowDiagonal)
                neighborEdges = this.Edges.Where(e => e.Source == node && e.Destination.Type != NodeType.Wall).ToList();
            else
                neighborEdges = this.Edges.Where(e => e.Source == node && e.Destination.Type != NodeType.Wall && e.Distance == Graph.MovementCost).ToList();
            
            if (!allowCrossCorners)
            {
                List<Edge> edgesToRemove = new List<Edge>();

                foreach (Edge edge in neighborEdges)
                {
                    if (edge.IsTopRight && !neighborEdges.Any(e => e.IsTop && e.IsRight) ||
                        edge.IsBottomRight && !neighborEdges.Any(e => e.IsBottom && e.IsRight) ||
                        edge.IsBottomLeft && !neighborEdges.Any(e => e.IsBottom && e.IsLeft) ||
                        edge.IsTopLeft && !neighborEdges.Any(e => e.IsTop && e.IsLeft))
                        edgesToRemove.Add(edge);
                }

                foreach (Edge edge in edgesToRemove)
                {
                    neighborEdges.Remove(edge);
                }
            }

            return neighborEdges;
        }

        /// <summary>
        /// Check if the Point is inside the 2D Grid
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsInside(int x, int y)
        {
            return (x >= 0 && x < this.Width && y >= 0 && y < this.Height);
        }

        /// <summary>
        /// Check if the Point is walkable
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsWalkable(int x, int y)
        {
            return this.IsInside(x, y) && this[x, y].Type != NodeType.Wall;
        }

        /// <summary>
        /// ToString Method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Graph: {0}x{1}", this.Width, this.Height);
        }

        #endregion
    }
}
