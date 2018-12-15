using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    [Tooltip("Tiempo Inicial En Segundos")]
    public int tiempoInicial;
    [Tooltip("Escala Del Tiempo Del Timer")]
    [Range(-10.0f, 10.0f)]
    public float EscalaDelTiempo = 1f;

    private Text myText;
    private float tiempoDelFrameConTimeScale = 0f;
    private float tiempoAMostrarEnSegundos = 0f;
    private float escalaATiempoAlPausar, escalaDeTiempoInicial;
    private bool estaPausado = false;

    // Start is called before the first frame update
    void Start()
    {
        escalaDeTiempoInicial = EscalaDelTiempo;
        myText = GetComponent<Text>();
        tiempoAMostrarEnSegundos = tiempoInicial;
        ActualizarTimer(tiempoInicial);
    }

    // Update is called once per frame
    void Update()
    {
        tiempoDelFrameConTimeScale = Time.deltaTime * EscalaDelTiempo;
        tiempoAMostrarEnSegundos += tiempoDelFrameConTimeScale;
        ActualizarTimer(tiempoAMostrarEnSegundos);
        }

    public void ActualizarTimer(float tiempoEnSegundos)
    {
        int minutos = 0;
        int segundos = 0;
        string textoDelReloj;

        if (tiempoEnSegundos < 0)
        {
            tiempoEnSegundos = 0;
        }

        minutos = (int)tiempoEnSegundos / 60;
        segundos = (int)tiempoEnSegundos % 60;
        textoDelReloj = minutos.ToString("00") + ":" + segundos.ToString("00");
        myText.text = textoDelReloj;

    }
}
