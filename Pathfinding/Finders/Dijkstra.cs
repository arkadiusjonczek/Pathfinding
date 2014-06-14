using Pathfinding.Core;

namespace Pathfinding.Finders
{
    /// <summary>
    /// Dijkstra Algorithm class
    /// Uses A* Algorithmn class because 
    /// A* is based on Dijkstra but uses no heuristic
    /// </summary>
    public class Dijkstra : Astar
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="graph"></param>
        public Dijkstra(Graph graph) : base(graph, HeuristicType.None) { }
    }
}
