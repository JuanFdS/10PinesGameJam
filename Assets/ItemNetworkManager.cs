using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemNetworkManager : NetworkBehaviour
{
	public GameObject[] itemPrefabs;

	public GameObject InstanciarItem(Vector3 posicion, int tipoItem)
	{
		var item = (GameObject)Instantiate(itemPrefabs[tipoItem], posicion, Quaternion.identity);
		NetworkServer.Spawn(item);
		return item;
	}

}
