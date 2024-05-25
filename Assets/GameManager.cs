using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GameManager : NetworkSingleton<GameManager>
{
    public GameObject BallPrefab;
    public Transform ClientOneSpawnPoint;
    public Transform ClientTwoSpawnPoint;


    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {

            PlayersManager.Instance.OnPlayerCountTwo.AddListener(StartGame);

        }
    }

    private void StartGame()
    {
        Debug.Log("game started.");
        SpawnBall();
    }

    private void SpawnBall()
    {
        var instance = Instantiate(BallPrefab);

        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();
    }
}
