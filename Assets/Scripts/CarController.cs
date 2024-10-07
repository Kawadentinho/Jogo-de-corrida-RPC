using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour, IMovable
{

    public float speed = 10f; // Velocidade de movimentação do carro
    public float rotationSpeed = 100f; // Velocidade de rotação do carro
    private Rigidbody2D rb; // Componente Rigidbody2D para física do carro




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

        // Movimenta e rotaciona o carro usando os métodos da interface
        Move(moveInput);
        Rotate(rotationInput);
    }

    // Implementação do método Move da interface IMovable
    public void Move(float moveInput)
    {
        rb.velocity = transform.up * moveInput * speed; // Movimenta o carro
    }

    // Implementação do método Rotate da interface IMovable
    public void Rotate(float rotationInput)
    {
        rb.rotation -= rotationInput * rotationSpeed * Time.deltaTime; // Rotaciona o carro
    }
}