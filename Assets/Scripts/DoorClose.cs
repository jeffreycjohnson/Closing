using UnityEngine;
using System.Collections;

public class DoorClose : MonoBehaviour
{

    public float CloseTime = 10f; // time to close in seconds

    private Vector3 openPos;
    private float closeDist = 1f;

	void Start ()
    {
	    
	}
	
	void Update ()
    {
        transform.Translate(0, -closeDist * Time.deltaTime / CloseTime, 0);
	}
}
