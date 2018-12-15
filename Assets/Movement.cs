using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour
{ 
	public int speed;
	
	void Update ()
	{
		if (isLocalPlayer)
		{
			var dir = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
			transform.position += dir * speed * Time.deltaTime;
		}
	}
}
