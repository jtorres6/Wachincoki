using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class muestrapuntos : MonoBehaviour
{
    public Text puntosBasura;
    private Transform padre;
    private int puntosactuales;
    private int puntosmaximos;

    private Canvas canvas;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        puntosmaximos = gameManager.truckCapacity;
        puntosactuales = gameManager.GetTruckCurrentCapacity();

        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        puntosBasura.transform.position = posPoints;
        puntosBasura.text = (puntosactuales.ToString() + " / " + puntosmaximos.ToString());
        //puntosBasura.transform.SetParent (canvas.transform,false);
    }
}
