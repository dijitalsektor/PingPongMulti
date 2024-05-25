using Unity.Netcode;
using UnityEngine;


public class BallMovement : NetworkBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float minVelocity = 10f;

    private Vector3 lastFrameVelocity;
    private Rigidbody2D rb;

    private void Start()
    {
       
    }
    public void SetBallInitialVelocity()
    {
        
        
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = GenerateRandomInitialDirection() * speed;

    }

    private Vector2 GenerateRandomInitialDirection()
    {
        return new Vector2(Random.Range(0.8f, 1f), Random.Range(0.8f, 1f));
    }
    private void Update()
    {
        if (!IsServer) { return; }

        lastFrameVelocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer) { return; }

        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector2 collisionNormal)
    {
        var speed = lastFrameVelocity.magnitude;
        var direction = Vector2.Reflect(lastFrameVelocity.normalized, collisionNormal);

        Debug.Log("Out Direction: " + direction);
        rb.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}
