using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;
    bool rotate = false;
    float speed = 10.0f;
    float rotateXMayor = 0;
    float rotateXMinor = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Devuelve la posicion con transform
        offset = transform.position - player.transform.position;
    }

    void LateUpdate(){
        //se mueve el objeto camara siguiendo al jugador
         transform.position = player.transform.position + offset;
         if(rotate){
            transform.Rotate(0, (-1 * rotateXMayor), 0);
            transform.Rotate(0, ( rotateXMinor), 0);

            rotate = false;
         }

         if(Input.GetKey(KeyCode.T)){
            rotateXMayor = rotateXMayor + 1;
            transform.Rotate(0, 1f, 0);
            transform.position = new Vector3( 
                player.transform.position.x,
                transform.position.y,
                player.transform.position.z
                );
                rotate = true;
         }
         if(Input.GetKey(KeyCode.R)){
             rotateXMinor = rotateXMinor +1;
            transform.Rotate(0, -1f, 0);
            transform.position = new Vector3( 
                player.transform.position.x,
                transform.position.y,
                player.transform.position.z
                );
            rotate = true;
         }
    }

    // Update is called once per frame
    void Update(){
       
    }
}

