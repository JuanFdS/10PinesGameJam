using UnityEngine;
using UnityEngine.Networking;

public class GrabNDrop : NetworkBehaviour
{
    [SyncVar]
    public int cantidadItems = 0;

    public GameObject[] inventory;

	public int index = -1;
	public int inventarioMaximo = 3;

	void OnTriggerEnter2D(Collider2D col) {
        if (!isServer) return;

		if(col.gameObject.tag =="targetobject")
		{ 
			Debug.Log("Estas más cerca de ganar");
            guardarEnInventario(col.gameObject);

		} else if (col.gameObject.tag=="colectable")
		{ 
			
			guardarEnInventario(col.gameObject);
		}
	}

	void guardarEnInventario(GameObject item)
	{
		Debug.Log(index);
        if (HayLugarEnElInventario() && item.GetComponent<Item>().canBePickedBy(gameObject))
        {
            cantidadItems++;
            Destroy(item);

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
        index++;
        inventory[index] = go;
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
			itemADroppear.GetComponent<Item>().Invocar();
			itemADroppear.SetActive(true);
		}
	}

	[Command]
	void CmdServerDrop() {
        if (cantidadItems > 0)
        {
            cantidadItems--;
            Debug.Log("baje el item");
            var itemManager = GameObject.Find("ItemNetworkManager").GetComponent<ItemNetworkManager>();
            var itemPrefab = itemManager.itemPrefab;
            if (itemPrefab == null) { Debug.Log("Item no existe1"); }
            var item = (GameObject)Instantiate(itemPrefab, transform.position, Quaternion.identity);
            if (item == null) { Debug.Log("Item no existe"); }
            //item.GetComponent<BoxCollider2D>().enabled = false;
            Item itemScript = item.GetComponent<Item>();
            //itemScript.Invocar();
            itemScript.rememberDroppedBy(gameObject);
            
            NetworkServer.Spawn(item);
        }
    }
		
	void Update()
	{
        if (!isLocalPlayer) return;

		if (Input.GetKeyDown(KeyCode.Space))
		{
            CmdServerDrop();
        }
	}

	private bool HayLugarEnElInventario()
	{
        return cantidadItems < inventarioMaximo;
    }
}

