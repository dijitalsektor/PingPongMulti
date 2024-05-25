using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class PlayersManager : NetworkSingleton<PlayersManager>
{
    NetworkVariable<int> playersCount = new NetworkVariable<int>();
    public UnityEvent OnPlayerCountTwo;
    public GameObject Paddle;
    public int PlayerCount
    {
        get { return playersCount.Value; }
    }
    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {

            NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
            {
                playersCount.Value++;
                Debug.Log($"Player {id} is connected.");
                var instance = Instantiate(NetworkManager.GetNetworkPrefabOverride(Paddle),
                                           (playersCount.Value == 1) ? GameManager.Instance.ClientOneSpawnPoint : GameManager.Instance.ClientTwoSpawnPoint);

                var instanceNetworkObject = instance.GetComponent<NetworkObject>();
                instanceNetworkObject.SpawnAsPlayerObject(id);

                if (playersCount.Value == 2)
                {
                    OnPlayerCountTwo.Invoke();
                }

            };
            NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
            {
                playersCount.Value--;
                Debug.Log($"Player {id} is disconnected.");

            };
        }
    }
    private void Start()
    {




    }
}
