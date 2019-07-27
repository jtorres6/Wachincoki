using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour {
    private bool stopped;
    private Rigidbody rigidBody;

    public int speed = 5;
    public float originalX;
    public float originalY;
    public float originalZ;

    Truck() {
        stopped = false;
    }

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();

        transform.position = new Vector3(originalX, originalY, originalZ);
    }

    // Update is called once per frame
    void Update() {
        float x = -1;
        float z = 0;

        if (!stopped) {
            rigidBody.velocity = new Vector3(x * speed, 0, z * speed);
        }
    }

    IEnumerator OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.transform.tag);

        if (other.gameObject.transform.tag == "Reset") {
            yield return StopForSeconds(5);
            transform.position = new Vector3(originalX, originalY, originalZ);
        }

        if (other.gameObject.transform.tag == "StopArea") {
            yield return StopForSeconds(2);
        }
    }

    void OnTriggerStay(Collider other) {
        Debug.Log(other.gameObject.transform.tag);
    }

    IEnumerator StopForSeconds(int seconds) {
        stopped = true;
        rigidBody.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(seconds);
        stopped = false;
    }
}
