using UnityEngine;
using System.Collections;

public class Remote : MonoBehaviour
{
    private float elapsed = 0f;
    public float AnimationTime = 0.2f;
    private bool _bobbing = false;
    public float BobAmount = 0.1f;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Place"))
	    {
	        _bobbing = true;
	        elapsed = 0f;
	    }
	    if (_bobbing)
        {
            elapsed += Time.deltaTime;
	        if (elapsed/AnimationTime < 0.5f)
	        {
                transform.Translate(new Vector3(0, 0, BobAmount * Time.deltaTime), Space.Self);
	        }
            else if (elapsed / AnimationTime < 1f)
            {
                transform.Translate(new Vector3(0, 0, -BobAmount * Time.deltaTime), Space.Self);
	        }
            else
            {
                _bobbing = false;
            }
	    }
	}
}
