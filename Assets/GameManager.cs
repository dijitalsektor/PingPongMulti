using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GameManager : NetworkSingleton<GameManager>
{
    public GameObject BallPrefab;
    public Transform ClientOneSpawnPoint;
    public Transform ClientTwoSpawnPoint;
    public GameObject Colliders;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            CollidersActivationHandler(true);
            PlayersManager.Instance.OnPlayerCountTwo.AddListener(StartGame);

        }
        else if (IsClient && IsOwner)
        {
            CollidersActivationHandler(false);

        }
    }
    private void CollidersActivationHandler(bool isActive)
    {
        Colliders.SetActive(isActive);

    }
    private void StartGame()
    {
        Debug.Log("game started.");
        SpawnBall().SetBallInitialVelocity();
    }

    private BallMovement SpawnBall()
    {
        var instance = Instantiate(BallPrefab);

        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();

        return instance.GetComponent<BallMovement>();
    }
}
