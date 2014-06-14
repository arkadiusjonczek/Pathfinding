using Pathfinding.Core;

namespace Pathfinding.Finders
{
    public class AdjacentNodeEventArgs
    {
        public Node Parent { get; set; }
        public Node Adjacent { get; set; }
    }
}
