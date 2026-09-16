using UnityEngine;
public class PlayerAttack : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Debug.Log("ìGÇì|ÇµÇΩÅI");

        }
    }
}