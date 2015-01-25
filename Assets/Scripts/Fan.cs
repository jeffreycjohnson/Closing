using UnityEngine;
using System.Collections;

public class Fan : MonoBehaviour
{
    private Target target;
    private float spinSpeed = 0;
    private float maxSpinSpeed = 0.5f;
    private float dSpinSpeed = 0.1f;
    public GameObject Funnel;
    public static bool FunnelOff = false;

	void Start()
    {
        target = GetComponent<Target>();
	}
	
	void Update()
    {
        Funnel.GetComponent<Collider>().enabled = target.Running;
        Funnel.SetActive(target.Running);
        FunnelOff = !target.Running;

        if (target.Running)
        {
            spinSpeed += dSpinSpeed;  
            if (spinSpeed > maxSpinSpeed) spinSpeed = maxSpinSpeed;
        }
        else
        {
            spinSpeed -= dSpinSpeed;
            spinSpeed = 0;
            if (spinSpeed < 0) spinSpeed = 0;
        }
        transform.Rotate(new Vector3(0, 0, spinSpeed));
	}
}
