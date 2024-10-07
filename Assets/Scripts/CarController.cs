using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour, IMovable
{

    public float speed = 10f; // Velocidade de movimenta��o do carro
    public float rotationSpeed = 100f; // Velocidade de rota��o do carro
    private Rigidbody2D rb; // Componente Rigidbody2D para f�sica do carro




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

        // Movimenta e rotaciona o carro usando os m�todos da interface
        Move(moveInput);
        Rotate(rotationInput);
    }

    // Implementa��o do m�todo Move da interface IMovable
    public void Move(float moveInput)
    {
        rb.velocity = transform.up * moveInput * speed; // Movimenta o carro
    }

    // Implementa��o do m�todo Rotate da interface IMovable
    public void Rotate(float rotationInput)
    {
        rb.rotation -= rotationInput * rotationSpeed * Time.deltaTime; // Rotaciona o carro
    }
}