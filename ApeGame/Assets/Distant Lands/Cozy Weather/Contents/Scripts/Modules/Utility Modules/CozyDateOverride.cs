using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DistantLands.Cozy
{
    public abstract class CozyDateOverride : CozyModule
    {

        public float yearPercentage;

        /// <summary>
        /// Returns the current year percentage (0 - 1).
        /// </summary> 
        public abstract float GetCurrentYearPercentage();

        /// <summary>
        /// Returns the current year percentage (0 - 1) after a number of ticks has passed.
        /// </summary> 
        public abstract float GetCurrentYearPercentage(float inTicks);

        /// <summary>
        /// Gets the current day plus the current day percentage (0-1). 
        /// </summary> 
        public abstract float DayAndTime();
        public abstract void ChangeDay(int days);

    }
}