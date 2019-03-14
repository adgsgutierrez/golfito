using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    private Rigidbody rb;
    public const float MULTIPLICATE_VELOCITY = 20f; 
    public float speed;
    float moveHorizontal = 0.0f;
    float moveVertical = 0.0f;
    float multiplicateVelocity = 0;
    // Start is called before the first frame update
    
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 8.0F;
    private Vector3 moveDirection = Vector3.zero;
    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        // if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.D) ){
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            Vector3 movimiento = new Vector3((moveHorizontal * -1 ) , 0.0f , (moveVertical * -1));
            rb.AddForce(movimiento * ( speed + multiplicateVelocity ));
            
            //Rotate Player
            
        // }
    }
    // Update is called once per frame
    void Update(){
        
    }

    void OnTriggerEnter(Collider other){

    }
}
