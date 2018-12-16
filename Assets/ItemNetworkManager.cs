using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ItemNetworkManager : NetworkBehaviour
{
	public GameObject itemPrefab;
	public int numberOfItems;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public override void OnStartServer()
	{
		for (int i=0; i < numberOfItems; i++)
		{
			Debug.Log ("Created coso");
			var spawnPosition = new Vector3(
				Random.Range(-2.0f, 2.0f),
				Random.Range(-2.0f, 2.0f),
				0.0f);

			var spawnRotation = Quaternion.Euler( 
				0.0f, 
				0.0f, 
				0.0f);

			var item = (GameObject)Instantiate(itemPrefab, spawnPosition, spawnRotation);
			NetworkServer.Spawn(item);
		}
	}
}
