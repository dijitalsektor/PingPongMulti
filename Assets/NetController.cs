using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NetController : MonoBehaviour
{
    public enum NetType { Left,Right };
    public NetType CurrentNetType;
    public UnityEvent<NetType> GoalEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent(out BallMovement ballMovement))
        {
         

            GoalEvent.Invoke(CurrentNetType);


        }
    }
    
}
