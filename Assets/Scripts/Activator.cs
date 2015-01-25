using UnityEngine;
using System.Collections;

public class Activator : MonoBehaviour
{
    public Target[] Targets;

    public bool ButtonDown { get; private set; }

    public enum Type
    {
        Button,
        PlayerSensor,
        CubeSensor,
    };

    public Type ActivatorType;
	
	// Update is called once per frame
	void Update ()
	{
	    if (ActivatorType == Type.Button && Input.GetButtonDown("Fire1"))
        {
	        Transform t = GameObject.Find("Main Camera").transform;
	        RaycastHit hit;
	        if (Physics.Raycast(t.position, t.TransformDirection(Vector3.forward), out hit))
            {
	            if (hit.collider == transform.GetComponent<Collider>())
                {
                    foreach (Target ta in Targets)
                    {
                        ta.Running = !ta.Running;
                    }
                }
	        }
	    }
	    if (Input.GetButtonUp("Fire1"))
	    {
	        ButtonDown = false;
	    }
	}

    void OnTriggerEnter(Collider col)
    {
        if (ColliderIsOurType(col))
        {
            foreach (Target t in Targets)
            {
                t.Running = !t.Running;
            }
            //Target.GetComponent<Target>().Running = !Target.GetComponent<Target>().Running;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (ColliderIsOurType(col))
        {
            foreach (Target t in Targets)
            {
                t.Running = !t.Running;
            }
        }
    }

    bool ColliderIsOurType(Collider col)
    {
        return (ActivatorType == Type.PlayerSensor && col.tag == "Player")
            || (ActivatorType == Type.CubeSensor && col.tag == "CarryCube");
    }
}
