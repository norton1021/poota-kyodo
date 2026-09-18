using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    public float riseSpeed = 3f;
    public float riseDistance = 5f;

    private Vector3 startPosition;
    private bool rising = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (rising)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;

            // Žw’è‚µ‚½‹——£‚Ü‚Åã‚ª‚Á‚½‚ç’âŽ~
            if (transform.position.y >= startPosition.y + riseDistance)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    startPosition.y + riseDistance,
                    transform.position.z
                );

                rising = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rising = true;
        }
    }
}
