using UnityEngine;
using System.Collections;

public class Remote : MonoBehaviour
{
    private float elapsed = 0f;
    public float AnimationTime = 0.2f;
    private bool _bobbing = false;
    public float BobAmount = 0.1f;
    private float _amount;
	
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
	            _amount += BobAmount*Time.deltaTime;
                transform.Translate(new Vector3(0, 0, BobAmount * Time.deltaTime), Space.Self);
	        }
            else if (elapsed / AnimationTime < 1f)
            {
                _amount -= BobAmount * Time.deltaTime;
                transform.Translate(new Vector3(0, 0, -BobAmount * Time.deltaTime), Space.Self);
	        }
            else
            {
                transform.Translate(new Vector3(0, 0, -_amount), Space.Self);
                _amount = 0;
                _bobbing = false;
            }
	    }
	}
}
