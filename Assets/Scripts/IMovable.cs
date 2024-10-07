using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void Move(float moveInput); // Método que lida com a movimentação
    void Rotate(float rotationInput); // Método que lida com a rotação
}

