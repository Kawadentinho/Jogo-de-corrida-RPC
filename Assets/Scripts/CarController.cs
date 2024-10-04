using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{


    public float speed = 10f; // Velocidade de movimentação do carro
    public float rotationSpeed = 100f; // Velocidade de rotação do carro

    private Rigidbody2D rb; // Componente Rigidbody2D para física do carro



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicializa o Rigidbody2D
    }


    // Update is called once per frame
    void Update()
    {
        // Lê os inputs para acelerar e rotacionar
        float moveInput = Input.GetAxis("Vertical"); // Input de aceleração (frente/trás)
        float rotationInput = Input.GetAxis("Horizontal"); // Input de rotação (esquerda/direita)

        // Movimenta o carro para frente ou para trás
        rb.velocity = transform.up * moveInput * speed;

        // Rotaciona o carro com base no input horizontal
        rb.rotation -= rotationInput * rotationSpeed * Time.deltaTime;
    }
}


