using System;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : NetworkSingleton<PlayersManager>
{
    NetworkVariable<int> playersCount = new NetworkVariable<int>();

    public int PlayerCount
    {
        get { return playersCount.Value; }
    }

    private void Start()
    {
       
            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                playersCount.Value++;
                Debug.Log($"Player {id} is connected.");
            };
            NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
            {
                playersCount.Value--;
                Debug.Log($"Player {id} is disconnected.");
            };
        

    }
}
