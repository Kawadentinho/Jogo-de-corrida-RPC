using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{


    public float speed = 10f; // Velocidade de movimenta��o do carro
    public float rotationSpeed = 100f; // Velocidade de rota��o do carro

    private Rigidbody2D rb; // Componente Rigidbody2D para f�sica do carro



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicializa o Rigidbody2D
    }


    // Update is called once per frame
    void Update()
    {
        // L� os inputs para acelerar e rotacionar
        float moveInput = Input.GetAxis("Vertical"); // Input de acelera��o (frente/tr�s)
        float rotationInput = Input.GetAxis("Horizontal"); // Input de rota��o (esquerda/direita)

        // Movimenta o carro para frente ou para tr�s
        rb.velocity = transform.up * moveInput * speed;

        // Rotaciona o carro com base no input horizontal
        rb.rotation -= rotationInput * rotationSpeed * Time.deltaTime;
    }
}


