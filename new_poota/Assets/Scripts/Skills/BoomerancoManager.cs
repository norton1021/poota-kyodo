using UnityEngine;

public class BoomerancoManager : MonoBehaviour
{
    // 攻撃間隔
    public int span = 150;
    // 攻撃1撃目発生フレーム
    public int startFrame1 = 120;
    // 攻撃2撃目発生フレーム
    public int startFrame2 = 150;
    // 使用の可否
    public bool able = false;
    // Un-Coキャノン
    public GameObject boomerancoPrefab;
    // 左右どちらか
    public int side = 0;

    // フレームカウント
    int frameCount = 0;
    // プレイヤー
    GameObject player;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (this.player.GetComponent<PlayerController>().control)
        {
            if (able)
            {
                frameCount++;

                if (frameCount == startFrame1)
                {
                    side = 0;
                    Instantiate(boomerancoPrefab);
                }

                if (frameCount == startFrame2)
                {
                    side = 180;
                    Instantiate(boomerancoPrefab);
                }

                if (frameCount >= span)
                {
                    frameCount = 0;
                }
            }
        }
    }
}
