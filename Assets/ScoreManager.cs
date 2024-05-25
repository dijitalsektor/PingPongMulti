using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ScoreManager : NetworkSingleton<ScoreManager>
{
    private NetworkVariable<int> leftTeamScore = new NetworkVariable<int>();
    private NetworkVariable<int> RightTeamScore = new NetworkVariable<int>();

    public int tempCount = 1;
    //private bool isNetworkSpawned = false;
    public string Score
    {
        get { return $"{leftTeamScore.Value} -  {RightTeamScore.Value}"; }
    }

    public override void OnNetworkSpawn()
    {
        //if (IsServer)
        //{
        //    isNetworkSpawned = true;
        //}

        UIManager.Instance.UpdateScoreText(Score);

        leftTeamScore.OnValueChanged += GetChangeCounter;
        RightTeamScore.OnValueChanged += GetChangeCounter;
        //}
    }

    private void Update()
    {
        //if (isNetworkSpawned)
        //{

        //    if (IsServer)
        //    {

        //        if (Input.GetKeyDown(KeyCode.Space))
        //        {
        //            LefTeamGoal();
        //        }
        //        if (Input.GetKeyDown(KeyCode.N))
        //        {
        //            RighTeamGoal();
        //        }
        //    }
        //}
    }
    public void GetChangeCounter(int oldValue, int newValue)
    {
        Debug.Log("buraya girdi");
        UIManager.Instance.UpdateScoreText(Score);
    }

    public void LefTeamGoal()
    {
        leftTeamScore.Value = leftTeamScore.Value + 1;

    }
    public void RighTeamGoal()
    {
        RightTeamScore.Value = RightTeamScore.Value + 1;

    }
    public override void OnNetworkDespawn()
    {
        leftTeamScore.OnValueChanged -= GetChangeCounter;
        RightTeamScore.OnValueChanged -= GetChangeCounter;

    }
}
