using UnityEngine;
using UnityEngine.Networking;

public class GrabNDrop : NetworkBehaviour
{
    [SyncVar]
    public int cantidadItems = 0;

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

            Debug.Log("Agregado al inventario");
        }
        else
        {
            Debug.Log("inventario lleno, drop something with space key");
        }

    }

    [Command]
    void CmdServerDrop()
    {
        if (cantidadItems > 0)
        {
            cantidadItems--;
            var itemManager = GameObject.Find("ItemNetworkManager").GetComponent<ItemNetworkManager>();
            var itemPrefab = itemManager.itemPrefab;
            var item = (GameObject)Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Item itemScript = item.GetComponent<Item>();
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

