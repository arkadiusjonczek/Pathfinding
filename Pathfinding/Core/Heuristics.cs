using System;

namespace Pathfinding.Core
{
    /// <summary>
    /// Contains diffrent Heuristics
    /// </summary>
    public static class Heuristics
    {
        /// <summary>
        /// Manhattan Heuristic
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static float Manhattan(int dx, int dy)
        {
            return dx + dy;
        }

        /// <summary>
        /// Chebyshev Heuristic
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static float Chebyshev(int dx, int dy)
        {
            return (float)Math.Max(dx, dy);
        }

        /// <summary>
        /// Euclidean Heuristic
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <returns></returns>
        public static float Euclidean(int dx, int dy)
        {
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
