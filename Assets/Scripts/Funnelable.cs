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
            
            if (charmotor)
            {
                charmotor.movement.gravity = 0;
                transform.Translate(new Vector3(0, 0.5f, 0));
                //GetComponent<CharacterController>().SimpleMove(new Vector3(0, 1f, 0));
                //GetComponent<CharacterController>().Move(new Vector3(0, 1f, 0));
                //transform.Translate(new Vector3(0, -0.1f, 0));
                //GetComponent<CharacterController>().velocity = new Vector3(0, 1f, 0);
                //charmotor.constantForce.rigidbody.AddForce(new Vector3(0, 50f, 0));
            }
            else if (rigidbody)
            {
                rigidbody.useGravity = false;
                rigidbody.velocity = new Vector3(0, 0, 0);
                //rigidbody.AddForce(new Vector3(0.2f, 0, 0));
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
                //GetComponent<CharacterController>().Move(new Vector3(0, 0.03f, 0));
                
                transform.transform.Translate(dir, Space.World);
            }
            else
            {
                transform.transform.Translate(dir, Space.World);
                //rigidbody.velocity = new Vector3(0, 0.2f, 0);
            }
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
        }
    }
}
