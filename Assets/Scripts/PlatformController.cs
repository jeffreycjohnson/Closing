using UnityEngine;
using System.Collections;

public class PlatformController : MonoBehaviour
{
	public bool MoveInXAxis = true;
	public bool MovePositiveInitially = true;
	public float Speed = 100;
	public bool ShouldMove = true;

	private bool _movingPositively;

	// Use this for initialization
	void Start ()
	{
		_movingPositively = MovePositiveInitially;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		float moveAmount = (ShouldMove ? 1 : 0) * (_movingPositively ? 1 : -1) * Speed * Time.deltaTime;

		if (MoveInXAxis)
		{
			gameObject.rigidbody.velocity = Vector3.right * moveAmount;
		}
		else
		{
			gameObject.rigidbody.velocity = Vector3.forward * moveAmount;
		}
	}


	void OnCollisionEnter(Collision col)
	{
	    if (col.gameObject.tag != "CarryCube")
	    {
	        _movingPositively = !_movingPositively;
	    }
	}
}
