using UnityEngine;
using System.Collections;

public class Placeable : MonoBehaviour
{
    private bool beingplaced;
    public GameObject GhostPrefab;
    private GameObject Ghost;
    public float PushDistance = 0.2f;

	void Start()
    {
        Ghost = (GameObject)GameObject.Instantiate(GhostPrefab, transform.position, Quaternion.identity);
        Ghost.SetActive(false);
	}
	
	void Update()
    {
        Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool raycastpickup = Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8);
        
        
        if (raycastpickup && !Ghost.activeSelf && hit.transform.gameObject == gameObject && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Picked up");
            Ghost.SetActive(true);
        }
        else if (Ghost.activeSelf)
        {
            bool raycastdrop = Physics.Raycast(ray, out hit, Mathf.Infinity, ~((1 << 8) | (1 << 2)));
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

            if (Input.GetMouseButtonDown(0) && raycastdrop)
            {
                Ghost.SetActive(false);
                Debug.Log("Place");
                transform.position = Ghost.transform.position;
                transform.rotation = Ghost.transform.rotation;
            }
        }
        
	}
}
