using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    public float speed = 3f;          // ˆÚ“®‘¬“x
    public float distance = 5f;       // ã‰º‚ÉˆÚ“®‚·‚é‹——£

    private Vector3 startPosition;
    private bool goingUp = true;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (goingUp)
        {
            // ã‚ÉˆÚ“®
            transform.position += Vector3.up * speed * Time.deltaTime;

            // ˆê”Ôã‚É“ž’…
            if (transform.position.y >= startPosition.y + distance)
            {
                transform.position = new Vector3(
                    startPosition.x,
                    startPosition.y + distance,
                    startPosition.z
                );

                goingUp = false;
            }
        }
        else
        {
            // ‰º‚ÉˆÚ“®
            transform.position += Vector3.down * speed * Time.deltaTime;

            // ˆê”Ô‰º‚É“ž’…
            if (transform.position.y <= startPosition.y)
            {
                transform.position = startPosition;

                goingUp = true;
            }
        }
    }
}
