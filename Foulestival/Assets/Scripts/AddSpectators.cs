using UnityEngine;
using System.Collections;

public class AddSpectators : MonoBehaviour {


	public Transform spectator;
	public Transform arrivalZone;
    public float arrivalZoneSize;
    private int numberOfSpectators;

	// Use this for initialization
	void Start ()
    {
        numberOfSpectators = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < 5;i++)
                createNew();
        }
	}
    void createNew()
    {
        Instantiate(spectator, new Vector3(arrivalZone.position.x + (Random.value * 2 - 1) * arrivalZoneSize, 0, arrivalZone.position.z + (Random.value * 2 - 1)) * arrivalZoneSize, Quaternion.identity);
        numberOfSpectators++;
    }

}
