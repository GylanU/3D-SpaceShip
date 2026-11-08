using UnityEngine;
using UnityEngine.UIElements;

public class Collectable : MonoBehaviour
{
    //--------------private Vars--------------

    public bool m_isDestroyed = false;
    public float m_rotationSpeed = 1.0f;

    float m_initVerticlePos = 0.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_initVerticlePos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, m_rotationSpeed, 0.0f, Space.World);
    }

    void FixedUpdate()
    {
        if(m_isDestroyed)
        {
            transform.position = transform.position += new Vector3(0.0f, 2.0f * Time.deltaTime, 0.0f);
            m_rotationSpeed += 0.4f;

        }
        if(m_isDestroyed && transform.position.y > m_initVerticlePos + 1.2)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("PLAYER INTERSECTED EWITH COIN\n");


        if (collision.gameObject.CompareTag("Player") && !m_isDestroyed)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.m_coinCounter += 1;
            m_isDestroyed = true;
        }
    }

}
