using UnityEngine;
using System.Collections;

public class Placeable : MonoBehaviour
{
    private static bool coroutinestarted = false;
    public static bool placing = false;
    private static bool shouldchangeplacing = false;
    public GameObject GhostPrefab;
    private GameObject Ghost;
    public float PushDistance = 0.2f;
    public GameObject Door;

	void Start()
    {
        Ghost = (GameObject)GameObject.Instantiate(GhostPrefab, transform.position, Quaternion.identity);
        Ghost.SetActive(false);
        if (!coroutinestarted)
        {
            StartCoroutine("SetPlacing");
            coroutinestarted = true;
        }
	}

    IEnumerator SetPlacing()
    {
        while (true)
        {
            if (shouldchangeplacing)
            {
                placing = false;
                shouldchangeplacing = false;
            }
            yield return null;
        }
    }
	
	void Update()
    {
        Camera cam = FindObjectOfType<Camera>();
        if (cam == null) return;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool raycastpickup = Physics.Raycast(ray, out hit, Mathf.Infinity, (1 << 8) | (1 << 9));

        if (raycastpickup && !Ghost.activeSelf && (hit.transform.gameObject == gameObject || hit.transform == transform.parent)
            && Input.GetButtonDown("Place") && !placing && !Door.GetComponent<DoorClose>().doneMoving)
        {
            Ghost.SetActive(true);
            placing = true;
            if (gameObject.layer == 8) gameObject.layer = 2;
        }
        else if (Ghost.activeSelf)
        {
            // 9 means both doesn't place on others and others can't be placed on it.
            int mask;
            if (gameObject.layer == 9) mask = ~((1 << 2) | (1 << 8) | (1 << 9));
            else mask = ~((1 << 9) | (1 << 2));
            bool raycastdrop = Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
            Vector3 pushout = hit.normal;
            pushout.Normalize();
            pushout.Scale(new Vector3(PushDistance, PushDistance, PushDistance));
            Ghost.transform.position = hit.point + pushout;

            Vector3 vec = hit.normal;
            if (hit.normal == Vector3.up) {
                Ghost.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (hit.normal == Vector3.down) {
                Ghost.transform.rotation = Quaternion.Euler(new Vector3(180, 0, 0));
            }
            else if (hit.normal == Vector3.forward) {
                Ghost.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            }
            else if (hit.normal == Vector3.back) {
                Ghost.transform.rotation = Quaternion.Euler(new Vector3(270, 0, 0));
            }
            else if (hit.normal == Vector3.left) {
                Ghost.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
            else if (hit.normal == Vector3.right) {
                Ghost.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
            }
            bool pastDoor = Ghost.transform.position.x > Door.transform.position.x;
            if (pastDoor || !raycastdrop) Ghost.transform.position = new Vector3(-1000, 0, 0); // programming

            if (Input.GetButtonDown("Place") && raycastdrop && !pastDoor)
            {
                Ghost.SetActive(false);
                transform.position = Ghost.transform.position;
                transform.rotation = Ghost.transform.rotation;
                shouldchangeplacing = true;
                if (rigidbody)
                {
                    rigidbody.angularVelocity = Vector3.zero;
                    rigidbody.velocity = Vector3.zero;
                }
                if (gameObject.layer == 2) gameObject.layer = 8;
                if (hit.transform.name == "Platform")
                {
                    transform.parent = hit.transform;
                }
                else
                {
                    transform.parent = null;
                }
            }

            if (Door.GetComponent<DoorClose>().doneMoving)
            {
                Ghost.SetActive(false);
                shouldchangeplacing = true;
            }
        }
        
	}
}
