﻿using UnityEngine;
using System.Collections;

public class Funnelable : MonoBehaviour
{

    GameObject funnel;
    static float FunnelSpeed = 0.06f;
    CharacterMotor charmotor;

    void Awake()
    {
        charmotor = GetComponent<CharacterMotor>();
    }

    void FixedUpdate()
    {
        if (rigidbody && rigidbody.IsSleeping()) rigidbody.WakeUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Funnel")
        {
            funnel = other.gameObject;

            if (charmotor)
            {
                GetComponent<CharacterController>().stepOffset = 0;
                charmotor.movement.gravity = 0;
                charmotor.movement.velocity = new Vector3(0f, 0f, 0f);
                //charmotor.SetVelocity(Vector3.zero);
            }
            else if (rigidbody)
            {
                rigidbody.useGravity = false;
                rigidbody.velocity = new Vector3(0, 0, 0);
                rigidbody.angularVelocity = new Vector3(0, 0, 0);
                rigidbody.rotation = Quaternion.identity;
                rigidbody.freezeRotation = true;
            }
        }
    }
	void Update()
    {
        if (funnel != null)
        {
            Vector3 dir = funnel.transform.up;
            dir.Scale(new Vector3(FunnelSpeed, FunnelSpeed, FunnelSpeed));
            if (charmotor)
            {
                GetComponent<CharacterController>().Move(dir);
            }
            else
            {
                transform.transform.Translate(dir, Space.World);
                rigidbody.velocity = new Vector3(0, 0, 0);
            }
            if (Fan.FunnelOff)
            {
                ExitFunnel();
            }
        }
        
	}
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Funnel" && collider.gameObject == funnel)
        {
            ExitFunnel();
        }
    }

    private void ExitFunnel()
    {
        funnel = null;
        if (charmotor)
        {
            charmotor.movement.gravity = 10;
            GetComponent<CharacterController>().stepOffset = 0.2f;
        }
        else if (rigidbody)
        {
            rigidbody.useGravity = true;
            rigidbody.freezeRotation = false;
        }
    }
}
