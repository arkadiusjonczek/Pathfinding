using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pathfinding.Core
{
    public class SearchOptions
    {
        public bool AllowDiagonal { get; set; }
        public bool AllowCrossCorners { get; set; }

        public SearchOptions() : this(true, true) {}

        public SearchOptions(bool allowDiagonal, bool allowCrossCorners)
        {
            this.AllowDiagonal = allowDiagonal;
            this.AllowCrossCorners = allowCrossCorners;
        }
    }
}
