using System.Collections.Generic;
using System.Text;

using UnityEngine; //For DeltaTime;


namespace Assets.Scripts
{
    public class VariableWish
    {
        private float wish;
        private float maxWish;
        private float stepSize;

        public VariableWish(float maxWishValue = 10, float wishValue = 5, float stepSizeValue = 1)
        {   
            stepSize = System.Math.Abs(stepSizeValue); //stepSize could be 0 for a wish that doesn't evolve. That's not the goal but...why not ?

            if (maxWishValue > 0)
                maxWish = maxWishValue;
            else
                maxWish = 10; // Default value;

            if (wishValue < maxWish && wishValue > 0)
                wish = wishValue;
            else
                wish = maxWish / 2;

        }
        public float getWish()
        {
            return wish;
        }
        public float getMaxWish()
        {
            return maxWish;
        }
        public void update()
        {
            float step = Time.deltaTime * Random.Range(-1f,1f)*stepSize;

            if (wish + step <= 0 || wish + step >= maxWish)
                wish -= step;
            else
                wish += step;
        }
    }
}
