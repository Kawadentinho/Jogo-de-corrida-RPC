using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CarNetwork : MonoBehaviourPun
{
private Vector3 networkPosition; // Posição sincronizada do carro
    private Quaternion networkRotation; // Rotação sincronizada do carro

    void Update()
    {
        if (!photonView.IsMine)
        {
            // Interpola a posição e rotação com base nos dados recebidos da rede
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Envia a posição e rotação do carro para os outros jogadores
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Recebe a posição e rotação do carro dos outros jogadores
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
