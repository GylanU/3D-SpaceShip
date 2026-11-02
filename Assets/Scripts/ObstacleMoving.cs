using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 direction = new Vector3(0, 0, -1);
    private Vector3 startPos;
    private int randomLane;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;

        direction = direction.normalized;
        randomLane = Random.Range(1, 4);
        if (randomLane == 1)
        {
            startPos.x = -10.0f;
            transform.position = startPos;
        }
        else if (randomLane == 2)
        {
            startPos.x = 0.0f;
            transform.position = startPos;
        }
        else if (randomLane == 3)
        {
            startPos.x = 10.0f;
            transform.position = startPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position += direction * step;
        if (transform.position.z <= -10)
        {
            randomLane = Random.Range(1, 4);
            if (randomLane == 1)
            {
                startPos.x = -10.0f;
                transform.position = startPos + direction * step;
            }
            else if (randomLane == 2)
            {
                startPos.x = 1.0f;
                transform.position = startPos + direction * step;
            }
            else if (randomLane == 3)
            {
                startPos.x = 10.0f;
                transform.position = startPos + direction * step;
            }
        }


    }
}
