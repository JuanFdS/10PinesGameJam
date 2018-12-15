using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnsManager : MonoBehaviour
{
	public GameObject jugadorActivo;
	public GameObject jugadorInactivo;
	public GameObject jugadorUno;
	public GameObject jugadorDos;

	float tiempoTurno = 10f; //



    // Start is called before the first frame update
    void Start()
    {
		
		jugadorActivo = jugadorUno;
		jugadorActivo.GetComponent<Movement>().enabled = true;
		jugadorActivo.GetComponentInChildren<SpriteRenderer>().enabled=true;
		jugadorInactivo = jugadorDos;
    
	}

	void setActive(GameObject jugador)
	{
		jugador.GetComponent<Movement>().enabled = true;
		jugadorInactivo = jugadorActivo;
		jugadorActivo = jugador;
		jugadorActivo.GetComponentInChildren<SpriteRenderer>().enabled=true;
		jugadorInactivo.GetComponent<Movement>().enabled = false;
		jugadorInactivo.GetComponentInChildren<SpriteRenderer>().enabled=false;


	}

	void unJugador()
	{
		tiempoTurno=10f;
	}

    // Update is called once per frame
    void Update()
    {
		
			
		if( tiempoTurno > 0)
		{
			tiempoTurno-=Time.deltaTime;
		} else
		{
			setActive(jugadorInactivo);
			tiempoTurno=10f;
		}

    }
}
