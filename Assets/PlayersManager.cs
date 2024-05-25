using System;
using Unity.Netcode;
using UnityEngine;

public class PlayersManager : NetworkSingleton<PlayersManager>
{
    NetworkVariable<int> playersCount = new NetworkVariable<int>();
    public GameObject Paddle;
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
            var instance = Instantiate(NetworkManager.GetNetworkPrefabOverride(Paddle), 
                                       (playersCount.Value == 1) ? GameManager.Instance.ClientOneSpawnPoint : GameManager.Instance.ClientTwoSpawnPoint);

            var instanceNetworkObject = instance.GetComponent<NetworkObject>();
            instanceNetworkObject.SpawnAsPlayerObject(id);
        };
        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            playersCount.Value--;
            Debug.Log($"Player {id} is disconnected.");

        };


    }
}
