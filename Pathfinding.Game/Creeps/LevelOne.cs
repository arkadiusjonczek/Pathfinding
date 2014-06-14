using Pathfinding.Core;
using System.Collections.Generic;

namespace Pathfinding.Game.Creeps
{
    /// <summary>
    /// Level One Creep
    /// </summary>
    class LevelOne : Creep
    {
        #region CTor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        public LevelOne(Stack<Node> path) : base(path)
        {
            this.movementMultiplier = .1f;
        }

        #endregion
    }
}
