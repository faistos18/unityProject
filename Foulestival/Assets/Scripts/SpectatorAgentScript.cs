using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine.UI;


namespace Assets.Scripts
{
    public class SpectatorAgentScript : MonoBehaviour
    {
        static float breakingPointBlatterCoeff = 0.6f; // When does going to the toilet becomes urgent ?

        // Fixed during Spectator's lifetime
        float age;  // float because this will be given by a Gaussian Function.
        float weight;
        bool isMale;
        float alcoholResistanceCoeff; // Every "Coeff" is between 0 and 1.
        float maxBladder; // Max capacity of the bladder. In Millilitters ( approximately from 300 to 600 for an adult)

        float badAlcoholBehaviorCoeff; // Does alcohol make this agent agressive ?

        // Moves during Spectator's lifetime.

        float bladder; // Evolves between 0 and maxBladder, hopefully for the spectator.
        float bloodAlcoolemyLevel; // Evolves between 0 and ... No limit for now.
        float agressiveness; // Evolves with bloodAlcoolemyLevel thanks to badAlcholBehaviorCoeff.

        public VariableWish goingToConcert; // How much does this spectator wants to go to a concert ?
        public VariableWish goingForBeer;   // How much does this spectator wants a beer ? ( This is fondamental wish. Real behavior depends on other factors, like proximity to a bar)


        //Stores behaviors.
        private List<Behavior> behaviors;
        Behavior activeBehavior;

        //Tooltip
        private bool isMouseOn;
        Transform spectatorTooltip;

        //public Transform spectatorTooltip;

        public SpectatorAgentScript()
        {
            behaviors = new List<Behavior>();
            goingForBeer = new VariableWish();
            goingToConcert = new VariableWish();
        }
        void Start()
        {
            //Constructs values.
            weight = generateGaussianLimited(77.4f, 10f, 40f, 150f);
            age = generateGaussianLimited(25, 10f, 10f, 70f);
            isMale = Random.Range(0f, 1f) > 0.3; // 70% of Hellfest people are male. (adapt this with the study)

            alcoholResistanceCoeff = generateGaussianLimited(0.6f, 0.2f, 0f, 1f);
            maxBladder = generateGaussianLimited(450f, 150f, 150, 900);
            badAlcoholBehaviorCoeff = alcoholResistanceCoeff = generateGaussianLimited(0.6f, 0.2f, 0f, 1f);


            //Tooltip
            isMouseOn = false;
            spectatorTooltip = GameObject.FindWithTag("Tooltip").transform;
            //Behaviors
            behaviors.Add(new GoToConcertBehavior(this));
            behaviors.Add(new GetBeerBehavior(this));
        }

        // Update is called once per frame
        void Update()
        {
            //Tooltip
            if (isMouseOn)
            {
                updateToolTip();
                //Puts the tooltip is the right place.
                RectTransform rectTransform = (RectTransform)spectatorTooltip.GetComponent("RectTransform");
                rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x - Screen.width / 2.0f, Input.mousePosition.y - Screen.height / 2.0f);
            }

            //Behavior
            goingForBeer.update();
            goingToConcert.update();

            foreach(var behavior in behaviors)
                behavior.updateWish();

            //Change behavior if needed.
            Behavior mostWished = behaviors.OrderBy(behavior => behavior.getWish() ).First();

            if(mostWished == activeBehavior)
                activeBehavior.updateBehavior();
            else // The active behavior changes.
            {
                if (activeBehavior != null)
                {
                    activeBehavior.onBehaviorStopped();
                    activeBehavior.isActive = false;
                }

                mostWished.isActive = true;
                mostWished.onBehaviorStarted();
                activeBehavior = mostWished;
            }
        }

        /*
        void receivedMessage(string message)
        {
            //Sends the message to each Behavior ? To the behavior whose name is said in the beginning of the message ? To active only ?
        }
         * //*/

        static float generateGaussian(float mean, float stdDev)
        {
            float u1 = Random.Range(0f, 1f); //these are uniform(0,1) random floats
            float u2 = Random.Range(0f, 1f);
            float randStdNormal = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Sin(2.0f * Mathf.PI * u2); //random normal(0,1)
            float randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
        static float generateGaussianLimited(float mean, float stdDev, float min, float max)
        {
            float gaussianRes = generateGaussian(mean, stdDev);
            if (gaussianRes < min)
                return min;
            if (gaussianRes > max)
                return max;
            return gaussianRes;
        }

        //Tooltip functions

        void OnMouseEnter()
        {
            //Debug.Log("Mouse Enter Tooltip");

            //Puts the tooltip is the right place.
            RectTransform rectTransform = (RectTransform)spectatorTooltip.GetComponent("RectTransform");
            rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x - Screen.width / 2.0f, Input.mousePosition.y - Screen.height / 2.0f);
            CanvasGroup alphaCanvas = (CanvasGroup)spectatorTooltip.GetComponent("CanvasGroup");
            alphaCanvas.alpha = 0.7f;
            isMouseOn = true;

            updateToolTip();
        }

        void OnMouseExit()
        {
            //Debug.Log("Mouse Exit Tooltip");

            CanvasGroup alphaCanvas = (CanvasGroup)spectatorTooltip.GetComponent("CanvasGroup");

            alphaCanvas.alpha = 0;
            isMouseOn = false;
        }
        void updateToolTip()
        {
            //Changes the text to correspond with the properties.
            //Order : Alcohol, bladder, agressiveness, behavior.
            spectatorTooltip.GetChild(0).GetComponent<Text>().text = bloodAlcoolemyLevel.ToString() + "g/mL";
            spectatorTooltip.GetChild(1).GetComponent<Text>().text = bladder.ToString() + "mL";
            spectatorTooltip.GetChild(2).GetComponent<Text>().text = agressiveness.ToString() + "%";
            spectatorTooltip.GetChild(3).GetComponent<Text>().text = activeBehavior.getBehaviorName().ToString();
        }
    }
}