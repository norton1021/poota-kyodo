using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // 生成する敵
    public GameObject[] enemyPrefab = new GameObject[9];
    // 生成のクールタイム
    public int span = 2;
    // 生成数の上限
    public int limit = 500;
    // 生成数のカウント
    public int enemyCount = 0;
    // 同時に生成する敵の種類数
    public int varieties = 3;
    // 生成可能かどうか
    public bool canGenerate = true;

    // プレイヤー
    GameObject player;
    // フレームカウント
    int frameCount = 0;
    // モンスターID最低
    int minId = 0;
    // モンスターID最大
    int maxId = 0;
    private bool canSpawn = true;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        if (this.varieties > enemyPrefab.Length)
        {
            this.varieties = enemyPrefab.Length;
        }
    }

    void Update()
    {
        if (this.minId + varieties < enemyPrefab.Length)
        {
            this.minId = player.GetComponent<PlayerLevel>().level / 3;
            this.maxId = this.minId + varieties;
        }
        else
        {
            this.minId = enemyPrefab.Length - varieties;
            this.maxId = enemyPrefab.Length;
        }

        if (canGenerate)
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
                if (enemyCount < limit && canSpawn)
                {
                    enemyCount++;
                    Vector3 enemyPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Camera.main.nearClipPlane) + InstantiatePosition());
                    enemyPosition.z = 0;
                    Instantiate(enemyPrefab[Random.Range(this.minId, this.maxId)], enemyPosition, transform.rotation);
                }
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
    public void StopSpawn(float time)
    {
        StartCoroutine(StopSpawnCoroutine(time));
    }
    private IEnumerator StopSpawnCoroutine(float time)
    {
        canSpawn = false;
        yield return new WaitForSeconds(time);  
        canSpawn = true;
    }
}