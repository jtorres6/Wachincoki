using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupBehaviour2 : MonoBehaviour{
    private bool isHoldingObject = false;
    private bool hasCollided = false;
    private GameObject collision;
    private GameObject objeto = null;
    private Vector3 size;

    private Rigidbody rigidBody;
    private bool ready = false;
    private bool horizontal = false;
    private bool vertical = false;
    private int dirHorizontal = 1; //1 derecha -1 izquierda
    private int dirVertical = 0; //1 arriba -1 abajo
    private float threeshold = 0.3f;

    private float initialPress;
    private float finalRelease;
    private float maximunValue = 1.0f; //Tiempo necesario para conseguir el mayor lanzamiento 
    // Start is called before the first frame update
    void Start(){
        size = GetComponent<Collider>().bounds.size;
        rigidBody = transform.parent.GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update(){
        if((rigidBody.velocity.x > threeshold || rigidBody.velocity.x < -threeshold) && (rigidBody.velocity.z > threeshold || rigidBody.velocity.z < -threeshold)){
            vertical = true;
            
            if(rigidBody.velocity.x > threeshold){
                dirHorizontal = 1;
            }
            else if(rigidBody.velocity.x < -threeshold){
                dirHorizontal = -1;
            }
            else{
                dirHorizontal = 0;
            }

            if(rigidBody.velocity.z > threeshold){
                dirVertical = 1;
            }
            else if(rigidBody.velocity.z < -threeshold){
                dirVertical = -1;
            }
            else{
                dirVertical = 0;
            }

        }
        else if((rigidBody.velocity.z > threeshold || rigidBody.velocity.z < -threeshold)){
            vertical = true;
            horizontal = false;
            dirHorizontal = 0; 

            if(rigidBody.velocity.z > threeshold){
                dirVertical = 1;
            }
            else if(rigidBody.velocity.z < -threeshold){
                dirVertical = -1;
            }
            else{
                dirVertical = 0;
            }
        }
        else if((rigidBody.velocity.x > threeshold || rigidBody.velocity.x < -threeshold)){
            horizontal = true;
            vertical = false;

            if(rigidBody.velocity.x > threeshold){
                dirHorizontal = 1;
            }
            else if(rigidBody.velocity.x < -threeshold){
                dirHorizontal = -1;
            }
            else{
                dirHorizontal = 0;
            }

            dirVertical = 0;
        }


        if(Input.GetKeyDown(KeyCode.KeypadPeriod) || Input.GetKeyUp(KeyCode.M)){
            if(hasCollided && !isHoldingObject){
                isHoldingObject = true;
                objeto = Instantiate(collision, new Vector3(this.transform.position.x, this.transform.position.y+2, this.transform.position.z), Quaternion.identity);
                objeto.transform.SetParent(this.transform);
                Rigidbody body = objeto.GetComponent<Rigidbody>();
                body.useGravity = false;
                body.isKinematic = true;
                body.detectCollisions = false;
                Destroy(collision);
            }
            else if(isHoldingObject){
                initialPress = Time.time;
                ready = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.KeypadPeriod) || Input.GetKeyUp(KeyCode.M)){
            if(isHoldingObject && ready){
                finalRelease = Time.time;
                ready = false;
                throwObject();
            }
        }
    }

    void OnTriggerEnter(Collider collision){
        if (collision.gameObject.tag == "Rubbish"){
            hasCollided = true;
            this.collision = collision.gameObject;
        }
    }

    void OnTriggerExit(Collider collision){
        if (collision.gameObject.tag == "Rubbish"){
            hasCollided = false;
        }
    }

    void throwObject(){
        int h = horizontal ? 1 : 0;
        int v = vertical ? 1 : 0;
        float dist = 0;

        if(finalRelease - initialPress >= maximunValue){
            dist = 12;
        }
        else{
            dist = 8 + (finalRelease - initialPress);
        }

        Vector3 target = new Vector3(this.transform.position.x,0.5f,this.transform.position.z);
        target.x += dist * h * dirHorizontal;
        target.z += dist * v * dirVertical;
    
        objeto.transform.SetParent(null);
        Rigidbody body = objeto.GetComponent<Rigidbody>();
        body.useGravity = true;
        body.isKinematic = false;
        body.detectCollisions = true;
        
        Vector3 throwDirection = target - objeto.transform.position;
        float X0 = 0;
        float Y0 = 0;
        float Z0 = 0;

        float time = 2.0f;

        float Vx = (throwDirection.x - X0) / time;
        float Vz = (throwDirection.z - Z0) / time;
        float Vy = (throwDirection.y - Y0 + (0.5f*Mathf.Abs(Physics.gravity.magnitude) * Mathf.Pow(time,2))) / time;

        Vx -= body.velocity.x;
        Vy -= body.velocity.y;
        Vz -= body.velocity.z;

        body.AddForce(Vector3.right * Vx, ForceMode.VelocityChange);
        body.AddForce(Vector3.up * Vy, ForceMode.VelocityChange);
        body.AddForce(Vector3.forward * Vz, ForceMode.VelocityChange);

        objeto.transform.rotation = new Quaternion(0,0,0,1);
        isHoldingObject = false;
        hasCollided = false;
    }
}
