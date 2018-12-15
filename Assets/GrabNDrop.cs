using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabNDrop : MonoBehaviour
{
	public GameObject[] inventory;

	public int index=0;

	void OnTriggerEnter2D(Collider2D col){
		
		if(col.gameObject.tag == "colectable" && col.gameObject.tag =="targetobject")
		{ //&& this.inventary not full

		} else if (col.gameObject.tag=="colectable") // && inventory not full
		{ 
			
			guardarEnInventario(col.gameObject);
		}
	}

	void guardarEnInventario(GameObject go)
	{Debug.Log(index);
			if(index<3){
				inventory[index]=go;
				index++;
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
		if(index==3) index=2;
		//inventory[index].SetActive(true);
		inventory[index].transform.position = transform.position;
		inventory[index]=null;
	
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && index >0)
		{
			Drop();
			index--;
		}
	}



}

