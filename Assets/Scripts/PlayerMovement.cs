using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float swingSpeed;
    public float grappleSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;



    [Header("Camera Effects")]
    public PlayerCamera cam;
    public float grappleFov = 95f;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        grappling,
        swinging,
        walking,
        air
    }

    public bool freeze;

    public bool activeGrapple;
    public bool swinging;


    //health things
    int hp = 5;
    float goodSpd = 10f;
    int hitCooldownTimer = 0;
    int healTimer;
    int timerMax = 100;
    bool isHit = false;
    public Graphic hurtOverlay;

    public AudioClip enemyDieAudio;
    public AudioClip hurtAudio;
    public AudioClip dieAudio;
    AudioSource src;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        src = GetComponent<AudioSource>();

    }

    private void Update()
    {
        TextStuff();
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        // handle drag
        if (grounded && !activeGrapple)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //TextStuff();
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (isHit)
        {
            if (hitCooldownTimer < timerMax)
            {
                hitCooldownTimer++;
            }
            else
            {
                hitCooldownTimer = 0;
                isHit = false;
            }
            
        }
        else if (hp < 5)
        {
            if (healTimer < timerMax * 3) //healing takes longer than iframes
            {
                healTimer++;
            }
            else
            {
                hp++;
                healTimer = 0;
            }
        }
        float alpha = (5f - hp) * 0.2f;
        hurtOverlay.color = new Color(1f, 1f, 1f, alpha);


        if (hp < 1 && !src.isPlaying)
        {
            GameOver();
            //StartCoroutine(GameOver());
        }
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        
    }

    private void StateHandler()
    {
        // Mode - Freeze
        if (freeze)
        {
            state = MovementState.freeze;
            moveSpeed = 0;
            rb.velocity = Vector3.zero;
        }

        // Mode - Grappling
        else if (activeGrapple)
        {
            state = MovementState.grappling;
            moveSpeed = grappleSpeed;
        }

        // Mode - Swinging
        else if (swinging)
        {
            state = MovementState.swinging;
            moveSpeed = swingSpeed;
        }              

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        if (activeGrapple) return;
        if (swinging) return;

        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

   

        // on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

        // in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);


    }

    private void SpeedControl()
    {
        if (activeGrapple) return;

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump()
    {

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

        //cam.DoFov(grappleFov);
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
        //cam.DoFov(85f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();

            GetComponent<Grappling>().StopGrapple();
        }
    }

    public bool GotHit(bool isLaser) //called by sendMessage in enemy scripts; returns bool that det if enemy dies
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (Round(flatVel.magnitude, 1) < goodSpd && !isLaser)
        {
            if (!isHit && !isLaser)
            {
                hitCooldownTimer = 0;
                healTimer = 0;
                hp -= 1;
                if (hp < 1)
                {
                    src.clip = dieAudio;
                    GetComponent<AudioSource>().Play();
                    return false;
                }
                src.clip = hurtAudio;
                GetComponent<AudioSource>().Play(); //hit sfx
                isHit = true;
            }
            return false;
        }
        else if (!isLaser)
        {
            src.clip = enemyDieAudio;
            GetComponent<AudioSource>().Play();
            src.Play();
            return true;
        }
        else if (!isHit)
        {
            hitCooldownTimer = 0;
            healTimer = 0;
            hp -= 1;
            src.clip = hurtAudio;
            GetComponent<AudioSource>().Play(); //hit sfx
            isHit = true;
            return false;
        }
        else
        {
            return false;
        }
    }

    void GameOver()
    {
        //yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }

    #region Text & Debugging
    
    public TextMeshProUGUI text_speed;
    private void TextStuff()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        text_speed.SetText("Speed: " + Round(flatVel.magnitude, 1));
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }
    
    #endregion

}