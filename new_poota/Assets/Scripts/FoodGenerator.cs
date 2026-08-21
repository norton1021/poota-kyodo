using UnityEngine;

public class FoodGenerator : MonoBehaviour
{
    public int Span = 600;
    int frameCount=0;
    public GameObject foodprefab;
    
    void Update()
    {
        frameCount++;
        if (frameCount >= Span)
        {
            frameCount = 0;
            Instantiate(foodprefab,transform.position,transform.rotation);
        }
    }
}
