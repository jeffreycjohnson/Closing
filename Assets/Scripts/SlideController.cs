using UnityEngine;
using System.Collections;

public class SlideController : MonoBehaviour {
    private float _timer = 0;
    private Quaternion _origRot;
    public static bool Sliding = false;
	
	// Update is called once per frame
    void Update()
    {
        Transform t = GameObject.Find("Main Camera").transform;
        RaycastHit hit;
        Transform selected = GameObject.Find("Selected Effect").transform;
        selected.GetComponent<MeshRenderer>().enabled = false;
        if (Physics.Raycast(t.position, t.TransformDirection(Vector3.forward), out hit))
        {
            if (hit.transform.GetComponent<Activator>() != null ||
                (hit.transform.GetComponent<Placeable>() != null && !hit.transform.GetComponent<Placeable>().Door.GetComponent<DoorClose>().doneMoving))
            {
                selected.GetComponent<MeshRenderer>().enabled = true;
                selected.position = hit.transform.position;
                selected.localScale = hit.transform.localScale * 1.1f;
                selected.rotation = hit.transform.rotation;
            }
        }
        if (Input.GetButtonDown("Fire2") && !Sliding)
	    {
            Sliding = true;
	        _timer = 0;
            transform.GetComponent<CharacterMotor>().movement.maxForwardSpeed += 1;
            _origRot = transform.rotation;
	    }
        if (Sliding)
	    {
            _timer += Time.deltaTime;
	        if (_timer < 0.5)
	        {
                transform.localRotation = Quaternion.Lerp(_origRot,
                    new Quaternion(_origRot.x, _origRot.y, _origRot.z + 0.5f, _origRot.w), _timer * 2);
	        }
            else if (_timer < 1)
            {
                transform.localRotation = Quaternion.Lerp(new Quaternion(_origRot.x, _origRot.y, _origRot.z + 0.5f, _origRot.w), _origRot, (_timer - 0.5f) * 2);
            }
            else
            {
                Sliding = false;
                transform.localRotation = _origRot;
                transform.GetComponent<CharacterMotor>().movement.maxForwardSpeed -= 1;
            }
	    }
	}
}
