using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 基礎攻撃力
    public int attackPower = 20;
    // 攻撃範囲
    public float attackRange = 4f;
    // （初期値0からの）攻撃間隔
    public int span = 60;
    // （初期値0からの）攻撃判定発生フレーム
    public int startFrame = 59;
    // （初期値0からの）攻撃判定消滅フレーム
    public int endFrame= 60;
    // フレームカウント
    private int frameCounter = 0;
    // 攻撃判定の有無
    private bool isActive = false;

    void Update()
    {
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
            Attack();
        }

        if (frameCounter >= span)
        {
            frameCounter = 0;
        }
    }
    void Attack()
    {
        GetComponent<CircleRenderer>().DrawCircle(attackRange);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            transform.position, attackRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Debug.Log("攻撃範囲内" + enemy.gameObject.name);
                Health health = enemy.GetComponent<Health>();
                if (health != null)
                {
                    Debug.Log("敵にダメージ");
                    health.TakeDamage(attackPower);
                    
                }
                else
                {
                    Debug.Log("EnemyにHealthがありません");
                }
            }
        }
    }
}