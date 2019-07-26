using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubishInstancer : MonoBehaviour
{
    public InstanceArea area;
    public int instances;
    public Rubish[] rubishTypes;

    private List<GameObject> rubishInstances;

    RubishInstancer() {
        rubishInstances = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < instances; i++) {
            int idx = Random.Range(0, rubishTypes.Length);
            Rubish rubishType = rubishTypes[idx];

            int x = Random.Range(area.minX, area.maxX);
            int y = Random.Range(area.minY, area.maxY);
            int z = Random.Range(area.minZ, area.maxZ);

            GameObject rubish = Instantiate(rubishType.prefab, new Vector3(x, y, z), Quaternion.identity);
            rubishInstances.Add(rubish);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
