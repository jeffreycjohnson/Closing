using UnityEngine;
using System.Collections;

public class DoorClose : MonoBehaviour
{

    public float CloseTime = 10f; // time to close in seconds
    public float closeDist = 1f; // How far the door moves (

    private float endY;
    private bool doneMoving = false;
    private Vector4 color = new Vector4(0.5f, 0.55f, 0.45f, 1f);

	void Start()
    {
        endY = transform.position.y;
        transform.transform.transform.Translate(0, closeDist, 0);
	}
	
	void Update()
    {
        if (!doneMoving)
        {
            color *= 0.995f;
            transform.Translate(0, -closeDist * Time.deltaTime / CloseTime, 0);
            RenderSettings.ambientLight = new Color(color.x, color.y, color.z, color.w);
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
