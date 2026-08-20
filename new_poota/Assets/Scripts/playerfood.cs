using UnityEngine;
using UnityEngine.InputSystem;

public class playerfood : MonoBehaviour
{
    public PlayerLevel playerLevel;
    public CatHunger catHunger;
    public GameObject food;
    public GameObject holdpoint;
    public bool hasfood = false;

    void Update()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            if (!hasfood && food != null)
            {
                Collider2D hitFood = Physics2D.OverlapCircle(transform.position, 1.5f);
                if (hitFood.CompareTag("Food"))
                {
                    PickUpFood();
                    return;
                }
            }
            if (hasfood)
            {
                GameObject cat = GameObject.FindGameObjectWithTag("Cat");
                if (cat != null)
                {
                    Collider2D hitCat = Physics2D.OverlapCircle(transform.position, 1.5f);
                    if (hitCat.CompareTag("Cat"))
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
        Debug.Log("êHÇ◊ï®ÇèEÇ¡ÇΩ");
    }

    void GiveFoodToCat(GameObject cat)
    {
        hasfood = false;
        food.transform.SetParent(null);
        food.SetActive(false);
        Debug.Log("îLÇ…êHÇ◊ï®ÇÇ†Ç∞ÇΩ");
        playerLevel.AddExperience(20);
        cat.GetComponent<CatHunger>().Feed(30);
    }
}