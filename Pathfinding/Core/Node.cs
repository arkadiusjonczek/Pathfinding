namespace Pathfinding.Core
{
    /// <summary>
    /// Represents a Node
    /// </summary>
    public class Node
    {
        #region Properties

        /// <summary>
        /// Cost between this Node and the Start Node
        /// </summary>
        public float Cost { get; set; }

        /// <summary>
        /// Heuristic Cost between this Node and the End Node
        /// </summary>
        public float Heuristic { get; set; }

        /// <summary>
        /// f(v) = g(v) + h(v)
        /// Sum of Cost and Heuristic
        /// </summary>
        public float Function { get; set; }

        /// <summary>
        /// X Position of the Node
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y Position of the Node
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Type of the Node
        /// </summary>
        public NodeType Type { get; set; }

        /// <summary>
        /// Parent Node
        /// </summary>
        public Node Parent { get; set; }

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Node()
        {
            this.Cost = 0;
            this.Heuristic = 0;
            this.Function = 0;
            this.Type = NodeType.Empty;
            this.Parent = null;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ToString Method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Node: X={0}, Y={1}, Cost={2}", this.X, this.Y, this.Cost);
        }

        #endregion
    }
}
