using UnityEngine;

namespace Chinchillada.BehaviourSelections.Utilities
{
    public static class MathHelper
    {
        public static float Square(this float value)
        {
            return Mathf.Pow(value, 2);
        }

        /// <summary>
        /// Calculates the 2D perpendicular in clockwise direction of <paramref name="vector"/>.
        /// </summary> 
        public static Vector2 PerpendicularCW(this Vector2 vector)
        {
            float x2 = vector.x.Square();
            float y2 = vector.y.Square();

            float pythagoras = Mathf.Sqrt(x2 + y2);

            return new Vector2(-vector.y, vector.x) / pythagoras;
        }


        /// <summary>
        /// Calculates the 2D perpendicular in counter-clockwise direction of <paramref name="vector"/>.
        /// </summary> 
        public static Vector2 PerpendicularCCW(this Vector2 vector)
        {
            return -PerpendicularCW(vector);
        }
    }
}
