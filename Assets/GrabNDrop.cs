using UnityEngine;
using UnityEngine.Networking;

public class GrabNDrop : NetworkBehaviour
{
    [SyncVar]
    public int cantidadItems = 0;

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
        Debug.Log(cantidadItems);
        if (HayLugarEnElInventario() && item.GetComponent<Item>().canBePickedBy(gameObject))
        {
            cantidadItems++;
            Destroy(item);
			if (cantidadItems >= 3) {
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
        if (cantidadItems > 0)
        {
            cantidadItems--;
            var itemManager = GameObject.Find("ItemNetworkManager").GetComponent<ItemNetworkManager>();
			var droppedItem = itemManager.InstanciarItem (transform.position);
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
        return cantidadItems < inventarioMaximo;
    }
}

