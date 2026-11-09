using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour
{
    public float speed = 1.0f;
    public Vector3 direction = new Vector3(0, 0, -1);
    //potential next objexts
    public GameObject obstacle;
    public GameObject obstacle2;
    
    private Vector3 startPos;
    private int randomLane;
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
            if (transform.position.z <= 60)
            {
                int randomObstacle = Random.Range(0, 2);
                if (randomObstacle == 0)
                {
                    Instantiate(obstacle, new Vector3(0, 0, 80), Quaternion.identity);
                }
                else
                {
                    Instantiate(obstacle2, new Vector3(0, 0, 80), Quaternion.identity);
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
