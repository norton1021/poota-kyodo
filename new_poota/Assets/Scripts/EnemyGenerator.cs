using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // 生成する敵
    public GameObject[] enemyPrefab = new GameObject[5];
    // 生成のクールタイム
    public int span = 60;
    // 生成数の上限
    public int limit = 20;
    // 生成数のカウント
    public int enemyCount = 0;

    // フレームカウント
    int frameCount = 0;

    void Update()
    {
        if (enemyCount < limit)
        {
            frameCount++;
        }
        else
        {
            frameCount = 0;
        }
        
        if (frameCount >= span)
        {
            frameCount = 0;
            if (enemyCount < limit)
            {
                enemyCount++;
                GameObject go = Instantiate(enemyPrefab[0]);
                int px = Random.Range(-6, 7);
                go.transform.position = new Vector3(px, 7, 0);
            }    
        }
    }
}