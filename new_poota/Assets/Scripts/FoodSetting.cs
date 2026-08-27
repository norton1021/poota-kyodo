using UnityEngine;
using UnityEngine.InputSystem;

public class FoodSetting : MonoBehaviour
{
    // 食べ物の名前
    
    // 食べ物の経験値
    public int exp = 20;
    // 食べ物の満腹度
    public int feed = 30;
    // 食べ物の「プレイヤーに対する」当たり判定の半径
    public float hitboxOfFoods = 1.0f;

    // 食べ物を持つ場所
    Transform target;
    // 食べ物を持っているかどうか
    bool hasFood = false;

    GameObject player;
    GameObject cat;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.cat = GameObject.FindGameObjectWithTag("Cat");
        if (target == null && player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        // 当たり判定
        Vector2 p1 = this.player.transform.position;
        Vector2 p2 = transform.position;
        Vector2 p3 = this.cat.transform.position;
        Vector2 dir1 = p1 - p2;
        Vector2 dir2 = p1 - p3;
        float d1 = dir1.magnitude; // プレイヤーから食べ物までの距離
        float d2 = dir2.magnitude; // プレイヤーから猫までの距離
        float r1 = this.player.GetComponent<PlayerController>().hitboxOfPlayer;
        float r2 = hitboxOfFoods;
        float r3 = this.player.GetComponent<PlayerController>().hitboxOfCat;

        if (Keyboard.current.eKey.isPressed)
        {
            if (!hasFood)
            {
                if (d1 < r1 + r2)
                {
                    PickUpFood();
                }
            }
            else
            {
                if (d2 < r1 + r3)
                {
                    GiveFoodToCat();
                }
            }
        }

        if (hasFood)
        {
            transform.position = this.target.transform.position;
        }

        if (target == null)
        {
            return;
        }
    }

    void PickUpFood()
    {
        hasFood = true;
        this.player.GetComponent<PlayerStatus>().speed = 0.7f;
        GameObject generator = GameObject.Find("FoodGenerator");
        generator.GetComponent<FoodGenerator>().foodCount--;
        Debug.Log(gameObject.name + "を拾った");
    }

    void GiveFoodToCat()
    {
        hasFood = false;
        this.player.GetComponent<PlayerStatus>().speed = 1f;
        Debug.Log("猫に" + gameObject.name + "をあげた");
        this.player.GetComponent<PlayerLevel>().AddExperience(exp);
        this.cat.GetComponent<CatHunger>().Feed(feed);
        Destroy(gameObject);
    }
}