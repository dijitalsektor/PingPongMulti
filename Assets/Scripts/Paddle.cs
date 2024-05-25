using System;
using Unity.Netcode;
using UnityEngine;

public class Paddle : NetworkBehaviour
{
    public float speed = 5f; // Raketin hareket hýzý
    public float minY = -3.5f; // Raketin minimum y konumu
    public float maxY = 3.5f; // Raketin maksimum y konumu

    private Rigidbody2D rb;

    [SerializeField]
    private NetworkVariable<float> YAxisInputValue = new NetworkVariable<float>();

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner)
        {
            GetClientInput();
        }
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {
            Vector2 movement = new Vector2(0, YAxisInputValue.Value * speed * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + movement);

        }
    }

    private void GetClientInput()
    {
        float moveInput = Input.GetAxisRaw("Vertical");
        SendClientInputServerRPC(moveInput);
    }

    [ServerRpc]
    private void SendClientInputServerRPC(float moveInput)
    {
        YAxisInputValue.Value = moveInput;
    }
}
