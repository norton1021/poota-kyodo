using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    public float radius = 1.5f;
    public int segments = 60;
    private LineRenderer line;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.loop = true;
        line.widthMultiplier = 0.05f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.red;
        line.endColor = Color.red;

        DrawCircle();
    }

    // Update is called once per frame
    void DrawCircle()
    {
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * Mathf.PI * 2f / segments;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            line.SetPosition(i, new Vector3(x, y, 0) + transform.position);
        }
    }
}
