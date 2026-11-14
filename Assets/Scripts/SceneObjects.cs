using UnityEngine;
using UnityEngine.UIElements;

public class SceneObjects : MonoBehaviour
{
    //----------PUBLIC VARS-----------
    public float p_movingSpeed = 20.0f;
    public float m_frontOfScene_z = 65.0f;

    public float m_backOfScene_z = -9.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position -= new Vector3(0.0f, 0.0f, p_movingSpeed * Time.deltaTime);

        if(transform.position.z < m_backOfScene_z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, m_frontOfScene_z);
        }
    }
}
