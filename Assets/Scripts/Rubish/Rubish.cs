using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubish : MonoBehaviour {
    public int value;

    public int ownership;

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.tag == "Wave") {
            Debug.Log("Hit by wave. Remove " + value + " to Player " + ownership);
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.transform.tag == "TruckGarbage") {
            Debug.Log("Hit by Truck");
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.transform.tag == "ChangeOwnership") {
            Debug.Log("Change ownership");
            OwnershipChange othersOwnership = other.gameObject.GetComponent<OwnershipChange>();
            ownership = othersOwnership.ownership;
            Debug.Log("New ownership: " + ownership);
            return;
        }
    }

    public void SetOwnership(int owner) {
        ownership = owner;
    }
}
