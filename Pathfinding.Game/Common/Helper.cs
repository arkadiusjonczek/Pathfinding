using System.Drawing;

namespace Pathfinding.Game.Common
{
    /// <summary>
    /// Helper Class
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Detect Collision of two Circles
        /// Source: http://www.spieleprogrammierer.de/wiki/2D-Kollisionserkennung#Kollision_zwischen_zwei_Kreisen
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="radius1"></param>
        /// <param name="position2"></param>
        /// <param name="radius2"></param>
        /// <returns></returns>
        public static bool CircleCollision(PointF position1, float radius1, PointF position2, float radius2)
        {
            float dx = position1.X - position2.X;
            float dy = position1.Y - position2.Y;

            // Langsame Version, benötigt Wurzel:
            // return (Math.Sqrt(dx * dx + dy * dy) <= Convert.ToDouble(radius1) + Convert.ToDouble(radius2));

            // Schnelle Version, vergleicht die Quadrate:
            float radiusSum = radius1 + radius2;
            return dx * dx + dy * dy <= radiusSum * radiusSum;
        }
    }
}
