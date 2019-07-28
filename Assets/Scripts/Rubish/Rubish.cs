using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubish : MonoBehaviour {
    public GameObject textoAsociado;
    public int value;
    public int ownership;

    private GameManager gameManager;

    void Awake() {
        // if (GameManager.instance == null)
        //         Instantiate(gameManager);
        
        gameManager = GameManager.Instance;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.tag == "Wave") {
            Debug.Log("Hit by wave. Remove " + value + " to Player " + ownership);
            gameManager.DecreaseHP(ownership, value);
            Destroy(textoAsociado);
            Destroy(gameObject);
            return;
        }

        if (other.gameObject.transform.tag == "TruckGarbage") {
            gameManager.IncreaseHP(ownership, value/2);
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

    public void Update() {
        if (transform.position.y < -2) {
            Debug.Log("Destroy Object");
            Destroy(gameObject);
        }
    }
};