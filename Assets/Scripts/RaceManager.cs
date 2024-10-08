using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class RaceManager : MonoBehaviourPunCallbacks
{

    public int totalLaps = 3; // Número total de voltas da corrida
    public Text lapText; // UI que exibe a volta atual
    public Text finishText; // UI que exibe a mensagem de fim da corrida
    public Transform finishLine; // Referência à linha de chegada

    private int playerLap = 0; // Contador de voltas do jogador
    private bool raceFinished = false; // Flag para verificar se a corrida terminou

    void Start()
    {
        // Inicializa a UI
        lapText.text = "Lap: 0/" + totalLaps;
        finishText.text = ""; // Limpa o texto de fim da corrida no início
        
    }

    // Método chamado quando o jogador entra no trigger da linha de chegada
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Player" e se a colisão foi com a linha de chegada
        if (collision.CompareTag("Player") && collision.gameObject.CompareTag("Linha") && !raceFinished)
        {
            Debug.Log("Tá colidindo");
            TrackLap(); // Aumenta o contador de voltas

            if (HasFinishedRace())
            {
                FinishRace(); // Termina a corrida
            }
        }
    }

    // Método para contar as voltas
    public void TrackLap()
    {
        if (playerLap < totalLaps)
        {
            playerLap++; // Aumenta a contagem de voltas
            lapText.text = "Lap: " + playerLap + "/" + totalLaps; // Atualiza o texto de voltas
        }
    }

    // Verifica se o jogador completou todas as voltas
    public bool HasFinishedRace()
    {
        return playerLap >= totalLaps; // Retorna se o jogador terminou a corrida
    }

    // Método para finalizar a corrida
    private void FinishRace()
    {
        raceFinished = true; // Define que a corrida foi finalizada
        finishText.text = "Race Finished!"; // Exibe a mensagem de fim de corrida
        PhotonNetwork.LeaveRoom();
    }
}