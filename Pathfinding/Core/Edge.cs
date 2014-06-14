namespace Pathfinding.Core
{
    /// <summary>
    /// Edge between two Nodes
    /// </summary>
    public class Edge
    {
        #region Properties

        /// <summary>
        /// Source Node
        /// </summary>
        public Node Source { get; private set; }

        /// <summary>
        /// Destination Node
        /// </summary>
        public Node Destination  { get; private set; }

        /// <summary>
        /// Distance Cost between the Nodes
        /// </summary>
        public int Distance { get; private set; }

        public bool IsTop
        {
            get { return Source.X == Destination.X && Source.Y - 1 == Destination.Y; }
        }

        public bool IsRight
        {
            get { return Source.X + 1 == Destination.X && Source.Y == Destination.Y; }
        }

        public bool IsBottom
        {
            get { return Source.X == Destination.X && Source.Y + 1 == Destination.Y; }
        }

        public bool IsLeft
        {
            get { return Source.X - 1 == Destination.X && Source.Y == Destination.Y; }
        }

        public bool IsTopRight
        {
            get { return Source.X + 1 == Destination.X && Source.Y - 1 == Destination.Y; }
        }

        public bool IsBottomRight
        {
            get { return Source.X + 1 == Destination.X && Source.Y + 1 == Destination.Y; }
        }

        public bool IsBottomLeft
        {
            get { return Source.X - 1 == Destination.X && Source.Y - 1 == Destination.Y; }
        }

        public bool IsTopLeft
        {
            get { return Source.X - 1 == Destination.X && Source.Y + 1 == Destination.Y; }
        }


        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="Distance"></param>
        public Edge(Node From, Node To, int Distance)
        {
            this.Source = From;
            this.Destination = To;
            this.Distance = Distance;
        }

        #endregion
    }
}
