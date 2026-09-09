using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel;

    public Transform player;
    public RectTransform playerIcon;

    public Vector2 mapMin = new Vector2(-255, -255);
    public Vector2 mapMax = new Vector2(255, 255);

    // 食べ物
    public Transform[] foods;
    public RectTransform foodIconPrefab;
    public RectTransform foodIconParent;

    private RectTransform mapRect;
    private RectTransform[] foodIcons;

    void Start()
    {
        mapRect = mapPanel.GetComponent<RectTransform>();

        // 最初はマップを非表示
        mapPanel.SetActive(false);

        // 食べ物アイコンを作る
        foodIcons = new RectTransform[foods.Length];

        for (int i = 0; i < foods.Length; i++)
        {
            foodIcons[i] = Instantiate(
                foodIconPrefab,
                foodIconParent
            );

            UpdateFoodIcon(foodIcons[i], foods[i]);
        }
    }

    void Update()
    {
        // Mキーでマップを開閉
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapPanel.SetActive(!mapPanel.activeSelf);
        }

        // マップが開いている時だけ位置を更新
        if (mapPanel.activeSelf)
        {
            // プレイヤー位置
            Vector2 playerPos =
                WorldToMapPosition(player.position);

            playerIcon.anchoredPosition = playerPos;

            // 食べ物位置
            for (int i = 0; i < foods.Length; i++)
            {
                if (foods[i] == null)
                {
                    foodIcons[i].gameObject.SetActive(false);
                    continue;
                }

                foodIcons[i].gameObject.SetActive(true);

                UpdateFoodIcon(
                    foodIcons[i],
                    foods[i]
                );
            }
        }
    }

    Vector2 WorldToMapPosition(Vector3 worldPos)
    {
        float x = Mathf.InverseLerp(
            mapMin.x,
            mapMax.x,
            worldPos.x
        );

        float y = Mathf.InverseLerp(
            mapMin.y,
            mapMax.y,
            worldPos.y
        );

        float mapX =
            (x - 0.5f) * mapRect.rect.width;

        float mapY =
            (y - 0.5f) * mapRect.rect.height;

        return new Vector2(mapX, mapY);
    }

    void UpdateFoodIcon(
        RectTransform icon,
        Transform food
    )
    {
        icon.anchoredPosition =
            WorldToMapPosition(food.position);
    }
}