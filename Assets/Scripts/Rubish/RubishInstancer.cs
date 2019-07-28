using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubishInstancer : MonoBehaviour
{
    //const float y_offset = 0.5f;

    public InstanceArea[] areas;
    public Rubish[] rubishTypes;

    public Canvas canvas;
    public int maxCounter;

    private List<GameObject> rubishInstances;
    private int currentCounter;

    public GameObject textPrefab;

    RubishInstancer() {
        rubishInstances = new List<GameObject>();
        currentCounter = 0;
    }

    // Start is called before the first frame update
    void OnEnable() {
        List<Rubish> noLongerValid = new List<Rubish>();
        bool valid = false;

        // Greedy to create rubish
        while (currentCounter < maxCounter && noLongerValid.Count < rubishTypes.Length) {
            do {
                int area_idx = Random.Range(0, areas.Length);
                InstanceArea insarea = areas[area_idx];

                int idx = Random.Range(0, rubishTypes.Length);
                Rubish rubishType = rubishTypes[idx];
                rubishType.ownership = insarea.ownership;

                float x = Random.Range(insarea.minX, insarea.maxX);
                float y = Random.Range(insarea.minY, insarea.maxY);
                float z = Random.Range(insarea.minZ, insarea.maxZ);

                if (currentCounter + rubishType.value <= maxCounter) {
                    GameObject rubish = Instantiate(rubishType.gameObject, new Vector3(x, y, z), Quaternion.identity);
                    currentCounter += rubishType.value;
                    rubishInstances.Add(rubish);

                    // Instanciacion de los text de la UI
                    GameObject textoPuntos = GameObject.Instantiate(textPrefab);
                    textoPuntos.transform.SetParent(canvas.transform);
                    Text textoDelPrefab = textoPuntos.GetComponent<Text>();

                    // instanciamos el objeto y ya desde la esfera se le llama para la posicion y los puntos
                    rubish.GetComponentInChildren<Points>().puntosBasura = textoDelPrefab;
                    rubish.GetComponentInChildren<Rubish>().textoAsociado = textoPuntos;

                    valid = true;
                } else {
                    noLongerValid.Add(rubishType);
                }

            } while(!valid); 
        } 

        currentCounter = 0;
        rubishInstances = null;
        rubishInstances = new List<GameObject>();
    }

    // Update is called once per frame
    void Update() {
        
    }
}
