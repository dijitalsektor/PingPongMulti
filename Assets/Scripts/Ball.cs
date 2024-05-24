using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 5f; // Topun hareket h�z�
    private Vector2 direction; // Topun hareket y�n�

    // Start is called before the first frame update
    void Start()
    {
        // Topa ba�lang��ta rastgele bir y�n ver
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Topu hareket ettir
        transform.Translate(direction * speed * Time.deltaTime);

        // Ekran kenarlar�na �arp�nca y�n� tersine �evir
        if (transform.position.y <= -4.5f || transform.position.y >= 4.5f)
        {
            direction.y *= -1;
        }
    }

    // �arp��ma kontrol�
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // �arp��ma olduysa topun y�n�n� de�i�tir
            direction.x *= -1;
        }
    }

    // Topu ba�lang�� konumuna geri d�nd�r
    public void ResetBall()
    {
        transform.position = Vector2.zero;
        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
