﻿using UnityEngine;
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
        endY = transform.localPosition.y;
        transform.transform.transform.Translate(0, closeDist, 0);
	}
	
	void Update()
    {
        transform.GetComponent<BoxCollider>().enabled = (elapsed / CloseTime > 0.8) || !SlideController.Sliding;
        if (!doneMoving)
        {
            elapsed += Time.deltaTime;
            Vector4 c = Vector4.Lerp(color, new Vector4(0.0f, 0.0f), elapsed/CloseTime);
            Vector3 pos = transform.localPosition;
            transform.localPosition = Vector3.Lerp(new Vector3(pos.x, endY + closeDist, pos.z), new Vector3(pos.x, endY, pos.z), elapsed / CloseTime);
            RenderSettings.ambientLight = new Color(c.x, c.y, c.z, c.w);
            Color light = Color.Lerp(Color.green, Color.red, elapsed/CloseTime);
            transform.GetChild(0).GetComponent<MeshRenderer>().material.color = light;
            transform.GetChild(0).GetChild(0).GetComponent<Light>().color = light;
            if (transform.localPosition.y <= endY)
            {
                doneMoving = true;
                transform.localPosition = new Vector3(transform.localPosition.x, endY, transform.localPosition.z);
                // hook for restart level and stuff here
            }
        }
	}
}
