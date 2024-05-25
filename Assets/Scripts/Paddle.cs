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
        Debug.Log("Rigidbody olustu");
    }
    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner)
        {
            GetClientInput();
        }
        // Yön tuþlarý veya diðer giriþlerle raketin hareketini kontrol et

        //float moveAmount = moveInput * speed * Time.deltaTime;

        //// Raketin yeni pozisyonunu hesapla
        //float newYPosition = transform.position.y + moveAmount;

        //// Yeni pozisyonu sýnýrla
        //newYPosition = Mathf.Clamp(newYPosition, minY, maxY);

        //// Raketin konumunu güncelle
        //transform.position = new Vector2(transform.position.x, newYPosition);
    }

    private void FixedUpdate()
    {
        if (IsServer)
        {


            // Rigidbody üzerinde yalnızca y ekseni hareketini uygula

            Vector2 movement = new Vector2(0, YAxisInputValue.Value * speed * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + movement);

            //// Yeni pozisyonu sınırla
            //Vector2 clampedPosition = new Vector2(rb.position.x, Mathf.Clamp(rb.position.y, minY, maxY));
            //rb.MovePosition(clampedPosition);
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
