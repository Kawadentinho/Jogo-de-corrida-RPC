using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarController : MonoBehaviourPun, IMovable
{

    public float speed = 10f; // Velocidade máxima do carro
    public float acceleration = 5f; // Taxa de aceleração
    public float deceleration = 5f; // Taxa de desaceleração
    public float rotationSpeed = 100f; // Velocidade de rotação do carro
    private Rigidbody2D rb; // Componente Rigidbody2D para a física do carro

    private float currentSpeed = 0f; // Velocidade atual do carro
    private Vector3 networkPosition; // Posição sincronizada do carro na rede
    private Quaternion networkRotation; // Rotação sincronizada na rede

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Inicializa o Rigidbody2D
    }

    void Update()
    {
        if (photonView.IsMine) // Somente o carro local controla seu movimento
        {
            // Lê os inputs para acelerar e rotacionar
            float moveInput = Input.GetAxis("Vertical"); // Input de aceleração (frente/trás)
            float rotationInput = Input.GetAxis("Horizontal"); // Input de rotação (esquerda/direita)

            // Movimenta e rotaciona o carro
            Move(moveInput);
            Rotate(rotationInput);

            // Envia a posição e rotação atual via RPC para sincronizar
            photonView.RPC("UpdateCarState", RpcTarget.Others, transform.position, transform.rotation, currentSpeed);
        }
        else
        {
            // Se não for o carro local, interpola para suavizar o movimento
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }

    // Implementação do método Move da interface IMovable
    public void Move(float moveInput)
    {
        if (moveInput > 0)
        {
            // Aumenta a velocidade atual de acordo com a aceleração
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, speed);
        }
        else if (moveInput < 0)
        {
            // Desacelera ou reverte a direção
            currentSpeed = Mathf.Max(currentSpeed - acceleration * Time.deltaTime, -speed);
        }
        else
        {
            // Desacelera naturalmente quando não há input
            if (currentSpeed > 0)
            {
                currentSpeed = Mathf.Max(currentSpeed - deceleration * Time.deltaTime, 0);
            }
            else if (currentSpeed < 0)
            {
                currentSpeed = Mathf.Min(currentSpeed + deceleration * Time.deltaTime, 0);
            }
        }

        // Movimenta o carro com base na velocidade atual
        rb.velocity = transform.up * currentSpeed;
    }

    // Implementação do método Rotate da interface IMovable
    public void Rotate(float rotationInput)
    {
        rb.rotation -= rotationInput * rotationSpeed * Time.deltaTime; // Rotaciona o carro
    }

    // Método RPC para sincronizar posição, rotação e velocidade
    [PunRPC]
    public void UpdateCarState(Vector3 position, Quaternion rotation, float speed)
    {
        networkPosition = position;
        networkRotation = rotation;
        currentSpeed = speed;
    }
}