using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System.Collections;

using static System.Net.Mime.MediaTypeNames;

public class PlayerController : MonoBehaviour
{
    //---------PRIVATE COMPONENTS------
    private Rigidbody m_rigidbody;

    //--------PUBLIC COMPONENTS----------
    public float p_jumpHeight = 3.0f;

    //----------UI----------
    public UnityEngine.UI.Text p_coinCounterText;
    public UnityEngine.UI.Text p_scoreCounterText;

    private uint m_scoreCounter = 0;
    private uint m_coinCounter = 0;

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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //m_input = GetComponent<InputActionReference>();
        m_rigidbody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame

    void Update()
    {

        //MOVE LEFT
        m_isLeft = p_moveLeft.action.ReadValue<float>();

        if(m_isLeft == 1 && m_canMoveLeft && m_moveLane <= LANE.LEFT)
        {
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
        updateUI();
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
                direction = 0.0f - m_rigidbody.position.x;
                break;

            case LANE.RIGHT:
                direction = 10.0f - m_rigidbody.position.x;
                break;

        }
        Vector3 pos = Vector3.zero;
        pos.x = direction / 8.0f;
        transform.position += pos;

    }




    //---------------COLLISION RESPONSES--------------------

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //play death animation
            //-TO DO

            //destory object after a few second
            Destroy(gameObject);
        }
    }
}
