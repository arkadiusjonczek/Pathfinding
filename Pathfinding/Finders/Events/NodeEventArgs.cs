using Pathfinding.Core;
using System;

namespace Pathfinding.Finders
{
    public class NodeEventArgs : EventArgs
    {
        public Node Node { get; set; }
    }
}
