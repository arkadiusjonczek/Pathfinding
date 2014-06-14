using Pathfinding.Core;
using System.Collections.Generic;

namespace Pathfinding.Finders
{
    public class GraphPathEventArgs
    {
        public Stack<Node> Path { get; set; }
    }
}
