using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubish : MonoBehaviour {
    public GameObject prefab;
    public int value;

    void OnTriggerEnter(Collider other) {
        Debug.Log("Hola");
        Debug.Log("Hit by " + other.gameObject.transform.tag);
        if (other.gameObject.transform.tag == "Wave") {
            Debug.Log("Hit by wave");
            Destroy(prefab);
        }
    }
}
