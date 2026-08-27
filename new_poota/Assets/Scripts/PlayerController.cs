using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // プレイヤーの「食べ物に対する」当たり判定の半径
    public float hitboxOfPlayer = 1.0f;
    // 猫の「プレイヤーに対する」当たり判定の半径
    public float hitboxOfCat = 1.0f;
    // 攻撃範囲
    public float range = 5f;
    // （初期値0からの）攻撃間隔
    public int span = 150;
    // （初期値0からの）攻撃判定発生フレーム
    public int startFrame = 50;
    // （初期値0からの）攻撃判定消滅フレーム
    public int endFrame = 150;
    // 操作の可否
    public bool control = true;

    // フレームカウント
    int frameCount = 0;
    // 攻撃判定の有無
    bool isActive = false;
    // プレイヤーのステータス
    GameObject player;

    void Start()
    {
        Application.targetFrameRate = 60;
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (control)
        {
            PlayerStatus playerStatus = this.player.GetComponent<PlayerStatus>();

            // Aキーで左に移動
            if (Keyboard.current.aKey.isPressed)
            {
                transform.Translate(-0.1f * playerStatus.speed, 0, 0);
            }


            // Dキーで右に移動
            if (Keyboard.current.dKey.isPressed)
            {
                transform.Translate(0.1f * playerStatus.speed, 0, 0);
            }

            // Wキーで上に移動
            if (Keyboard.current.wKey.isPressed)
            {
                transform.Translate(0, 0.1f * playerStatus.speed, 0);
            }

            // Sキーで下に移動
            if (Keyboard.current.sKey.isPressed)
            {
                transform.Translate(0, -0.1f * playerStatus.speed, 0);
            }

            frameCount++;

            if (frameCount >= startFrame)
            {
                isActive = true;
                GetComponent<CircleRenderer>().line.enabled = true;
            }

            if (frameCount >= endFrame)
            {
                isActive = false;
                GetComponent<CircleRenderer>().line.enabled = false;
            }

            if (isActive)
            {
                PlayerNormalAttack(range, playerStatus.power);
            }

            if (frameCount >= span)
            {
                frameCount = 0;
            }
        }
    }

    void PlayerNormalAttack(float range, int power)
    {
        // 攻撃範囲内にいる敵を探す
        GetComponent<CircleRenderer>().DrawCircle(range);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                EnemyStatus enemyStatus = enemy.GetComponent<EnemyStatus>();
                if (enemyStatus != null)
                {
                    enemyStatus.EnemyDecreaceHp(power);
                }
            }
        }
    }
}
