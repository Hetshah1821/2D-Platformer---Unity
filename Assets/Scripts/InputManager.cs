using UnityEngine;

public class InputManager : MonoBehaviour {
    
    [SerializeField] private float characterSpeed = 5f;
    [SerializeField] private float jumpPower = 12f ;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int extraJumps;
    [SerializeField] private float walljumpX;
    [SerializeField] private float walljumpY;
    private Rigidbody2D body;
    private Animator animate;
    private BoxCollider2D boxCollider;
    private float wallJump;
    private float horizontalInput; 
    private float coyoteCounter;
    private float jumpCounter;
    

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate() 
    {
        InputSystem();
    }

    private void InputSystem() 
    {
         horizontalInput = Input.GetAxis("Horizontal");
        

        
        if (horizontalInput > 0.01f) 
        {
            transform.localScale = Vector3.one;
        } 
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        animate.SetBool("run", horizontalInput != 0);
        animate.SetBool("grounded", IsGrounded());

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0) 
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        }

        if (OnWall()) 
         {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        } else 
        {
            body.gravityScale = 7;
            body.linearVelocity = new Vector2(horizontalInput * characterSpeed, body.linearVelocity.y);
        }

        if (IsGrounded()) 
        {
            coyoteCounter = coyoteTime;
            jumpCounter = extraJumps;
        } else 
        {
            coyoteCounter -= Time.deltaTime; 
        }
    }

    private void Jump() 
    {
        if (coyoteCounter < 0 && !OnWall() && jumpCounter <= 0) 
        {
            return;
        }
        SoundManager.instance.Playsound(jumpSound);

        if(OnWall())
        {
          WallJump();
        }
        else
        {
            if (IsGrounded()) 
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            } 
            else 
            {
                if (coyoteCounter > 0) 
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                } else 
                {
                    if (jumpCounter > 0) 
                    {
                        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
                    coyoteCounter = 0;
            }
        }
    }

    private void WallJump() 
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * walljumpX, walljumpY));
        wallJump = 0;
    }
    private bool IsGrounded() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,0,Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null ;
    }

    private bool OnWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    { 
      return horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}


