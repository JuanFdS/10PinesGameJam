using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemNetworkManager : NetworkBehaviour
{
	public GameObject itemPrefab;

	public GameObject InstanciarItem(Vector3 posicion)
	{
		var item = (GameObject)Instantiate(itemPrefab, posicion, Quaternion.identity);
		NetworkServer.Spawn(item);
		return item;
	}

}
