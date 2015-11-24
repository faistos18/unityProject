using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Perception
    {
        SpectatorAgentScript script; // Usefull for accessing alcohol blood level, that distorts perceptions.
        
        public Perception(SpectatorAgentScript scriptValue)
        {
            script = scriptValue;
        }
        public float computeDistanceTo(String tag)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);
            if (objs.Length == 0) //No objects with this tag.
                return -1f; //This can't be a distance, so it indicates an error.
            else
                return objs.Min(obj => Vector3.Distance(script.transform.position, obj.transform.position));
        }
    }
}
