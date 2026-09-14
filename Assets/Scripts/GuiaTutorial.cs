using UnityEngine;
using TMPro;

[System.Serializable]
public struct Dialogo
{
    [TextArea(2, 5)]
    public string oracion;
    public bool enfocarProtagonista;
}

public class GuiaTutorial : MonoBehaviour
{
    public GameObject cuadroDialogo;
    public TextMeshProUGUI textoUI;
    public Dialogo[] lineasDialogo;
    public MovimientoJugador scriptJugador;

    public GameObject camaraPrincipal;
    public GameObject camaraConejo;
    public GameObject camaraJugador;

    public Transform posicionCinematicaJugador;
    public Transform posicionCinematicaConejo;

    private int lineaActual = 0;
    private bool enDialogo = false;

    void Start()
    {
        cuadroDialogo.SetActive(false);
        camaraConejo.SetActive(false);
        camaraJugador.SetActive(false);
    }

    void Update()
    {
        if (enDialogo && Input.GetMouseButtonDown(0))
        {
            AvanzarDialogo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Jugador" && !enDialogo)
        {
            IniciarDialogo();
        }
    }

    void IniciarDialogo()
    {
        enDialogo = true;
        cuadroDialogo.SetActive(true);
        scriptJugador.puedeMoverse = false;

        camaraPrincipal.SetActive(false);

        scriptJugador.transform.position = posicionCinematicaJugador.position;
        scriptJugador.transform.rotation = posicionCinematicaJugador.rotation;

        transform.position = posicionCinematicaConejo.position;
        transform.rotation = posicionCinematicaConejo.rotation;

        lineaActual = 0;
        MostrarLinea();
    }

    void AvanzarDialogo()
    {
        lineaActual++;

        if (lineaActual < lineasDialogo.Length)
        {
            MostrarLinea();
        }
        else
        {
            TerminarDialogo();
        }
    }

    void MostrarLinea()
    {
        textoUI.text = lineasDialogo[lineaActual].oracion;

        if (lineasDialogo[lineaActual].enfocarProtagonista)
        {
            camaraConejo.SetActive(false);
            camaraJugador.SetActive(true);
        }
        else
        {
            camaraJugador.SetActive(false);
            camaraConejo.SetActive(true);
        }
    }

    void TerminarDialogo()
    {
        enDialogo = false;
        cuadroDialogo.SetActive(false);
        scriptJugador.puedeMoverse = true;

        camaraConejo.SetActive(false);
        camaraJugador.SetActive(false);
        camaraPrincipal.SetActive(true);

        GetComponent<Collider>().enabled = false;
    }
}