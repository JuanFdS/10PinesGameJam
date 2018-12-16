using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrabNDrop : NetworkBehaviour
{
    public GameObject cartelInventarioLleno;
    public GameObject cartelVictoriaPrefab;
    public GameObject cartelDerrotaPrefab;
    public int inventarioMaximo = 3;

    private GameObject instanciaCartelInventarioLleno;
    private Queue<int> inventario = new Queue<int>();

    public GameObject InstanciaCartelInventarioLleno
    {
        get {
            if (instanciaCartelInventarioLleno == null)
            {
                instanciaCartelInventarioLleno = Instantiate(cartelInventarioLleno);
                instanciaCartelInventarioLleno.SetActive(false);
            }
            return instanciaCartelInventarioLleno;
        }
        set
        {
            instanciaCartelInventarioLleno = value;
        }
    }

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
        if (HayLugarEnElInventario() && PuedeAgarrar(item))
        {
            AgregarAlInventario(item);

            if (TieneTodosLosTiposDeItems())
            {
                RpcNotificarQueGanaElJugadorActual();
            }
        }
        else
        {
            Debug.Log("Inventario lleno, drop something with space key");
        }

    }

    private void AgregarAlInventario(GameObject item)
    {
        int itemType = item.GetComponent<Item>().itemType;
        inventario.Enqueue(itemType);
        Destroy(item);
        Debug.Log("Agregado al inventario item tipo " + itemType);

        RpcActualizarEstadoInventario(!HayLugarEnElInventario());
    }

    [ClientRpc]
    private void RpcActualizarEstadoInventario(bool inventarioLleno)
    {
        if (isLocalPlayer)
            InstanciaCartelInventarioLleno.SetActive(inventarioLleno);
    }

    private bool PuedeAgarrar(GameObject item)
    {
        return item.GetComponent<Item>().canBePickedBy(gameObject);
    }

    private bool TieneTodosLosTiposDeItems()
    {
        int[] tiposDeItems = new int[] { 0, 1, 2 };
        foreach (int tipoItem in tiposDeItems)
        {
            if (!inventario.Contains(tipoItem)) return false;
        }
        return true;
    }

    [ClientRpc]
	void RpcNotificarQueGanaElJugadorActual()
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
            var tipoItem = inventario.Dequeue();
            var itemManager = GameObject.Find("ItemNetworkManager").GetComponent<ItemNetworkManager>();
			var droppedItem = itemManager.InstanciarItem (transform.position, tipoItem);
			Item itemScript = droppedItem.GetComponent<Item>();
			itemScript.rememberDroppedBy(gameObject);
            RpcActualizarEstadoInventario(!HayLugarEnElInventario());
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

