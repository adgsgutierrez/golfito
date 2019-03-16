using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControler : MonoBehaviour
{
    private Rigidbody rb;
    public const float MULTIPLICATE_VELOCITY = 20f; 
    public float speed;
    float moveHorizontal = 0.0f;
    float moveVertical = 0.0f;
    float multiplicateVelocity = 0;

    private float vida = 100f;
    private int puntos = 0;
    // Start is called before the first frame update
    int minute = 0;
    int second = 0;
    string minutes = "00";
    string seconds = "00";
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 8.0F;
    private Vector3 moveDirection = Vector3.zero;
    public Text tiempoView;
    public Text lifeView;
    float tiempo = 0.0f;
    private bool gravedad = false;
    private bool endGame = false;
    void Start(){
        rb = GetComponent<Rigidbody>();
        checkedVida();
    }

    void FixedUpdate(){
        if(!endGame){
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            Vector3 movimiento = new Vector3((moveHorizontal * -1 ) , 0.0f , (moveVertical * -1));
            rb.AddForce(movimiento * ( speed + multiplicateVelocity ));
        }else{
            Debug.Log("End Game");
        }
    }
    // Update is called once per frame
    void Update(){
        if(gravedad){
           transform.position =  new Vector3 (transform.position.x, Time.deltaTime, transform.position.z);
        }
        setTimer();
    }
    void setTimer(){
            tiempo = tiempo + (1 * Time.deltaTime);
            minute = (int)(tiempo / 60);
            second = (int)(tiempo % 60);
            minutes = (minute < 10)?"0"+minute : ""+minute ;
            seconds = (second < 10)?"0"+second : ""+second ;
            tiempoView.text = "" + minutes + ":"+seconds;
    }
    void OnTriggerEnter(Collider other){
        Debug.Log("Colision");
        if(other.gameObject.CompareTag("objectTramp")){
            menosVida();
       }else if(other.gameObject.CompareTag("miniorPoints")){
            menosPuntos();
            other.gameObject.SetActive(false);
       }else if(other.gameObject.CompareTag("morePoints")){
            masPuntos();
            other.gameObject.SetActive(false);
       }else if(other.gameObject.CompareTag("watterTramp")){
            speed = 0;
       }else if(other.gameObject.CompareTag("gemaGravedad")){
           gravedad = true;
       }else if(other.gameObject.CompareTag("lodo")){
           speed = 1;
       }
    }

    public void menosVida(){
        vida = vida - 10;
        checkedVida();
    }
    public void masPuntos(){
        puntos = puntos + 15;
        checkedVida();
    }

    public void menosPuntos(){
        puntos = puntos - 10;
        speed = 7;
        checkedVida();
    }

    public void checkedVida(){
        Debug.Log("vida "+vida);
        Debug.Log("puntos "+puntos);
        if(vida <= 0){
            endGame = true;
        }
        string life = "";
        for(int x = 0 ; x < vida ; x ++){
            life = life + "|";
        }
        lifeView.text = life;
    }
}
