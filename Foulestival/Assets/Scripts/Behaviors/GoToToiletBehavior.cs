using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Assets.Scripts
{
    class GoToToiletBehavior : Behavior
    {
        static float coeff = 1;

        public GoToToiletBehavior(SpectatorAgentScript script)
            : base(script)
        {
            //
        }

        protected override float computeWish()
        {
            float baseVal = 0;
            if ((script.bladder / script.maxBladder) < SpectatorAgentScript.breakingPointBlatterCoeff)
                baseVal = 0;
            else if (script.bladder < script.maxBladder)
                baseVal = coeff / (script.maxBladder - script.bladder);
            else
                baseVal = 100;

            return baseVal;
        }

        public override void onBehaviorStarted()
        {
            GameObject[] toilets = GameObject.FindGameObjectsWithTag("Toilet");
            if (toilets.Length != 0)
            {
                GameObject closestToilet = toilets.OrderBy(toilet => Vector3.Distance(script.transform.position, toilet.transform.position)).First();
                script.GetComponent<NavMeshAgent>().destination = closestToilet.transform.position;
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
            return "Part aux toilettes";
        }
    }
    class FlushToiletBehavior : Behavior
    {
        static float timeForFlush = 10f;//seconds
        private float startTime;
        public FlushToiletBehavior(SpectatorAgentScript script)
            : base(script)
        {
            startTime = -timeForFlush - 0.1f; // If start=0 when the game starts, then the system thinks it has initiated the behavior at startTime=0, so it triggers the behavior.
        }
        protected override float computeWish()
        {
            float timeEllapsedSinceStart = Time.fixedTime - startTime;
            if(timeEllapsedSinceStart < timeForFlush)
                return 1000;

            float bladderFilling = script.bladder / script.maxBladder;
            if (bladderFilling >= 0.1)
            {
                if (script.perception.computeDistanceTo("Toilet") < 2f)
                    return 1000;
                else
                    return 0;
            }
            else
                return 0;
        }
        public override void onBehaviorStarted()
        {
            startTime = Time.fixedTime;
            script.bladder = 0;
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
            return "Utilise les toilettes";
        }
    }

}
