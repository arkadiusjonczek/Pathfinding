using Pathfinding.Core;
using System.Collections.Generic;

namespace Pathfinding.Core
{
    /// <summary>
    /// Result of the Graph Search
    /// </summary>
    public class SearchResult
    {
        #region Properties

        /// <summary>
        /// Search successful?
        /// </summary>
        public bool FoundPath { get; private set; }
        
        /// <summary>
        /// Path between Start and End Node
        /// </summary>
        public Stack<Node> Path { get; private set; }

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// (if search was not successful)
        /// </summary>
        /// <param name="found"></param>
        public SearchResult() : this(false, null) { }

        /// <summary>
        /// Constructor
        /// (if search was successful)
        /// </summary>
        /// <param name="found"></param>
        /// <param name="path"></param>
        public SearchResult(bool found, Stack<Node> path)
        {
            this.FoundPath = found;
            this.Path = path;
        }

        #endregion
    }
}
