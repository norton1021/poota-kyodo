using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject mapPanel;
    public Transform player;
    public RectTransform playerIcon;
    public Vector2 mapMin;
    public Vector2 mapMax;
void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mapPanel.SetActive(!mapPanel.activeSelf);
        }
        if (mapPanel.activeSelf)
        {
            UpdatePlayerPosition();
        }
    }
    void UpdatePlayerPosition()
    {
        float x = Mathf.InverseLerp(mapMin.x, mapMax.x, player.position.x);
        float y = Mathf.InverseLerp(mapMin.y, mapMax.y, player.position.y);
        RectTransform mapRect = mapPanel.GetComponent<RectTransform>();
        float mapX = (x - 0.5f) * mapRect.rect.width;
        float mapY = (y - 0.5f) * mapRect.rect.height;
        playerIcon.anchoredPosition = new Vector2(mapX, mapY);
    }
    }