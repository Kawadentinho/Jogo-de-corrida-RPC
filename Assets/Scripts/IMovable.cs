using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovable
{
    void Move(float moveInput); // M�todo que lida com a movimenta��o
    void Rotate(float rotationInput); // M�todo que lida com a rota��o
}

