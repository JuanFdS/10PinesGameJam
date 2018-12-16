using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject droppedBy;
    private float droppedAt;

    void setActive()
	{
		GetComponent<BoxCollider2D>().enabled=true;
	}

	public void Invocar()
	{
		Invoke("setActive",5f);
	}

    internal void rememberDroppedBy(GameObject jugador)
    {
        droppedBy = jugador;
        droppedAt = Time.time;
    }

    internal bool canBePickedBy(GameObject jugador)
    {
        return droppedBy != jugador || (Time.time - droppedAt >= 5f);
    }
}
