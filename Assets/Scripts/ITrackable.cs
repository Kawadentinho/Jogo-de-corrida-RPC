using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackable
{
    void TrackLap(); // Método para rastrear voltas
    bool HasFinishedRace(); // Método que verifica se o jogador terminou a corrida
}

