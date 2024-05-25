using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using static NetController;

public class GameManager : NetworkSingleton<GameManager>
{
    public GameObject BallPrefab;
    public Transform ClientOneSpawnPoint;
    public Transform ClientTwoSpawnPoint;
    public GameObject Colliders;
    public NetController LeftNetController;
    public NetController RightNetController;

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            CollidersActivationHandler(true);

            //TODO:Remove this later
            PlayersManager.Instance.OnPlayerCountTwo.AddListener(StartGame);

            LeftNetController.GoalEvent.AddListener(OnGoal);
            RightNetController.GoalEvent.AddListener(OnGoal);

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
    public void StartGame()
    {
        Debug.Log("game started.");
        SpawnBall().SetBallInitialVelocity();
    }

    public void OnGoal(NetType netType)
    {
        switch (netType)
        {
            case NetType.Left:
                Debug.Log($"top Left  kalesine girdi.");
                ScoreManager.Instance.RighTeamGoal();

                break;
            case NetType.Right:
                Debug.Log($"top Right  kalesine girdi.");
                ScoreManager.Instance.LefTeamGoal();

                break;
            default:
                break;
        }
    }

    private BallMovement SpawnBall()
    {
        var instance = Instantiate(BallPrefab);

        var instanceNetworkObject = instance.GetComponent<NetworkObject>();
        instanceNetworkObject.Spawn();

        return instance.GetComponent<BallMovement>();
    }
}
