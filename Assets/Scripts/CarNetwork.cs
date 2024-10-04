using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNetwork : MonoBehaviourPun, IPunObservable
{


    private Vector3 networkPosition; // Posição sincronizada do carro
    private Quaternion networkRotation; // Rotação sincronizada do carro


    // Update is called once per frame
    void Update()
    {

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
