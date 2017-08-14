using System;

namespace Visyn.Mathematics.Trigonometry
{
    public class Angle
    {
        /// <summary>
        /// Convert degrees to radians.  radians = degrees*PI/180
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static double ToRadians(double degrees) => (degrees*(Math.PI/180.0));
        /// <summary>
        /// Convert radians to degrees.  degrees = radians*180/PI
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static double ToDegrees(double radians) => (radians*(180.0/Math.PI));
    }
}
