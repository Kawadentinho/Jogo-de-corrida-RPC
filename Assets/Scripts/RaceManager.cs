using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class RaceManager : MonoBehaviourPunCallbacks
{

    public int totalLaps = 3; // N�mero total de voltas da corrida
    public Text lapText; // UI que exibe a volta atual
    public Text finishText; // UI que exibe a mensagem de fim da corrida
    public Transform finishLine; // Refer�ncia � linha de chegada

    private int playerLap = 0; // Contador de voltas do jogador
    private bool raceFinished = false; // Flag para verificar se a corrida terminou

    void Start()
    {
        // Inicializa a UI
        lapText.text = "Lap: 0/" + totalLaps;
        finishText.text = ""; // Limpa o texto de fim da corrida no in�cio
    }

    // M�todo chamado quando o jogador entra no trigger da linha de chegada
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o jogador cruzou a linha de chegada
        if (collision.CompareTag("Player") && !raceFinished) // Verifica se o jogador n�o terminou a corrida
        {
            TrackLap(); // Aumenta o contador de voltas

            if (HasFinishedRace())
            {
                FinishRace(); // Termina a corrida
            }
        }
    }

    // Implementa��o do m�todo TrackLap da interface ITrackable
    public void TrackLap()
    {
        if (playerLap < totalLaps)
        {
            playerLap++; // Aumenta a contagem de voltas
            lapText.text = "Lap: " + playerLap + "/" + totalLaps; // Atualiza o texto de voltas
        }
    }

    // Implementa��o do m�todo HasFinishedRace da interface ITrackable
    public bool HasFinishedRace()
    {
        return playerLap >= totalLaps; // Retorna se o jogador terminou a corrida
    }

    // M�todo para finalizar a corrida
    private void FinishRace()
    {
        raceFinished = true; // Define que a corrida foi finalizada
        finishText.text = "Race Finished!"; // Exibe a mensagem de fim de corrida
        PhotonNetwork.LeaveRoom(); // Sai da sala online
    }
}