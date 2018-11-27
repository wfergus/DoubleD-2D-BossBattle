using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacter : MonoBehaviour {

public float accelerationForce = 5f;

	public float jumpForce = 5f;

    public float maxSpeed = 5f;

    public Rigidbody2D rb2d;

    public Collider2D playerGroundCollider;

    public PhysicsMaterial2D playerMovingPhysicsMaterial, playerStoppingPhysicsMaterial;

    public Collider2D groundDetectTrigger;

   public ContactFilter2D groundContactFilter;

    Animator anim;

    //private bool doubleJump = false;
    private bool facingRight = true;
    private float horizontalInput;
    private bool isOnGround;
    private Collider2D[] groundHitDetectionResults = new Collider2D[16];
    //public LayerMask whatIsGround;
    private Checkpoint currentCheckpoint;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateIsOnGround();
        UpdateHorizontalInput();
        HandleJumpInput();
        //winText.text = "";
        //SetCountText();

        //if ((isOnGround || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        //{
        //    anim.SetBool("Ground", false);
        //    rb2d.AddForce(new Vector2(0, jumpForce));

        //    if (!doubleJump && !isOnGround)
        //        doubleJump = true;
        //}
    }

    private void FixedUpdate()
    {
        UpdatePhysicsMaterial();
        Move();
        anim.SetFloat("vSpeed", Mathf.Abs(rb2d.velocity.y));
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(move));
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

    }

    private void UpdatePhysicsMaterial()
    {
        if (Mathf.Abs(horizontalInput) > 0)
        {
            playerGroundCollider.sharedMaterial = playerMovingPhysicsMaterial;
        }
        else
        {
            playerGroundCollider.sharedMaterial = playerStoppingPhysicsMaterial;
        }
    }

    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void UpdateIsOnGround()
    {
        isOnGround = groundDetectTrigger.OverlapCollider(groundContactFilter, groundHitDetectionResults) > 0;
        // Debug.Log("IsOnGround?: " + isOnGround);
    }

    private void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //if (isOnGround)
        //    doubleJump = false;
    }

    private void Move()
    {
        rb2d.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rb2d.velocity;
        clampedVelocity.x = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = clampedVelocity;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void Respawn()
    {
        if (currentCheckpoint == null)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
        {
            rb2d.velocity = Vector2.zero;
            transform.position = currentCheckpoint.transform.position;
        }
    }

    public void SetCurrentCheckpoint(Checkpoint newCurrentCheckpoint)
    {
        if (currentCheckpoint != null)
            currentCheckpoint.SetIsActivated(false);

        currentCheckpoint = newCurrentCheckpoint;
        currentCheckpoint.SetIsActivated(true);
    }
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("PickUp"))
    //    {
    //        other.gameObject.SetActive(false);
    //        count = count + 1;
    //        SetCountText();
    //    }
    //}
    //void SetCountText()
    //{
    //    countText.text = "Count: " + count.ToString();
    //    if (count >= 6)
    //    {
    //        winText.text = "You Win";
    //    }
    //}
}
