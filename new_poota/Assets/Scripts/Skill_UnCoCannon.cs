using UnityEngine;

public class Skill_UnCoCannon : MonoBehaviour
{
    Vector3 rotation = new Vector3(0, 0, 90);
    int count = 0;
    GameObject player;

    void Start()
    {
       count++;
    }

    void Update()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
        transform.Rotate(rotation * Time.deltaTime);
    }
}
