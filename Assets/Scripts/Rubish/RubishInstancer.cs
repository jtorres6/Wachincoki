using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubishInstancer : MonoBehaviour
{
    const float y_offset = 0.5f;

    public InstanceArea[] areas;
    public Rubish[] rubishTypes;
    public int maxCounter;

    private List<GameObject> rubishInstances;
    private int currentCounter;

    RubishInstancer() {
        rubishInstances = new List<GameObject>();
        currentCounter = 0;
    }

    // Start is called before the first frame update
    void Start() {
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
                float y = Random.Range(insarea.minY, insarea.maxY) + y_offset;
                float z = Random.Range(insarea.minZ, insarea.maxZ);

                if (currentCounter + rubishType.value <= maxCounter) {
                    GameObject rubish = Instantiate(rubishType.gameObject, new Vector3(x, y, z), Quaternion.identity);
                    currentCounter += rubishType.value;
                    rubishInstances.Add(rubish);
                    valid = true;
                } else {
                    noLongerValid.Add(rubishType);
                }

            } while(!valid); 
        }                  
    }

    // Update is called once per frame
    void Update() {
        
    }
}
