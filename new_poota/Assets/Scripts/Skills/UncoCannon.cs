using UnityEngine;

public class UncoCannon : MonoBehaviour
{
    // 攻撃倍率
    public float atkMag = 1.5f;
    // 弾速
    public float speed = 0.3f;
    
    // 角度を決めるための変数
    int rotation = 0;

    // プレイヤーのステータス参照用の変数
    GameObject player;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        transform.position = this.player.transform.position;
        this.rotation = this.player.GetComponent<PlayerController>().frameCount % 8;
    }

    void Update()
    {
        // 8方向に射出
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

        // 画面外に出ると消滅
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
        GameObject playerStatus = GameObject.FindGameObjectWithTag("Player");
        if (collision.CompareTag("Enemy"))
        {
            EnemyStatus enemyStatus = collision.GetComponent<EnemyStatus>();
            if (enemyStatus != null)
            {
                enemyStatus.EnemyDecreaceHp((int)(playerStatus.GetComponent<PlayerStatus>().power * atkMag));
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("boss enemy"))
        {
            BossStatus bossStatus = collision.GetComponent<BossStatus>();
            if (bossStatus != null)
            {
                bossStatus.EnemyDecreaceHp((int)(playerStatus.GetComponent<PlayerStatus>().power * atkMag));
                Destroy(gameObject);
            }
        }
    }
}
