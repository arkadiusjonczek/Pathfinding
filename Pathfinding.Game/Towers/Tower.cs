using Pathfinding.Game.Creeps;
using System;
using System.Drawing;

namespace Pathfinding.Game.Towers
{
    /// <summary>
    /// Abstract Tower Class
    /// </summary>
    public abstract class Tower
    {
        #region Members

        /// <summary>
        /// Width of the Tower
        /// </summary>
        protected int width;

        /// <summary>
        /// Height of the Tower
        /// </summary>
        protected int height;

        /// <summary>
        /// Multiplier of the Attack frequency
        /// </summary>
        protected float attackFrequencyMultiplier;

        #endregion

        #region Properties

        /// <summary>
        /// X Position of the Tower
        /// </summary>
        public int X { get; protected set; }

        /// <summary>
        /// Y Position of the Tower
        /// </summary>
        public int Y { get; protected set; }

        /// <summary>
        /// X Position on the Grid
        /// </summary>
        public int GridX { get; protected set; }

        /// <summary>
        /// Y Position on the Grid
        /// </summary>
        public int GridY { get; protected set; }

        /// <summary>
        /// Radius
        /// </summary>
        public float Radius { get; protected set; }

        /// <summary>
        /// Damage
        /// </summary>
        public float Damage { get; protected set; }

        /// <summary>
        /// If the Tower is selected
        /// </summary>
        public bool IsSelected { get; set; }

        /// <summary>
        /// Next Tick to fire
        /// </summary>
        public int NextTick { get; set; }

        #endregion

        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        public Tower(int gridX, int gridY)
        {
            this.GridX = gridX;
            this.GridY = gridY;
            this.X = Properties.Settings.Default.GridNodeSize * gridX + gridX;
            this.Y = Properties.Settings.Default.GridNodeSize * gridY + gridY;
            this.IsSelected = false;
            this.NextTick = 0;
            this.attackFrequencyMultiplier = 1f;
        }

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Abstract Draw Method to override
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void Draw(Graphics graphics);

        #endregion

        #region Public Methods

        /// <summary>
        /// If the Next Tick was reached
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public bool NextTickReached(int tick)
        {
            return this.NextTick == 0 || this.NextTick <= tick;
        }

        /// <summary>
        /// Attack a Creep
        /// </summary>
        /// <param name="creep"></param>
        public void Attack(Creep creep)
        {
            creep.RemoveLife(this.Damage);

            this.NextTick = Environment.TickCount + (int)(Properties.Settings.Default.AttackSpeed * this.attackFrequencyMultiplier);
        }

        #endregion
    }
}
