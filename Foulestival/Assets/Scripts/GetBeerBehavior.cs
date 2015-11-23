using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts
{
    class GetBeerBehavior : Behavior
    {
        static float distCoeff = 4;
        static float distCoeff2 = 1;

        public GetBeerBehavior(SpectatorAgentScript script): base(script)
        {
            //
        }
        protected override float computeWish(SpectatorAgentScript script)
        {
            return script.goingForBeer.getWish() + distCoeff / (computeDistanceToBar() + distCoeff2); // GetBeerBehavior depends on the distance to the closest bar (with a maximum if we are very close to it)
        }
        private float computeDistanceToBar() //Computes distance to the closest bar.
        {
            GameObject[] bars = GameObject.FindGameObjectsWithTag("Bar");
            if (bars.Length == 0) // No bars, what a pity.
                return 0f;
            else
                return bars.Min(bar => Vector3.Distance(script.transform.position, bar.transform.position));
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
}
