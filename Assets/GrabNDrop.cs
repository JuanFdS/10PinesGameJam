using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrabNDrop : NetworkBehaviour
{
    public Stack<int> inventario = new Stack<int>();

    public GameObject cartelVictoriaPrefab;
    public GameObject cartelDerrotaPrefab;

    public int inventarioMaximo = 3;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!isServer) return;

        if (col.gameObject.tag == "targetobject")
        {
            Debug.Log("Estas más cerca de ganar");
            guardarEnInventario(col.gameObject);

        }
        else if (col.gameObject.tag == "colectable")
        {

            guardarEnInventario(col.gameObject);
        }
    }

    void guardarEnInventario(GameObject item)
    {
        Debug.Log(inventario.Count);
        if (HayLugarEnElInventario() && item.GetComponent<Item>().canBePickedBy(gameObject))
        {
            inventario.Push(item.GetComponent<Item>().itemType);
            Destroy(item);
			if (inventario.Count >= 3 && inventario.Contains(0) && inventario.Contains(1) && inventario.Contains(2)) {
				RpcNotificarResultado ();
			}

            Debug.Log("Agregado al inventario");
        }
        else
        {
            Debug.Log("inventario lleno, drop something with space key");
        }

    }

	[ClientRpc]
	void RpcNotificarResultado()
	{
		if (isLocalPlayer) {
			Instantiate (cartelVictoriaPrefab);
		} else {
			Instantiate (cartelDerrotaPrefab);
		}
	}

    [Command]
    void CmdServerDrop()
    {
        if (inventario.Count > 0)
        {
            var tipoItem = inventario.Pop();
            var itemManager = GameObject.Find("ItemNetworkManager").GetComponent<ItemNetworkManager>();
			var droppedItem = itemManager.InstanciarItem (transform.position, tipoItem);
			Item itemScript = droppedItem.GetComponent<Item>();
			itemScript.rememberDroppedBy(gameObject);
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
        return inventario.Count < inventarioMaximo;
    }
}

