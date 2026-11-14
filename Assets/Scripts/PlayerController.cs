using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //---------PRIVATE COMPONENTS------
    private Rigidbody m_rigidbody;
    private bool m_isDead = false;
    private bool m_isBeingChased = false;

    //---------POLICE/BEING CHASED-------
    public GameObject m_police;
    private float timer = 10.0f;

    //--------PUBLIC COMPONENTS----------
    public float p_jumpHeight = 10.0f;

    //----------UI----------
    public TMP_Text p_coinCounterText;
    public TMP_Text p_scoreCounterText;

    public uint m_scoreCounter = 0;
    public uint m_coinCounter = 0;

    //-----input system-----
    public InputActionReference p_moveLeft;
    public InputActionReference p_moveRight;
    public InputActionReference p_jump;

    //-----------variables to do with moving the player between lanes--------------
    private float m_isJumping = 0;

    private float m_isLeft = 0;
    private bool m_canMoveLeft = true;
    private float m_isRight = 0;
    private bool m_canMoveRight = true;

    enum LANE { LEFT = 1, RIGHT = -1, MIDDLE = 0};

    private LANE m_lane = LANE.MIDDLE;
    private LANE m_moveLane = 0;

    private LANE m_previousPosition = LANE.MIDDLE;



// Start is called once before the first execution of Update after the MonoBehaviour is created
void Start()
    {
        //m_input = GetComponent<InputActionReference>();
        m_rigidbody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        //--------------MOVEMENT-------------------

        //MOVE LEFT
        m_isLeft = p_moveLeft.action.ReadValue<float>();

        if(m_isLeft == 1 && m_canMoveLeft && m_moveLane <= LANE.LEFT)
        {
            m_previousPosition = m_lane;

            m_moveLane += 1;
            m_canMoveLeft = false;
            Debug.Log(m_moveLane.ToString());

        }
        else if(m_isLeft == 0)
        {
            m_canMoveLeft = true;
        }



        //MOVE RIGHT
        m_isRight = p_moveRight.action.ReadValue<float>();

        if (m_isRight == 1 && m_canMoveRight && m_moveLane >= LANE.RIGHT)
        {
            m_previousPosition = m_lane;

            m_moveLane -= 1;
            m_canMoveRight = false;

        }
        else if (m_isRight == 0)
        {
            m_canMoveRight = true;
        }



        if((int)m_lane + (int)m_moveLane <= 1 && (int)m_lane + (int)m_moveLane >= -1)
        {
            m_lane += (int)m_moveLane;
        }
        else
            Debug.Log((int)m_lane );

        m_moveLane = 0;



        //----------update UI-------------
        //update score
        m_scoreCounter += 1;

        updateUI();

        //-------------UPDATE DEATH / BEING CHASED-----------------
        Vector3 targetLocation = (transform.position + new Vector3(0, 3, -8));

        if (m_isBeingChased)
        {
            //move police towards desired intimidating location
            m_police.transform.position = Vector3.MoveTowards(m_police.transform.position, targetLocation, 10 * Time.deltaTime);
            timer -= Time.deltaTime;
            Debug.Log("TIMER going down" + timer);

            //set timer and reset
            if (timer <= 0.0f)
            {
                m_isBeingChased = false;
                timer = 10.0f;
                Debug.Log("TIMER RESET");
            }
        
        
        }
        else
        {
            m_police.transform.position = Vector3.MoveTowards(m_police.transform.position, targetLocation - new Vector3(0, 0, 10), 10 * Time.deltaTime);

        }
    }

    //------------UPDATE USER INTERFACE------------

    void updateUI()
    {
        p_coinCounterText.text = m_coinCounter.ToString();
        p_scoreCounterText.text = m_scoreCounter.ToString();
    }

    //----------------UPDATE PHYSICS-----------------
    private float prevY = 0;
    private float deltaY = 0;
    void FixedUpdate()
    {
        deltaY = prevY - transform.position.y;
        prevY = transform.position.y;

        //JUMP
        m_isJumping = p_jump.action.ReadValue<float>();

        if (m_isJumping == 1.0f && deltaY == 0)
        {
            m_rigidbody.AddForce(0.0f, p_jumpHeight, 0.0f, ForceMode.Impulse);
        }


        //Move the rigidbody towards the direction
        float direction = 0;
        switch (m_lane)
        {
            case LANE.LEFT:
                direction = -10.0f - m_rigidbody.position.x;
                break;

            case LANE.MIDDLE:
                direction = 2.50f - m_rigidbody.position.x;
                break;

            case LANE.RIGHT:
                direction = 11.0f - m_rigidbody.position.x;
                break;

        }
        Vector3 pos = Vector3.zero;
        pos.x = direction * 8.0f;
        transform.position += pos * Time.deltaTime;

    }




    //---------------COLLISION RESPONSES--------------------

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {

            if (m_isBeingChased)
            {
                m_isDead = true;
                //isDead will execute some sort of death animation.



                //temporary Destory function since we dont have 
                Destroy(gameObject);

            }
            else
            {
                m_isBeingChased = true;
                //previous position
                m_lane = m_previousPosition;

                switch(m_previousPosition)
                {
                   case LANE.LEFT: transform.position = new Vector3(-10, transform.position.y, transform.position.z);
                        m_lane = LANE.LEFT;
                        Debug.Log("IS BEING TELEPORTED PREOBABLY" + m_previousPosition);

                        break;
                   case LANE.MIDDLE: transform.position = new Vector3(0, transform.position.y, transform.position.z);
                        m_lane = LANE.MIDDLE;
                        Debug.Log("IS BEING TELEPORTED PREOBABLY" + m_previousPosition);

                        break;
                   case LANE.RIGHT: transform.position = new Vector3(11, transform.position.y, transform.position.z);
                        m_lane = LANE.RIGHT;
                        Debug.Log("IS BEING TELEPORTED PREOBABLY" + m_previousPosition);

                        break;

                }
       
                //Debug.Log("IS BEING TELEPORTED PREOBABLY" + m_previousPosition);
                //transform.position = new Vector3(0, transform.position.y + 20, transform.position.z);

            }
                m_isBeingChased = true;
            
        }
        if (collision.gameObject.CompareTag("FrontOfObstacle"))
        {
            m_isDead = true;
            Debug.Log("INSTA KILLLL");
            Debug.Log("INSTA KILLLL");
            //call death function
            Destroy(gameObject);
        }
    }

  

}
