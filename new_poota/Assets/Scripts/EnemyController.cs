using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // 追尾するターゲット
    Transform target;
    
    void Start()
    {
        // ターゲットが未設定ならタグ "Player" を探す
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void Update()
    {
        EnemyStatus enemyStatus = GetComponent<EnemyStatus>();

        if (target == null)
        {
            return;
        }

        // ターゲット方向のベクトルを計算
        Vector3 direction = (target.position - transform.position).normalized;

        // 直線的に移動（回転なし）
        transform.position += direction * enemyStatus.speed * Time.deltaTime;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        EnemyStatus enemyStatus = GetComponent<EnemyStatus>();
        PlayerStatus playerStatus = collision.gameObject.GetComponent<PlayerStatus>();
        playerStatus.PlayerDecreaceHp(enemyStatus.power);
    }
}
