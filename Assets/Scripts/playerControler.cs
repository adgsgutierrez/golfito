using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerControler : MonoBehaviour
{
    private Rigidbody rb;
    public const float MULTIPLICATE_VELOCITY = 20f; 
    public float speed;
    private Vector3 posicion;
    float moveHorizontal = 0.0f;
    float moveVertical = 0.0f;
    float multiplicateVelocity = 0;
    public ParticleSystem sistemaParticulasDiamantes;
    public ParticleSystem sistemaParticulasGolpe;
    public AudioSource[] sounds;
    private AudioSource audioRecoleccion;
    private AudioSource audioGolpe;
    private float vida = 50f;
    private int puntos = 0;

    public int next;
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
    public Text points;
    float tiempo = 0.0f;
    private bool gravedad = false;
    private bool endGame = false;

    
    void Start(){
        rb = GetComponent<Rigidbody>();
        sounds  = GetComponents<AudioSource>();
        audioRecoleccion = sounds[1];
        audioGolpe = sounds[0];   
        audioRecoleccion.Stop();
        audioGolpe.Stop();
        sistemaParticulasDiamantes.Stop();
        sistemaParticulasGolpe.Stop();
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
            audioGolpe.Play();
            posicion = transform.position;
            sistemaParticulasGolpe.transform.position = posicion ;
            sistemaParticulasGolpe.Play();
            menosVida();
       }else if(other.gameObject.CompareTag("miniorPoints")){
            audioGolpe.Play();
            posicion = transform.position;
            sistemaParticulasGolpe.transform.position = posicion ;
            sistemaParticulasGolpe.Play();
            menosPuntos();
            other.gameObject.SetActive(false);
       }else if(other.gameObject.CompareTag("morePoints")){
            audioRecoleccion.Play();
            masPuntos();
            posicion = other.gameObject.transform.position;
            sistemaParticulasDiamantes.transform.position = posicion ;
            sistemaParticulasDiamantes.Play();
            other.gameObject.SetActive(false);
       }else if(other.gameObject.CompareTag("watterTramp")){
            speed = 0;
       }else if(other.gameObject.CompareTag("gemaGravedad")){
           gravedad = true;
       }else if(other.gameObject.CompareTag("lodo")){
           speed = 1;
       }else if(other.gameObject.CompareTag("endScene")){
           SceneManager.LoadScene(next);
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
        puntos = (puntos>0)?(puntos - 10):0;
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
        points.text = "Puntos: " + puntos;
    }
}
