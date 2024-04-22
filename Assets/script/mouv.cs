using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouv : MonoBehaviour
{
    // D�finition des variables modifiables dans l'�diteur Unity
    public float speed = 10f; // Vitesse de d�placement au sol
    public float jumpSpeed = 13f; // Vitesse initiale du saut
    public float gravity = 15f; // Force de gravit� appliqu�e au joueur
    public float runningSpeed = 20f; // Vitesse de course
    public float crouchSpeed = 4f;

    [SerializeField]
    private float currentSpeed; // Vitesse actuelle, qui varie entre marche et course
    private Vector3 moveDirection = Vector3.zero; // Direction de d�placement actuelle
    private CharacterController characterController; // R�f�rence au CharacterController attach� au joueur

    // Gestion du double saut
    private bool canDoubleJump; // Indique si le joueur peut effectuer un double saut
    [SerializeField]
    private int jumpCount = 0; // Compte le nombre de sauts effectu�s

    // Variable pour suivre l'�tat de course du joueur
    private bool isRunning = false;
    public bool crouch = false;
    void Start()
    {
        // Initialisation du CharacterController et de la vitesse de d�placement
        characterController = GetComponent<CharacterController>();
        currentSpeed = speed;
        // Position de d�part du joueur (peut �tre ajust�e selon les besoins du jeu)
        transform.position = new Vector3(0, 1, 0);
    }

    void Update()
    {
        // R�initialiser le double saut et le compteur de sauts lorsque le joueur touche le sol
        if (characterController.isGrounded)
        {
            canDoubleJump = true;
            jumpCount = 0;
        }

        // Gestion du mouvement horizontal
        // Prend les entr�es de l'axe horizontal et vertical et les transforme en vecteur de mouvement
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection); // S'assure que le mouvement est relatif � la direction du joueur
        moveDirection.x *= currentSpeed;
        moveDirection.z *= currentSpeed;

        // Gestion du saut
        if (Input.GetButtonDown("Jump"))
        {
            // Permettre le saut si le joueur est au sol ou effectuer un double saut si autoris�
            if (characterController.isGrounded || (canDoubleJump && jumpCount < 2))
            {
                moveDirection.y = jumpSpeed;
                if (!characterController.isGrounded)
                {
                    // D�sactiver le double saut apr�s l'avoir utilis�
                    canDoubleJump = false;
                }
                jumpCount++; // Incr�mente le compteur de sauts
            }
        }

        // Appliquer la gravit� au joueur
        moveDirection.y -= gravity * Time.deltaTime;
        // Effectuer le mouvement bas� sur le vecteur de d�placement calcul�
        characterController.Move(moveDirection * Time.deltaTime);

        // Gestion de la course
        isRunning = Input.GetKey(KeyCode.LeftShift);
        // Ajuste la vitesse actuelle en fonction de si le joueur court ou marche
        currentSpeed = isRunning ? runningSpeed : speed;

        crouch = Input.GetKey(KeyCode.LeftControl);
        
        if (crouch == true)
        {
            currentSpeed = crouchSpeed;
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        }
        else 
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        


    }
}