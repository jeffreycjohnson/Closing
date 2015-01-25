using UnityEngine;
using System.Collections;

public class LaserGrill : MonoBehaviour
{
    public GameObject ToggleObjectParent;

    private Target target;
    private bool lastrunning;

	void Start()
    {
        target = GetComponent<Target>();
        lastrunning = !target.Running; // so that we do the logic on the first Update
	}

	void Update()
    {
        if (target.Running != lastrunning)
        {
            ToggleObjectParent.SetActive(target.Running);
        }
        lastrunning = target.Running;
	}
}
