using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f; // Topun hareket hýzý
    private Vector2 direction; // Topun hareket yönü

    // Start is called before the first frame update
    void Start()
    {
        // Topa baþlangýçta rastgele bir yön ver
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Topu hareket ettir
        transform.Translate(direction * speed * Time.deltaTime);

        // Ekran kenarlarýna çarpýnca yönü tersine çevir
        if (transform.position.y <= -4.5f || transform.position.y >= 4.5f)
        {
            direction.y *= -1;
        }
    }

    // Çarpýþma kontrolü
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Çarpýþma olduysa topun yönünü deðiþtir
            direction.x *= -1;
        }
    }

    // Topu baþlangýç konumuna geri döndür
    public void ResetBall()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
