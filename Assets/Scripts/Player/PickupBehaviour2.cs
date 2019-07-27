﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour2 : MonoBehaviour{
    private bool isHoldingObject = false;
    private bool hasCollided = false;
    private GameObject collision;
    private GameObject objeto = null;
    private Vector3 size;

    private float throwAngle = 45.0f;
    private float gravity = 9.81f;
    private Rigidbody rigidBody;
    private bool ready = false;
    private bool horizontal = false;
    private bool vertical = false;
    private int dirHorizontal = 1; //1 derecha -1 izquierda
    private int dirVertical = 0; //1 arriba -1 abajo

    private float initialPress;
    private float finalRelease;
    private float maximunValue = 1.0f; //Tiempo necesario para conseguir el mayor lanzamiento 
    // Start is called before the first frame update
    void Start(){
        size = GetComponent<Collider>().bounds.size;
        rigidBody = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update(){
        if(rigidBody.velocity.x != 0 && rigidBody.velocity.z != 0){
            vertical = true;
            
            if(rigidBody.velocity.x > 0){
                dirHorizontal = 1;
            }
            else{
                dirHorizontal = -1;
            }

            if(rigidBody.velocity.z > 0){
                dirVertical = 1;
            }
            else{
                dirVertical = -1;
            }

        }
        else if(rigidBody.velocity.z != 0){
            vertical = true;
            horizontal = false;
            dirHorizontal = 0; 

            if(rigidBody.velocity.z > 0){
                dirVertical = 1;
            }
            else{
                dirVertical = -1;
            }
        }
        else if(rigidBody.velocity.x != 0){
            horizontal = true;
            vertical = false;

            if(rigidBody.velocity.x > 0){
                dirHorizontal = 1;
            }
            else{
                dirHorizontal = -1;
            }

            dirVertical = 0;
        }

        if(Input.GetKeyDown(KeyCode.KeypadPeriod) || Input.GetKeyDown(KeyCode.M)){
            if(hasCollided && !isHoldingObject){
                collision.transform.position = new Vector3(this.transform.position.x,size.y,this.transform.position.z);
                isHoldingObject = true;
                objeto = Instantiate(collision,this.transform);
                Destroy(collision);
            }
            else if(isHoldingObject){
                initialPress = Time.time;
                ready = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.KeypadPeriod) || Input.GetKeyDown(KeyCode.M)){
            if(isHoldingObject && ready){
                finalRelease = Time.time;
                ready = false;
                StartCoroutine(throwObject());
            }
        }
    }

    void OnTriggerEnter(Collider collision){
        string collidedObject = collision.gameObject.name;
        if (collidedObject.Contains("Rubbish")){
            hasCollided = true;
            this.collision = collision.gameObject;
        }
    }

    void OnTriggerExit(Collider collision){
        string collidedObject = collision.gameObject.name;
        if (collidedObject.Contains("Rubbish")){
            hasCollided = false;
            isHoldingObject = false;
        }
    }

    IEnumerator throwObject(){
        int h = horizontal ? 1 : 0;
        int v = vertical ? 1 : 0;
        float dist = 0;

        if(finalRelease - initialPress >= maximunValue){
            dist = 10;
        }
        else{
            dist = 5 + (finalRelease - initialPress);
        }

        Debug.Log(dist);

        Vector3 target = new Vector3(this.transform.position.x,0.5f,this.transform.position.z);
        target.x += dist * h * dirHorizontal;
        target.z += dist * v * dirVertical;
    
        isHoldingObject = false;
        objeto.transform.SetParent(null);
        hasCollided = false;

        float throwDistance = Vector3.Distance(objeto.transform.position,target);
        float throwVelocity = throwDistance / (Mathf.Sin(2 * throwAngle * Mathf.Deg2Rad) / gravity);
    
        float Vx = Mathf.Sqrt(throwVelocity) * Mathf.Cos(throwAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(throwVelocity) * Mathf.Sin(throwAngle * Mathf.Deg2Rad);
    
        float flightDuration = throwDistance / Vx;

        objeto.transform.rotation = Quaternion.LookRotation(target - objeto.transform.position);

        float elapse_time = 0;
 
        while (elapse_time < flightDuration){
            objeto.transform.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);
           
            elapse_time += Time.deltaTime;
 
            yield return null;
        }
        objeto.transform.rotation = new Quaternion(0,0,0,1);
    }
}
