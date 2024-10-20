using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
 // Referência ao RaceManager para comunicar o número de voltas
    public RaceManager raceManager;

    // Detecta colisão com o carro do jogador
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (collision.CompareTag("Player"))
        {
            // Chama o método para aumentar a volta no RaceManager
            raceManager.TrackLap();
        }
    }
}
