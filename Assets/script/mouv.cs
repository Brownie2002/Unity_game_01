using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouv : MonoBehaviour
{
    // Définition des variables modifiables dans l'éditeur Unity
    public float speed = 10f; // Vitesse de déplacement au sol
    public float jumpSpeed = 13f; // Vitesse initiale du saut
    public float gravity = 15f; // Force de gravité appliquée au joueur
    public float runningSpeed = 20f; // Vitesse de course
    public float crouchSpeed = 4f;

    [SerializeField]
    private float currentSpeed; // Vitesse actuelle, qui varie entre marche et course
    private Vector3 moveDirection = Vector3.zero; // Direction de déplacement actuelle
    private CharacterController characterController; // Référence au CharacterController attaché au joueur

    // Gestion du double saut
    private bool canDoubleJump; // Indique si le joueur peut effectuer un double saut
    [SerializeField]
    private int jumpCount = 0; // Compte le nombre de sauts effectués

    // Variable pour suivre l'état de course du joueur
    private bool isRunning = false;
    public bool crouch = false;
    void Start()
    {
        // Initialisation du CharacterController et de la vitesse de déplacement
        characterController = GetComponent<CharacterController>();
        currentSpeed = speed;
        // Position de départ du joueur (peut être ajustée selon les besoins du jeu)
        transform.position = new Vector3(0, 1, 0);
    }

    void Update()
    {
        // Réinitialiser le double saut et le compteur de sauts lorsque le joueur touche le sol
        if (characterController.isGrounded)
        {
            canDoubleJump = true;
            jumpCount = 0;
        }

        // Gestion du mouvement horizontal
        // Prend les entrées de l'axe horizontal et vertical et les transforme en vecteur de mouvement
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection); // S'assure que le mouvement est relatif à la direction du joueur
        moveDirection.x *= currentSpeed;
        moveDirection.z *= currentSpeed;

        // Gestion du saut
        if (Input.GetButtonDown("Jump"))
        {
            // Permettre le saut si le joueur est au sol ou effectuer un double saut si autorisé
            if (characterController.isGrounded || (canDoubleJump && jumpCount < 2))
            {
                moveDirection.y = jumpSpeed;
                if (!characterController.isGrounded)
                {
                    // Désactiver le double saut après l'avoir utilisé
                    canDoubleJump = false;
                }
                jumpCount++; // Incrémente le compteur de sauts
            }
        }

        // Appliquer la gravité au joueur
        moveDirection.y -= gravity * Time.deltaTime;
        // Effectuer le mouvement basé sur le vecteur de déplacement calculé
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