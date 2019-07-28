using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour {
    private bool stopped;
    private Rigidbody rigidBody;
    private float vX, vZ;

    public int speed = 5;

    public Vector3[] positions;
    public Vector3[] scales;
    public Vector3[] velocity;

    Truck() {
        stopped = false;
    }

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        int idx = Random.Range(0, positions.Length);

        transform.position = positions[idx];
        transform.localScale = scales[idx];
        vX = velocity[idx].x;
        vZ = velocity[idx].z;
    }

    // Update is called once per frame
    void Update() {
        if (!stopped) {
            rigidBody.velocity = new Vector3(vX * speed, 0, vZ * speed);
        }
    }

    IEnumerator OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject.transform.tag);

        if (other.gameObject.transform.tag == "Reset") {
            yield return StopForSeconds(5);

            int idx = Random.Range(0, positions.Length);
            transform.position = positions[idx];
            transform.localScale = scales[idx];
            vX = velocity[idx].x;
            vZ = velocity[idx].z;
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
        yield return new WaitForSeconds(0.2f);
        stopped = false;
    }
}
