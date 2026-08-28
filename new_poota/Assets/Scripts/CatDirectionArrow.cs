using UnityEngine;

public class CatDirectionArrow : MonoBehaviour
{
    public Transform player;
    public Transform cat;
    
    void Update()
    {
        if (player == null || cat == null)
        {
            return;
        }
        Vector3 direction=cat.position-player.position;
        float angle =Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
