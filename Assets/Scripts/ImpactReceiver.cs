using UnityEngine;
using System.Collections;

public class ImpactReceiver : MonoBehaviour
{
	public float Mass = 3;
	private CharacterController _cc;
	private Vector3 _currentImpact;

	// Use this for initialization
	void Start ()
	{
		_cc = this.GetComponent<CharacterController>();
	}

	public void AddImpact(Vector3 forceVector)
	{
		_currentImpact += forceVector / Mass;
	}

	// Update is called once per frame
	void Update ()
	{
		if (_currentImpact.magnitude > 0.2)
		{
			_cc.Move(_currentImpact * Time.deltaTime);
			_currentImpact = Vector3.Lerp(_currentImpact, Vector3.zero, 12 * Time.deltaTime);
		}
	}
}
