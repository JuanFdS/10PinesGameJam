using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{ 
	public int speed;
	
	void Update ()
	{
		var direction = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		transform.position += direction * speed * Time.deltaTime;
	}
}
