using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrackable
{
    void TrackLap(); // M�todo para rastrear voltas
    bool HasFinishedRace(); // M�todo que verifica se o jogador terminou a corrida
}

