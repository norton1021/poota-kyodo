using UnityEngine;
using UnityEngine.UI;

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
    // 体力のUI
    public Slider enemyHpSlider;
    // UIの頭上位置
    public Vector3 uiOffset = new Vector3(0, 1f, 0);

    // ゲームプレイ中の体力
    int currentHP = 100;
    // 無敵かどうか
    bool invincible = false;
    // フレームカウント
    int frameCount = 0;
    // カメラ
    Camera mainCamera;

    void Start()
    {
        currentHP = maxHP;
        UpdateHpUI();
    }

    void Update()
    {
        if (enemyHpSlider != null)
        {
            enemyHpSlider.transform.position = transform.position + uiOffset;
        }

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
            UpdateHpUI();
            if (currentHP <= 0)
            {
                GameObject generator = GameObject.Find("EnemyGenerator");
                generator.GetComponent<EnemyGenerator>().enemyCount--;
                Destroy(gameObject);
            }
        }
    }
    void UpdateHpUI()
    {
        if (enemyHpSlider != null)
        {
            enemyHpSlider.maxValue = maxHP;
            enemyHpSlider.value = currentHP;
        }
    }
}
