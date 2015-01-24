using UnityEngine;
using System.Collections;

public class DoorClose : MonoBehaviour
{

    public float CloseTime = 10f; // time to close in seconds
    public float closeDist = 1f; // How far the door moves (

    private float endY;
    private bool doneMoving = false;

	void Start()
    {
        endY = transform.position.y;
        transform.transform.transform.Translate(0, closeDist, 0);
	}
	
	void Update()
    {
        if (!doneMoving)
        {
            transform.Translate(0, -closeDist * Time.deltaTime / CloseTime, 0);
            if (transform.position.y < endY)
            {
                doneMoving = true;
                transform.position = new Vector3(transform.position.x, endY, transform.position.z);
                Debug.Log("Door closed");
                // hook for restart level and stuff here
            }
        }
	}
}
