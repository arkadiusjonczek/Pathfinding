using Pathfinding.Core;

namespace Pathfinding.Finders
{
    #region Delegates

    // TODO: better names for Event Handlers
    public delegate void AlgorithmEventHandler(object sender, NodeEventArgs e);
    public delegate void AdjacentEventHandler(object sender, AdjacentNodeEventArgs e);
    public delegate void PathEventHandler(object sender, GraphPathEventArgs e);

    #endregion

    /// <summary>
    /// Interface for the Search Algorithm classes
    /// </summary>
    public interface ISearchAlgorithm
    {
        /// <summary>
        /// Path was found
        /// </summary>
        event PathEventHandler FoundPath;

        /// <summary>
        /// Search is complete
        /// </summary>
        event AlgorithmEventHandler SearchCompleted;

        /// <summary>
        /// Search was started
        /// </summary>
        event AlgorithmEventHandler SearchStarted;

        /// <summary>
        /// Checking one Node for Adjacents
        /// </summary>
        event AlgorithmEventHandler CheckingNode;

        /// <summary>
        /// Checking one Adjacent of a Node
        /// </summary>
        event AdjacentEventHandler CheckingNodeAdjacent;

        /// <summary>
        /// Search for the Path using Start and End Node
        /// </summary>
        /// <returns></returns>
        SearchResult Search(SearchOptions searchOptions = null);
    }
}
