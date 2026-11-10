using UnityEngine;
using UnityEngine.UIElements;

public class Collectable : MonoBehaviour
{
    //--------------private Vars--------------

    public bool m_isDestroyed = false;
    public float m_rotationSpeed = 1.0f;
    public AudioClip coinsound;

    float m_initVerticlePos = 0.0f;
    AudioSource source;
    GameObject coin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coin = GameObject.Find("AudioObject");

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
            m_rotationSpeed += 1f;

        }
        if(m_isDestroyed && transform.position.y > m_initVerticlePos + 0.1)
        {
            Destroy(this.gameObject);
            source.clip = coinsound;
            source.Play();
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
