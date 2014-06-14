using Pathfinding.Core;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.Game.Creeps
{
    /// <summary>
    /// Level Two Creep
    /// </summary>
    class LevelTwo : Creep
    {
        #region CTor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        public LevelTwo(Stack<Node> path) : base(path)
        {
            this.Life = 100;
            this.Width += 2;
            this.Height += 2;
            this.backgroundColor = Brushes.LimeGreen;
            this.movementMultiplier = .15f;
            this.protectionMultiplier = 1.5f;
        }

        #endregion
    }
}
