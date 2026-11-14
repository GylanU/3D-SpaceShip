using UnityEngine;
using UnityEngine.UIElements;

public class Collectable : MonoBehaviour
{
    //--------------private Vars--------------

    public bool m_isDestroyed = false;
    public float m_rotationSpeed = 1.0f;
    public AudioClip clip;

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
            AudioSource.PlayClipAtPoint(clip, transform.position);
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player") && !m_isDestroyed)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.m_coinCounter += 1;
            m_isDestroyed = true;
        }
    }

}
