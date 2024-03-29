﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour2 : MonoBehaviour{
    public int speed = 5;
    private Rigidbody rigidBody;
    private Transform parentTransform;
    private Animator animator;
    // Start is called before the first frame update

    private int facing = 0; // 0 up 1 right 2 down 3 left
    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        rigidBody = transform.parent.GetComponent<Rigidbody>();
        parentTransform = transform.parent.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        float x = 0; 
        float z = 0;
        
        if (Input.GetKey(KeyCode.UpArrow)){
            z = 1;
            facing = 0;
        }

        if(Input.GetKey(KeyCode.DownArrow)){
            z = -1;
            facing = 2;
        }
        
        if(Input.GetKey(KeyCode.RightArrow)){
            x = 1;
            facing = 1;
        }

        if(Input.GetKey(KeyCode.LeftArrow)){
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
