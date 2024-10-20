using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;


public class RaceManager : MonoBehaviourPunCallbacks
{

public Transform player; // Referência ao carro do jogador
    public Text lapText; // Texto na UI para mostrar as voltas
    public Text finishText; // Texto na UI para mostrar quando a corrida termina
    public int totalLaps = 3; // Número total de voltas da corrida
    private int currentLap = 0; // Contador de voltas atual

    private bool raceFinished = false; // Para checar se a corrida terminou

    void Start()
    {
        // Inicializa os textos da UI
        lapText.text = "Lap: 0/" + totalLaps; // Mostra que o jogador está na volta 0 inicialmente
        finishText.text = ""; // Limpa o texto de fim de corrida
    }

    // Método que detecta a colisão com a linha de chegada
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o player colidiu com a linha de chegada
        if (collision.CompareTag("Linha") && !raceFinished)
        {
            TrackLap();

            if (currentLap >= totalLaps)
            {
                FinishRace(); // Se o jogador completar as voltas, finaliza a corrida
            }
        }
    }

    // Método para contar as voltas
    public void TrackLap()
    {
        if (currentLap < totalLaps)
        {
            currentLap++; // Aumenta a contagem de voltas
            lapText.text = "Lap: " + currentLap + "/" + totalLaps; // Atualiza o texto de volta
        }
    }


    // M�todo para finalizar a corrida
    private void FinishRace()
    {
        raceFinished = true; // Define que a corrida foi finalizada
        finishText.text = "Race Finished!"; // Exibe a mensagem de fim de corrida
        PhotonNetwork.LeaveRoom();
    }
}