using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 direction = new Vector3(0, 0, -1);

    //potential next objexts
    public GameObject obstacle;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;
    public GameObject obstacle5;

    private Vector3 startPos;
    private bool SpawnFlag = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = new Vector3(0, 0, 80);

        direction = direction.normalized;
        
        startPos.x = 0.0f;
        transform.position = startPos;
       
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position += direction * step;
        if (!SpawnFlag)
        {
            if (transform.position.z <= 55)
            {
                //spawn random new obstacle from selection
                //very basic wave function collapse

                int randomObstacle = Random.Range(0, 5);

                if (randomObstacle == 0)
                {
                    Instantiate(obstacle, startPos, Quaternion.identity);
                }
                else if (randomObstacle == 1)
                {
                    Instantiate(obstacle2, startPos, Quaternion.identity);
                }
                else if (randomObstacle == 2)
                {
                    Instantiate(obstacle3, startPos, Quaternion.identity);
                }
                else if (randomObstacle == 3)
                {
                    Instantiate(obstacle4, startPos, Quaternion.identity);
                }
                else
                {
                    Instantiate(obstacle5, startPos, Quaternion.identity);
                }

                SpawnFlag = true;
            }
        }
        
        
        if (transform.position.z <= -20)
        {
            Destroy(gameObject);
           
        }


    }
}
