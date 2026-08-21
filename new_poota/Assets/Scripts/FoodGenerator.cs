using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public int Span = 600;
    int frameCount=0;
    public GameObject foodprefab;
    // 生成数の上限
    public int limit = 1;
    // 生成数のカウント
    public int foodCount = 0;

    void Update()
    {
        if (foodCount < limit)
        {
            frameCount++;
        }
        else
        {
            frameCount = 0;
        }
        
        if (frameCount >= Span)
        {
            frameCount = 0;
            if (foodCount < limit)
            {
                foodCount++;
                Instantiate(foodprefab, transform.position, transform.rotation);
            }
        }
    }
}
