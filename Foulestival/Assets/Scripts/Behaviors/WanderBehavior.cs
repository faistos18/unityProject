using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System;

namespace Assets.Scripts
{
    class WanderBehavior : Behavior
    {
        static float nextDestinationMaxDistance = 30f;
        static float destinationChangeTime = 3f;
        static float wanderWish = 3.0f;

        Vector3 currentDestination;
        
        float lastChangeTime;

        public WanderBehavior(SpectatorAgentScript script)
            : base(script)
        {
            //
        }
        protected override float computeWish()
        {
            return wanderWish;
        }
        public override void onBehaviorStarted()
        {
            //updateBehavior();
        }
        public override void updateBehavior() //Every "destinationChangeTime", the destination change to a new one, located nextDestinationMaxDistance away maximum.
        {
            float ellapsedTimeSinceChange = Time.fixedTime - lastChangeTime;
            if(Vector3.Distance(currentDestination,script.transform.position) <1.0f || ellapsedTimeSinceChange > destinationChangeTime)
            {
                wander();
                lastChangeTime = Time.fixedTime;
            }
        }
        private void wander()
        {
            // does nothing except pick a new destination go to
            currentDestination = UnityEngine.Random.insideUnitSphere * nextDestinationMaxDistance;
            currentDestination.y = 0;
            currentDestination += script.transform.position;
            // don't need to change direction every frame seeing as you walk in a straight line only
            script.transform.LookAt(currentDestination);

            script.GetComponent<NavMeshAgent>().SetDestination(currentDestination);
        }
        public override String getBehaviorName()
        {
            return "Se promène";
        }
        public override void onBehaviorStopped()
        {
            //
        }
    }
}
