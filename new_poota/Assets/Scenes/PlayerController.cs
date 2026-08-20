using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // 攻撃範囲
    public float range = 4f;
    // （初期値0からの）攻撃間隔
    public int span = 60;
    // （初期値0からの）攻撃判定発生フレーム
    public int startFrame = 59;
    // （初期値0からの）攻撃判定消滅フレーム
    public int endFrame = 60;

    // フレームカウント
    int frameCounter = 0;
    // 攻撃判定の有無
    bool isActive = false;

    void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        PlayerStatus playerStatus = GetComponent<PlayerStatus>();

        // 食べ物を拾うことで減速
        if (GetComponent<playerfood>().hasfood)
            playerStatus.speed = 0.7f;
        else
            playerStatus.speed = 1.0f;

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

        frameCounter++;

        if (frameCounter >= startFrame)
        {
            isActive = true;
        }

        if (frameCounter >= endFrame)
        {
            isActive = false;
        }

        if (isActive)
        {
            PlayerNormalAttack(range, playerStatus.power);
        }

        if (frameCounter >= span)
        {
            frameCounter = 0;
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
                Debug.Log("攻撃範囲内:" + enemy.gameObject.name);
                EnemyStatus enemyStatus = enemy.GetComponent<EnemyStatus>();
                if (enemyStatus != null)
                {
                    enemyStatus.EnemyDecreaceHp(power);
                }
            }
        }
    }
}
