using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    public float stompBounce = 8f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, stompBounce);
            }
            Debug.Log("ìGÇì|ÇµÇΩÅI");
        }
    }
}