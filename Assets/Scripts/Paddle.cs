using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 5f; // Raketin hareket h�z�
    public float minY = -3.5f; // Raketin minimum y konumu
    public float maxY = 3.5f; // Raketin maksimum y konumu

    // Update is called once per frame
    void Update()
    {
        // Y�n tu�lar� veya di�er giri�lerle raketin hareketini kontrol et
        float moveInput = Input.GetAxisRaw("Vertical");
        float moveAmount = moveInput * speed * Time.deltaTime;

        // Raketin yeni pozisyonunu hesapla
        float newYPosition = transform.position.y + moveAmount;

        // Yeni pozisyonu s�n�rla
        newYPosition = Mathf.Clamp(newYPosition, minY, maxY);

        // Raketin konumunu g�ncelle
        transform.position = new Vector2(transform.position.x, newYPosition);
    }
}
