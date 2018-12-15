using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrabNDrop : NetworkBehaviour
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
			if(isServer) { RpcDesaparecerItem(go); }

			Debug.Log("Agregado al inventario");
		}
		else
		{
			Debug.Log("inventario lleno, drop something with space key");
		}

	}

	[ClientRpc]
	void RpcDesaparecerItem(GameObject go)
	{
		go.SetActive (false);
	}
	
	GameObject itemADroppear;

	[ClientRpc]
	void RpcClientDrop()
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

	[Command]
	void CmdServerDrop() {
		RpcClientDrop();
	}
		
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space) && index >= 0)
		{
			CmdServerDrop();
		}
	}

	private bool inventarioLleno()
	{
		return (index >= inventarioMaximo - 1);
	}
}

