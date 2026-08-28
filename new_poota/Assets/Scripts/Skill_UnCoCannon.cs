using UnityEngine;

public class Skill_UnCoCannon : MonoBehaviour
{
    int rotation = 0;
    GameObject player;

    void Start()
    {
        this.rotation = (int)Time.deltaTime % 8;
    }

    void Update()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        switch (this.rotation)
        {
            case 0:
                transform.Translate(1, 0, 0);
                break;
            case 1:
                transform.Translate(1, 1, 0);
                break;
            case 2:
                transform.Translate(0, 1, 0);
                break;
            case 3:
                transform.Translate(-1, 1, 0);
                break;
            case 4:
                transform.Translate(-1, 0, 0);
                break;
            case 5:
                transform.Translate(-1, -1, 0);
                break;
            case 6:
                transform.Translate(0, -1, 0);
                break;
            case 7:
                transform.Translate(1, -1, 0);
                break;
        }
    }
}
