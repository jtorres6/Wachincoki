using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour2 : MonoBehaviour{
    private int speed = 5;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start(){
        rigidBody = transform.parent.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update(){
        float x = 0; 
        float z = 0;

        if(Input.GetKey(KeyCode.UpArrow)){
            z = 1;
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            z = -1;
        }
        
        if(Input.GetKey(KeyCode.RightArrow)){
            x = 1;
        }

        if(Input.GetKey(KeyCode.LeftArrow)){
            x = -1;
        }
        
        rigidBody.velocity = new Vector3(x * speed, 0, z * speed);
    }
}
