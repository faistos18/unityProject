using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts
{
    class GoToBarBehavior : Behavior
    {
        static float distCoeff = 2;
        static float distCoeff2 = 1;

        public GoToBarBehavior(SpectatorAgentScript script): base(script)
        {
            //
        }
        protected override float computeWish()
        {
            float closestBarDist = script.perception.computeDistanceTo("Bar");
            if (closestBarDist != -1.0)
                return script.goingForBeer.getWish() + distCoeff / (closestBarDist + distCoeff2); // GetBeerBehavior depends on the distance to the closest bar (with a maximum if we are very close to it)
            else
                return 0; //There is no bars !
        }
        public override void onBehaviorStarted()
        {
            GameObject[] bars = GameObject.FindGameObjectsWithTag("Bar");
            if (bars.Length != 0)
            {
                GameObject closestBar = bars.OrderBy(bar => Vector3.Distance(script.transform.position, bar.transform.position)).First();

                script.GetComponent<NavMeshAgent>().destination = closestBar.transform.position;
            }
        }

        public override void onBehaviorStopped()
        {
            //
        }

        public override void updateBehavior()
        {
            //throw new NotImplementedException();
        }
        public override String getBehaviorName()
        {
            return "Part chercher une bière";
        }
    }




    class DrinkBeerBehavior:Behavior
    {
        public static float timeForABeer = 5f;
        public static float timeAfterABeer = 10f;

        float timeOfLastBeer;
        public DrinkBeerBehavior(SpectatorAgentScript script): base(script)
        {
            timeOfLastBeer = -timeForABeer -0.1f; //If timeOfLastBeer=0, the system thinks it has initiated the behavior at t=0, so the behavior continues until the beer is consumed. Not a good idea.
            // So, we say the last time you started drinking a beer was a loooong time ago, you don't have to drink it now.
        }
        protected override float computeWish()
        {
            float ellapsedTime = Time.fixedTime - timeOfLastBeer;
            if (ellapsedTime < timeForABeer) //We are drinking a beer right now, so we want to continue.
            {
                return 1000;
            }
            else if( ellapsedTime > timeForABeer && ellapsedTime < timeAfterABeer) //Just had a beer, don't want another one. Gives time to "escape" the bar.
            {
                return 0;
            }
            else  //It's been a long time since last beer so, if we are here, we will want to have one.
            {
                //If we are in a certain zone, we want to have a beer. Except if we already had one.
                float closestBarDist=script.perception.computeDistanceTo("Bar");
                if (closestBarDist == -1.0)
                    return 0; //No bar ==> No beer wish.
                if ( closestBarDist< 2f)
                    return 100;
                else
                    return 0;
            }
        }
        public override void onBehaviorStarted()
        {
            timeOfLastBeer = Time.fixedTime;
            //We consider it is an "immediate" transfert to the bladder.
            script.drinkBeer(200);
        }
        public override void updateBehavior()
        {
            //Here, we could add beer to a "stock".
        }
        public override void onBehaviorStopped()
        {
            //
        }

        public override String getBehaviorName()
        {
            return "Boit une bonne bière !";
        }
    }
}
