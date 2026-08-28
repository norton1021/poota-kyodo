using UnityEngine;

public class UncoCannon : MonoBehaviour
{
    // UŒ‚”{—¦
    public float atkMag = 1.5f;
    // ’e‘¬
    public float speed = 0.3f;
    
    // Šp“x‚ğŒˆ‚ß‚é‚½‚ß‚Ì•Ï”
    int rotation = 0;

    // “–‚½‚è”»’èAUŒ‚—Í‚ÌQÆ—p‚Ì•Ï”
    GameObject player;
    Collider2D playerCollider;
    Collider2D bulletCollider;


    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        transform.position = this.player.transform.position;
        this.rotation = this.player.GetComponent<PlayerController>().frameCount % 8;
        this.playerCollider = this.player.GetComponent<Collider2D>();
        this.bulletCollider = GetComponent<Collider2D>();
        if (this.bulletCollider != null && this.playerCollider != null)
        {
            Physics2D.IgnoreCollision(this.bulletCollider, this.playerCollider);
        }
    }

    void Update()
    {
        // 8•ûŒü‚ÉËo
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

        // ‰æ–ÊŠO‚Éo‚é‚ÆÁ–Å
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
            GameObject playerStatus = GameObject.FindGameObjectWithTag("Player");
            EnemyStatus enemyStatus = collision.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                enemyStatus.EnemyDecreaceHp((int)(playerStatus.GetComponent<PlayerStatus>().power * atkMag));
                Destroy(gameObject);
            }
        }
    }
}
