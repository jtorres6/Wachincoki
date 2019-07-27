using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour{

    public Text puntosBasura;
    private Transform padre;
    private int puntos;


    // Start is called before the first frame update
    void Start()
    {
        padre = transform.parent;
        puntos = padre.gameObject.GetComponentInChildren<Rubish> ().value;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posPoints = Camera.main.WorldToScreenPoint(this.transform.position);
        puntosBasura.transform.position = posPoints;
        puntosBasura.text = puntos.ToString();
    }
}
