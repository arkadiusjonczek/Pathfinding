using System.Drawing;

namespace Pathfinding.Game.Towers
{
    /// <summary>
    /// Laser Tower Class
    /// </summary>
    class LaserTower : Tower
    {
        #region CTor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public LaserTower(int x, int y) : base(x, y)
        {
            this.Damage = 1;
            this.Radius = 45;
            this.width = 19;
            this.height = 19;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Draw the Tower
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(Pens.Red, this.X, this.Y, this.width, this.height);

            float innerWidth = this.width / 2 + 1;
            float innerHeight = this.height / 2 + 1;
            float innerX = this.X + this.width / 4 + 1;
            float innerY = this.Y + this.height / 4 + 1;
            graphics.FillRectangle(Brushes.Red, innerX, innerY, innerWidth, innerHeight);

            if (this.IsSelected)
            {
                SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Red));
                float x1 = this.X - this.Radius + this.width / 2;
                float y1 = this.Y - this.Radius + this.height / 2;
                graphics.FillEllipse(brush, x1, y1, this.Radius * 2, this.Radius * 2);
                graphics.DrawEllipse(Pens.White, x1 - 1, y1 - 1, this.Radius * 2 + 2, this.Radius * 2 + 2);
            }
        }

        #endregion
    }
}
