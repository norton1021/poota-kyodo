using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SpecialAttack : MonoBehaviour
{
    public GameObject specialCutIn;
    public Slider specialGauge;
    public float maxGauge = 2000f;
    public GameObject specialNotReady;
    public GameObject specialReady;

    // スキルが使用可能かどうか
    bool canUse = false;
    // スキルが発動中かどうか
    bool isPlaying = false;
    // カットインを表示するかどうか
    bool cutInIsPlaying = false;
    // カットインの表示フレームカウント
    int cutInFrameCount = 0;
    // カットインの表示時間
    int cutInStopTime = 90;

    // スキル用_フレームカウント
    int frameCount = 0;
    // スキル用_生成停止時間
    int stopTime = 180;

    void Start()
    {
        specialCutIn.SetActive(false);
        specialGauge.maxValue = maxGauge;
        specialGauge.value = 0;
        specialNotReady.SetActive(true);
        specialReady.SetActive(false);
    }

    void Update()
    {
        // 発動時の効果
        if (Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (canUse || !isPlaying)
            {
                // スキルとカットインの管理
                canUse = false;
                isPlaying = true;
                cutInIsPlaying = true;
                specialGauge.value = 0;
                specialNotReady.SetActive(true);
                specialReady.SetActive(false);
                specialCutIn.SetActive(true);

                // スキル「ストップジェネレーション」の発動時の効果
                Special_StopGeneration_instant();
            }
        }

        // 発動中の効果
        if (isPlaying)
        {
            // スキル「ストップジェネレーション」の発動中の効果
            Special_StopGeneration();
        }

        // カットインの処理
        if (cutInIsPlaying)
        {
            cutInFrameCount++;

            if (cutInFrameCount > cutInStopTime)
            {
                specialCutIn.SetActive(false);
                cutInIsPlaying = false;
                cutInFrameCount = 0;
            }
        }
    }

    public void AddGauge(float amount)
    {
        specialGauge.value += amount;
        if (specialGauge.value >= maxGauge)
        {
            specialGauge.value = maxGauge;
            canUse = true;
            specialNotReady.SetActive(false);
            specialReady.SetActive(true);
        }
    }

    void Special_StopGeneration_instant()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    void Special_StopGeneration()
    {
        EnemyGenerator generatorComponent = GameObject.Find("EnemyGenerator").GetComponent<EnemyGenerator>();
        generatorComponent.canGenerate = false;
        generatorComponent.enemyCount = 0;

        frameCount++;

        if (frameCount >= stopTime)
        {
            generatorComponent.canGenerate = true;
            isPlaying = false;
            frameCount = 0;
        }
    }
}