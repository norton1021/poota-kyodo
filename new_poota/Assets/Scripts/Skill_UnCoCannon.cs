using UnityEngine;

public class Skill_UnCoCannon : MonoBehaviour
{
    // ’e‘¬
    public float speed = 0.3f;
    int rotation = 0;
    GameObject player;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
        rotation = player.GetComponent<PlayerController>().frameCount % 8;
    }

    void Update()
    {
        switch (this.rotation)
        {
            case 0:
                transform.Translate(speed, 0, 0);
                break;
            case 1:
                transform.Translate(speed, speed, 0);
                break;
            case 2:
                transform.Translate(0, speed, 0);
                break;
            case 3:
                transform.Translate(-speed, speed, 0);
                break;
            case 4:
                transform.Translate(-speed, 0, 0);
                break;
            case 5:
                transform.Translate(-speed, -speed, 0);
                break;
            case 6:
                transform.Translate(0, -speed, 0);
                break;
            case 7:
                transform.Translate(speed, -speed, 0);
                break;
        }

        if (transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1.25f, 0.5f, Camera.main.nearClipPlane)).x ||
            transform.position.x < Camera.main.ViewportToWorldPoint(new Vector3(-0.25f, 0.5f, Camera.main.nearClipPlane)).x ||
            transform.position.y > Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1.25f, Camera.main.nearClipPlane)).y ||
            transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0.5f, -0.25f, Camera.main.nearClipPlane)).y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("a");
            this.player = GameObject.FindGameObjectWithTag("Player");
            EnemyStatus enemyStatus = collision.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                enemyStatus.EnemyDecreaceHp(this.player.GetComponent<PlayerStatus>().power);
                Destroy(gameObject);
            }
        }
    }
}
