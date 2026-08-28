using UnityEngine;

public class UncoCannonManager : MonoBehaviour
{
    // （初期値0からの）攻撃間隔
    public int span = 240;
    // （初期値0からの）攻撃判定発生フレーム
    public int startFrame = 0;
    // （初期値0からの）攻撃判定消滅フレーム
    public int endFrame = 180;
    // 使用の可否
    public bool control = false;
    // Un-Coキャノン
    public GameObject uncoCannonPrefab;
    
    // フレームカウント
    int frameCount = 0;
    // 攻撃の有無
    bool isActive = false;

    void Update()
    {
        if (control)
        {
            frameCount++;

            if (frameCount >= startFrame)
            {
                isActive = true;
            }

            if (frameCount >= endFrame)
            {
                isActive = false;
            }

            if (isActive)
            {
                Instantiate(uncoCannonPrefab);
            }

            if (frameCount >= span)
            {
                frameCount = 0;
            }
        }
    }
}
