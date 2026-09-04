using UnityEngine;
using UnityEngine.UI;
public class BossStatus : MonoBehaviour
{
    // 最大体力
    public int maxHP = 100;
    // 基礎攻撃力
    public int power = 20;
    // 移動速度
    public float speed = 1.0f;
    // 無敵時間
    public int invincibleTime = 5;
    // 体力のUI
    public Slider enemyHpSlider;
    // UIの頭上位置
    public Vector3 uiOffset = new Vector3(0, 1f, 0);
    // ゲームプレイ中の体力
    public int currentHP = 100;
    public SpecialAttack specialAttack;
    // 無敵かどうか
    bool invincible = false;
    // フレームカウント
    int frameCount = 0;
    // 画面外に居たフレームのカウント
    int outsideFrameCount = 0;
    public GameObject areaBlock;

    void Start()
    {
        currentHP = maxHP;
        UpdateHpUI();
    }
    void UpdateHpUI()
    {
        if (enemyHpSlider != null)
        {
            enemyHpSlider.maxValue = maxHP;
            enemyHpSlider.value = currentHP;
        }
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
            GameObject.Find("SpecialAttackManeger").GetComponent<SpecialAttack>().AddGauge(1);
            Defeat();
        }
    }
}
    public void Defeat()
    {
        if (areaBlock != null)
        {
            Destroy(areaBlock);
        }
        Destroy(gameObject);
    }
}
