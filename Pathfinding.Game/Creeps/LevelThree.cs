using Pathfinding.Core;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.Game.Creeps
{
    /// <summary>
    /// Level Three Creep
    /// </summary>
    class LevelThree : Creep
    {
        #region CTor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="path"></param>
        public LevelThree(Stack<Node> path)
            : base(path)
        {
            this.Life = 100;
            this.Width += 2;
            this.Height += 2;
            this.backgroundColor = Brushes.BlueViolet;
            this.movementMultiplier = .1f;
            this.protectionMultiplier = 2f;
        }

        #endregion
    }
}
