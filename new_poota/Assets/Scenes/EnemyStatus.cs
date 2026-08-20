using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // 最大体力
    public int maxHP = 100;
    // 基礎攻撃力
    public int power = 20;
    // 移動速度
    public float speed = 1.0f;
    // 無敵時間
    public int invincibleTime = 10;

    // ゲームプレイ中の体力
    int currentHP = 100;
    // 無敵かどうか
    bool invincible = false;
    // フレームカウント
    int frameCount = 0;

    void Start()
    {
        currentHP = maxHP;
    }

    void Update()
    {
        if (invincible)
        {
            frameCount++;
            if (frameCount >= invincibleTime)
            {
                invincible = false;
                frameCount = 0;
            }
        }
    }

    public void EnemyDecreaceHp(int damage)
    {
        if (!invincible)
        {
            invincible = true;
            currentHP -= damage;
            if (currentHP <= 0)
            {
                Debug.Log(gameObject.name + "は倒れた");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log(gameObject.name + " HP:" + currentHP);
            }
        }
    }
}
