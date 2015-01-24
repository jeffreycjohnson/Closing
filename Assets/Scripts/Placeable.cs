using UnityEngine;
using System.Collections;

public class Placeable : MonoBehaviour
{
    private bool beingplaced;
    public GameObject GhostPrefab;
    private GameObject Ghost;

	void Start()
    {
        Ghost = (GameObject)GameObject.Instantiate(GhostPrefab, transform.position, Quaternion.identity);
        Ghost.SetActive(false);
	}
	
	void Update()
    {
        Ray ray = FindObjectOfType<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        bool raycast = Physics.Raycast(ray, out hit);
        
        if (raycast && !Ghost.activeSelf && hit.transform.gameObject == gameObject && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Picked up");
            Ghost.SetActive(true);
        }
        else if (Ghost.activeSelf)
        {
            Vector3 pushout = hit.normal;
            pushout.Normalize();
            Vector3 ghostscale = Ghost.transform.localScale;
            ghostscale.Scale(new Vector3(0.5f, 0.5f, 0.5f));
            pushout.Scale(ghostscale);
            Ghost.transform.position = hit.point + pushout;
            if (Input.GetMouseButtonDown(0))
            {
                Ghost.SetActive(false);
                Debug.Log("Place");
                transform.position = Ghost.transform.position;
            }
        }
        
	}
}
