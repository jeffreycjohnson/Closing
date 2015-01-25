using UnityEngine;
using System.Collections;

public class FallController : MonoBehaviour 
{
	private bool _falling;
	public bool Falling
	{
		get
		{
			return _falling;
		}
		set
		{
			_falling = value;

			if(_falling)
			{
				this.rigidbody.WakeUp();
			}
			else
			{
				this.rigidbody.Sleep();
			}
		}
	}

	void Start()
	{
		Falling = false;
	}
}
