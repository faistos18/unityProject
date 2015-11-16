using UnityEngine;
using System.Collections;

public class ClickToGo : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    // Use this for initialization
    void Start()
    {
        navMeshAgent = (NavMeshAgent)gameObject.GetComponent("NavMeshAgent");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))//1 is right click, 0 is left, 2 is middle.
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000000000000))
            {
                navMeshAgent.destination = hit.point;
            }
        }
    }

}
