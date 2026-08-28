using UnityEngine;

public class Skill_UnCoCannon : MonoBehaviour
{
    Vector3 rotation = new Vector3(0, 0, 45);
    GameObject player;

    void Start()
    {
        rotation *= Time.deltaTime;
        transform.Rotate(rotation);
    }

    void Update()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        transform.position += rotation;
    }
}
