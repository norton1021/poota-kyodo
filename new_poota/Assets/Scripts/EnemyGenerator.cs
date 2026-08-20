using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject[] enemyPrefab = new GameObject[5];
    int span = 60;
    int frameCount = 0;

    void Update()
    {
        frameCount++;
        if (frameCount >= span)
        {
            frameCount = 0;
            GameObject go = Instantiate(enemyPrefab[0]);
            int px = Random.Range(-6, 7);
            go.transform.position = new Vector3(px, 7, 0);
        }
    }
}