using UnityEngine;

public class Boomeranco : MonoBehaviour
{
    // 攻撃倍率
    public float atkMag = 3f;
    // 弾速
    public float speed = 0.4f;

    // 角度を決めるための変数
    int rotation = 0;

    // プレイヤーのステータス参照用の変数
    GameObject player;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        transform.position = this.player.transform.position;
        this.rotation = this.player.GetComponent<BoomerancoManager>().side;
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }

    void Update()
    {
        // 左右に射出
        switch (this.rotation)
        {
            case 0:
                transform.Translate(speed, 0, 0);
                break;
            default:
                transform.Translate(speed, 0, 0);
                break;
        }

        Vector3 pos = transform.position;
        pos.y = this.player.transform.position.y;
        transform.position = pos;
        speed -= 0.01f;

        // transform.Rotate(0, 0, 10);

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
            }
        }
        else if (collision.CompareTag("boss enemy"))
        {
            BossStatus bossStatus = collision.GetComponent<BossStatus>();
            if (bossStatus != null)
            {
                bossStatus.EnemyDecreaceHp((int)(playerStatus.GetComponent<PlayerStatus>().power * atkMag));
            }
        }
    }
}
