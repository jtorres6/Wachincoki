using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public int speed = 5;
    private Rigidbody rigidBody;
    private Transform parentTransform;
    // Start is called before the first frame update

    private int facing = 0; // 0 up 1 right 2 down 3 left
    void Start(){
        rigidBody = transform.parent.GetComponent<Rigidbody>();
        parentTransform = transform.parent.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update(){
        float x = 0; 
        float z = 0;

        if(Input.GetKey(KeyCode.W)){
            z = 1;
            facing = 0;
        }

        if(Input.GetKey(KeyCode.S)){
            z = -1;
            facing = 2;
        }
        
        if(Input.GetKey(KeyCode.D)){
            x = 1;
            facing = 1;
        }

        if(Input.GetKey(KeyCode.A)){
            x = -1;
            facing = 3;
        }

        Vector3 look;

        if (facing == 2) {  // up
            look = new Vector3(0.0f, 0.0f, 0.0f);
            parentTransform.rotation = Quaternion.Euler(look);
        }
        else if (facing == 1) {  // right
            look = new Vector3(0.0f, 270.0f, 0.0f);
            parentTransform.rotation = Quaternion.Euler(look);
        }
        else if (facing == 0) {  // down
            look = new Vector3(0.0f, 180.0f, 0.0f);
            parentTransform.rotation = Quaternion.Euler(look);
        }
        else if (facing == 3) {  // left
            look = new Vector3(0.0f, 90.0f, 0.0f);
            parentTransform.rotation = Quaternion.Euler(look);
        }
        
        rigidBody.velocity = new Vector3(x * speed, rigidBody.velocity.y , z * speed);
    }
}
