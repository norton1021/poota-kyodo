using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    // 最大体力
    public int maxHP = 100;
    // 基礎攻撃力
    public int power = 20;
    // 移動速度
    public float speed = 1.0f;

    // ゲームプレイ中の体力
    int currentHP = 100;

    void Start()
    {
        currentHP = maxHP;
    }

    public void EnemyDecreaceHp(int damage)
    {
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
