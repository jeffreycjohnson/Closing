using UnityEngine;
using System.Collections;

public class DoorClose : MonoBehaviour
{

    public float CloseTime = 10f; // time to close in seconds
    private float elapsed = 0f;
    public float closeDist = 1f; // How far the door moves (

    private float endY;
    public bool doneMoving = true;
    private Vector4 color = new Vector4(0.9f, 0.9f, 0.9f, 1f);

	void Start()
	{
        endY = transform.position.y;
        transform.transform.transform.Translate(0, closeDist, 0);
	}
	
	void Update()
    {
        transform.GetComponent<BoxCollider>().enabled = (elapsed / CloseTime > 0.8) || !SlideController.Sliding;
        if (!doneMoving)
        {
            elapsed += Time.deltaTime;
            Vector4 c = Vector4.Lerp(color, new Vector4(0.0f, 0.0f), elapsed/CloseTime);
            transform.Translate(0, -closeDist * Time.deltaTime / CloseTime, 0);
            RenderSettings.ambientLight = new Color(c.x, c.y, c.z, c.w);
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
