using UnityEngine;

public class playerfood : MonoBehaviour
{
    public GameObject food;
    public GameObject holdpoint;
    public bool hasfood = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!hasfood && food != null)
            {
                float foodDistance = Vector2.Distance(
                    transform.position, food.transform.position);
                if (foodDistance < 1.5f) ;
                {
                    PickUpFood();
                    return;
                }
            }
            if (hasfood)
            {
                GameObject cat = GameObject.FindGameObjectWithTag("cat");
                if (cat != null)
                {
                    float catDistance = Vector2.Distance(
                        transform.position, cat.transform.position);
                    if (catDistance < 1.5f)
                    {
                        GiveFoodToCat(cat);
                    }
                }
            }
        }
    }
    void PickUpFood()
    {
        hasfood = true;
        food.transform.SetParent(holdpoint.transform);
        food.transform.localPosition = Vector3.zero;
        Debug.Log("H‚×•¨‚ðE‚Á‚½");
    }

    void GiveFoodToCat(GameObject cat)
    {
        hasfood = false;
        food.transform.SetParent(null);
        food.SetActive(false);
        Debug.Log("”L‚ÉH‚×•¨‚ð‚ ‚°‚½");
    }
}