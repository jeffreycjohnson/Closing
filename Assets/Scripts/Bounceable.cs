using UnityEngine;
using System.Collections;

public class Bounceable : MonoBehaviour
{
	public float Bounciness = 100;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.name == "Bouncer")
		{
			this.GetComponent<ImpactReceiver>().AddImpact(Vector3.up * Bounciness);
		}
	}
}
