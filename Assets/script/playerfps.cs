using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerfps : MonoBehaviour
{
    //Camera
    public Camera playerCamera;

    CharacterController characterController;

    //rotation Cam
    float rotationX = 0;
    public float rotationSpeed = 2.0f;
    public float rotationXLimit = 45.0f;


    void Start()
    {
        //cache le curseur de la souris
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotation de la caméra

        //Input.GetAxis("Mouse Y") = mouvement de la souris haut/bas
        //On est en 3D donc applique ("Mouse Y") sur l'axe de rotation X 
        rotationX += -Input.GetAxis("Mouse Y") * rotationSpeed;

        //La rotation haut/bas de la caméra est comprise entre -45 et 45 
        //Mathf.Clamp permet de limiter une valeur
        //On limite rotationX, entre -rotationXLimit et rotationXLimit (-45 et 45)
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);


        //Applique la rotation haut/bas sur la caméra
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);


        //Input.GetAxis("Mouse X") = mouvement de la souris gauche/droite
        //Applique la rotation gauche/droite sur le Player
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);
    }
}