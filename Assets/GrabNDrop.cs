using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabNDrop : MonoBehaviour
{
	public GameObject[] inventory;

	public int index = -1;

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "colectable" && col.gameObject.tag =="targetobject")
		{ //&& this.inventary not full

		} else if (col.gameObject.tag=="colectable") // && inventory not full
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
	
	
	void Drop()
	{
		var itemADroppear = inventory [index];
		if (itemADroppear != null) {
			inventory[index] = null;
			index--;
			itemADroppear.transform.position = transform.position;
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
		return index > 3;
	}
}

