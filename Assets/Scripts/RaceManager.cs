using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RaceManager : MonoBehaviourPunCallbacks
{

    public int totalLaps = 3; // Número total de voltas da corrida
    public Text lapText; // UI que exibe a volta atual
    public Text finishText; // UI que exibe a mensagem de final de corrida
    public Transform finishLine; // Referência à linha de chegada

    private int playerLap = 0; // Contador de voltas do jogador

    void Update()
    {
        // Checa se o jogador cruzou a linha de chegada
        if (Vector2.Distance(transform.position, finishLine.position) < 1f)
        {
            if (playerLap < totalLaps)
            {
                playerLap++; // Aumenta a contagem de voltas
                lapText.text = "Lap: " + playerLap + "/" + totalLaps; // Atualiza o texto de voltas
            }
            else
            {
                finishText.text = "Race Finished!"; // Exibe a mensagem de fim da corrida
                PhotonNetwork.LeaveRoom(); // Sai da sala online
            }
        }
    }
}