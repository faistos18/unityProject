using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts
{
    class GoToConcertBehavior : Behavior
    {

        public GoToConcertBehavior(SpectatorAgentScript script): base(script)
        {
            //
        }
        static float distCoeff = 4;
        static float distCoeff2 = 1;
        protected override float computeWish()
        {
            float closestConcertDist = script.perception.computeDistanceTo("Concert");
            if(closestConcertDist == -1.0f)
                return 0; //No concerts => No wish to go.
            else
                return script.goingToConcert.getWish() + distCoeff / (closestConcertDist + distCoeff2); // Wish depends on the distance to the closest concert (with a maximum if we are very close to it)
        }
        public override void onBehaviorStarted()
        {
            GameObject[] concerts = GameObject.FindGameObjectsWithTag("Concert");
            if (concerts.Length != 0)
            {
                GameObject closestConcert = concerts.OrderBy(concert => Vector3.Distance(script.transform.position, concert.transform.position)).First();
                script.GetComponent<NavMeshAgent>().destination = closestConcert.transform.position;
            }
        }

        public override void onBehaviorStopped()
        {
            //Nothing.
        }
        public override void updateBehavior()
        {
            //throw new NotImplementedException();
        }
        public override String getBehaviorName()
        {
            return "Part voir un concert";
        }
    }
}
