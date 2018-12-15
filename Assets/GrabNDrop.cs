using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabNDrop : MonoBehaviour
{
	public GameObject[] inventory;

	public int index = -1;
	public int inventarioMaximo = 3;

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag =="targetobject")
		{ 
			Debug.Log("Estas más cerca de ganar");
			guardarEnInventario(col.gameObject);

		} else if (col.gameObject.tag=="colectable")
		{ 
			
			guardarEnInventario(col.gameObject);
		}
	}

	void guardarEnInventario(GameObject go)
	{
		Debug.Log(index);
		if(!inventarioLleno()){
			index++;
			inventory[index]=go;
			go.SetActive(false);

			Debug.Log("Agregado al inventario");
		}
		else
		{
			Debug.Log("inventario lleno, drop something with space key");
		}

	}
	
	GameObject itemADroppear;

	void Drop()
	{
		itemADroppear = inventory [index];
		if (itemADroppear != null) {
			inventory[index] = null;
			index--;
			itemADroppear.transform.position = transform.position;
			itemADroppear.GetComponent<BoxCollider2D>().enabled = false;
			itemADroppear.GetComponent<SetColliderActive>().Invocar();
			itemADroppear.SetActive(true);
		}
	}
		
	void Update()
	{
		
		if (Input.GetKeyDown(KeyCode.Space) && index >= 0)
		{
			
			Drop();
		}
	}

	private bool inventarioLleno()
	{
		return (index >= inventarioMaximo - 1);
	}
}

