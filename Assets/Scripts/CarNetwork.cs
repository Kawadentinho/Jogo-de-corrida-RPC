using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNetwork : MonoBehaviourPun, IPunObservable
{


    private Vector3 networkPosition; // Posi��o sincronizada do carro
    private Quaternion networkRotation; // Rota��o sincronizada do carro


    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            // Se for o carro do jogador local, a posi��o e rota��o s�o controladas normalmente
            return;
        }
        else
        {
            // Se n�o for o carro do jogador local, interpola a posi��o e rota��o com base nos dados recebidos da rede
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, Time.deltaTime * 10);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Envia a posi��o e rota��o do carro para os outros jogadores
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Recebe a posi��o e rota��o do carro dos outros jogadores
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
