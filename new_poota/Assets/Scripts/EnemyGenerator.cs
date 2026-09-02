using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // 生成する敵
    public GameObject[] enemyPrefab = new GameObject[5];
    // 生成のクールタイム
    public int span = 2;
    // 生成数の上限
    public int limit = 500;
    // 生成数のカウント
    public int enemyCount = 0;

    // フレームカウント
    int frameCount = 0;
    // モンスターID
    int monsterId = 0;

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
                Vector3 enemyPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane) + InstantiatePosition());
                enemyPosition.z = 0;
                monsterId = Random.Range(0, 5);
                Instantiate(enemyPrefab[monsterId], enemyPosition, transform.rotation);
            }    
        }
    }

    Vector3 InstantiatePosition()
    {
        // 画面の上下左右どこから生成するかを決める変数
        int select = Random.Range(0, 4);
        // 上下左右の画面幅のどのあたりから生成するかを決める変数
        float random = Random.Range(-0.75f, 0.75f);
        // プレイヤーからの離れ具合を決める変数
        float randomWidth = Random.Range(0.75f, 1f);

        switch (select)
        {
            case 0:
                return new Vector3(random, randomWidth, 0);
            case 1:
                return new Vector3(random, -randomWidth, 0);
            case 2:
                return new Vector3(randomWidth, random, 0);
            default:
                return new Vector3(-randomWidth, random, 0);
        }
    }
}