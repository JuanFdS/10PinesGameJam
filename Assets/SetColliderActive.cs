using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderActive : MonoBehaviour
{
	void setActive()
	{
		GetComponent<BoxCollider2D>().enabled=true;
	}

	public void Invocar()
	{
		Invoke("setActive",5f);
	}

}
