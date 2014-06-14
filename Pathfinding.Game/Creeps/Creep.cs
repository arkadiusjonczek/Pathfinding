using Pathfinding.Core;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Pathfinding.Game.Creeps
{
    /// <summary>
    /// Abstract Creep Class
    /// </summary>
    public abstract class Creep
    {
        #region Members

        /// <summary>
        /// Multiplier for the Movement Speed
        /// </summary>
        protected float movementMultiplier;

        /// <summary>
        /// Multiplier for the Protection
        /// </summary>
        protected float protectionMultiplier;

        /// <summary>
        /// Path of the Creep
        /// </summary>
        protected Stack<Node> path;

        /// <summary>
        /// Current Node of the Creep
        /// </summary>
        protected Node currentNode;

        /// <summary>
        /// Background Color of the Creep
        /// </summary>
        protected Brush backgroundColor;

        #endregion
        
        #region Properties

        /// <summary>
        /// X Position
        /// </summary>
        public int X { get; protected set; }

        /// <summary>
        /// Y Position
        /// </summary>
        public int Y { get; protected set; }

        /// <summary>
        /// X Position to go
        /// </summary>
        public int ToX { get; protected set; }

        /// <summary>
        /// Y Position to go
        /// </summary>
        public int ToY { get; protected set; }

        /// <summary>
        /// Width of the Creep
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Height of the Creep
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Life of the Creep
        /// </summary>
        public float Life { get; protected set; }

        /// <summary>
        /// Next Tick to move
        /// </summary>
        public int NextTick { get; set; }

        /// <summary>
        /// Finish was reached
        /// </summary>
        public bool FinishReached { get; protected set; }

        #endregion

        #region CTor

        /// <summary>
        /// Default Init Constructor
        /// </summary>
        private Creep()
        {
            this.Width = 10;
            this.Height = 10;
            this.Life = 100;
            this.movementMultiplier = 1f;
            this.protectionMultiplier = 1f;
            this.path = new Stack<Node>();
            this.NextTick = 0;
            this.backgroundColor = Brushes.Yellow;
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="path"></param>
        public Creep(Stack<Node> path) : this()
        {
            this.path = new Stack<Node>(new Stack<Node>(path));
            this.NextNode();
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Calculate GridValue to absolute graph value
        /// </summary>
        /// <param name="gridValue"></param>
        /// <returns></returns>
        private static int GetAbsValue(int gridValue)
        {
            return gridValue + gridValue * Properties.Settings.Default.GridNodeSize;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate Next Node
        /// </summary>
        public void NextNode()
        {
            if (this.path.Count == 0)
                return;

            this.currentNode = this.path.Pop();
            if (this.currentNode.Parent == null)
            {
                this.currentNode = this.path.Pop();
            }

            this.X = GetAbsValue(this.currentNode.Parent.X) + Properties.Settings.Default.GridNodeSize / 2 - this.Width / 2;
            this.Y = GetAbsValue(this.currentNode.Parent.Y) + Properties.Settings.Default.GridNodeSize / 2 - this.Height / 2;
            this.ToX = GetAbsValue(this.currentNode.X) + Properties.Settings.Default.GridNodeSize / 2 - this.Width / 2;
            this.ToY = GetAbsValue(this.currentNode.Y) + Properties.Settings.Default.GridNodeSize / 2 - this.Height / 2;
        }

        /// <summary>
        /// If Tick was reached
        /// </summary>
        /// <param name="tick"></param>
        /// <returns></returns>
        public bool NextTickReached(int tick)
        {
            return this.NextTick == 0 || this.NextTick <= tick;
        }

        /// <summary>
        /// Move Creep
        /// </summary>
        public void Move()
        {
            if (this.ToX == this.X && this.ToY == this.Y)
            {
                if (this.path.Count > 0)
                {
                    this.NextNode();
                }
                else
                {
                    this.FinishReached = true;
                    return;
                }
            }

            if (this.ToX > this.X)
                this.X++;
            else if (this.ToX < this.X)
                this.X--;
            if (this.ToY > this.Y)
                this.Y++;
            else if (this.ToY < this.Y)
                this.Y--;

            this.NextTick = Environment.TickCount + (int)(Properties.Settings.Default.CreepMovementSpeed * this.movementMultiplier);
        }

        /// <summary>
        /// Remove Life of the Creep
        /// </summary>
        /// <param name="damage"></param>
        public void RemoveLife(float damage)
        {
            this.Life -= damage / this.protectionMultiplier;

            if (this.Life < 0)
            {
                Life = 0;
            }
        }

        /// <summary>
        /// Draw Life of the Creep
        /// </summary>
        /// <param name="graphics"></param>
        public void DrawLife(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Green, this.X, this.Y - 5, (this.Life / 100) * this.Width, 3);
        }

        /// <summary>
        /// Draw the Creep
        /// </summary>
        /// <param name="graphics"></param>
        public void Draw(Graphics graphics)
        {
            graphics.FillEllipse(this.backgroundColor, this.X, this.Y, this.Width, this.Height);
            graphics.DrawEllipse(Pens.White, this.X, this.Y, this.Width, this.Height);
        }

        /// <summary>
        /// Update Path of Creep
        /// </summary>
        /// <param name="stack"></param>
        public void UpdatePath(Stack<Node> newPath)
        {
            Stack<Node> path = new Stack<Node>(new Stack<Node>(newPath));

            if (path.Contains(this.currentNode))
            {
                while (path.Pop() != this.currentNode) ;
                this.path = path;
            }
        }

        #endregion
    }
}
