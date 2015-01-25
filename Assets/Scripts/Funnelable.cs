using UnityEngine;
using System.Collections;

public class Funnelable : MonoBehaviour
{

    GameObject funnel;
    static float FunnelSpeed = 0.02f;
    CharacterMotor charmotor;

    void Awake()
    {
        charmotor = GetComponent<CharacterMotor>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Funnel")
        {
            funnel = other.gameObject;
            //transform.Translate(new Vector3(0, 0.1f, 0));
            if (charmotor)
            {
                charmotor.movement.gravity = 0;
                charmotor.movement.velocity = new Vector3(0, 0, 0);
            }
            else if (rigidbody)
            {
                rigidbody.useGravity = false;
                rigidbody.velocity = new Vector3(0, 0, 0);
            }
            //GetComponent<CharacterMotor>().ju
            //transform.transform.transform.transform.Translate(0, 0.2f, 0);
            //GetComponent<CharacterMotor>().enabled = false;
            //GetComponent<CharacterController>().SimpleMove(new Vector3(0, -2f * Time.deltaTime, 0));
            Debug.Log("Enter Funnel");
        }
    }
	void Update()
    {
        if (funnel != null)
        {
            Vector3 angles = angles = funnel.transform.up;
            //GetComponent<CharacterMotor>().movement.velocity = new Vector3(angles.x * FunnelSpeed, angles.y * FunnelSpeed, angles.z * FunnelSpeed);
            transform.transform.Translate(angles.x * FunnelSpeed, angles.y * FunnelSpeed, angles.z * FunnelSpeed, Space.World);



        }
	}
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Funnel")
        {
            funnel = null;
            //transform.Translate(new Vector3(0, 0.1f, 0));
            if (charmotor)
            {
                charmotor.movement.gravity = 10;
            }
            else if (rigidbody)
            {
                rigidbody.useGravity = true;
            }
            //GetComponent<CharacterMotor>().enabled = true;
            //GetComponent<CharacterController>().SimpleMove(new Vector3(0, -2f * Time.deltaTime, 0));
            Debug.Log("Exit Funnel");
        }
    }
}
