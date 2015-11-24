using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Scripts
{
    /*
     * Base class for behaviors. 
     * The behavior executed is the one that has the higher wish, given that the actual behavior have a bonus in order to increase indecision.
     * */
    abstract class Behavior
    {
        public static float activeBonus = 0.2f;

        private float wish; // "Wish" is the motivation associated with having this behavior.
        protected readonly SpectatorAgentScript script;  // In order to access to properties and methods from SpectatorAgentScript.
        public bool isActive;
        
        public Behavior(SpectatorAgentScript scriptValue)
        {
            script = scriptValue;
        }

        public void updateWish()
        {
            //The wish could be based on Blood Alcoohol Level, Closeness to something...
            wish = computeWish();
        }

        public float getWish()
        {
            return isActive ?  wish + activeBonus :  wish;
        }
        abstract protected float computeWish();
        abstract public String getBehaviorName();

        abstract public void onBehaviorStarted();
        abstract public void onBehaviorStopped();

        abstract public void updateBehavior();
    }
}
