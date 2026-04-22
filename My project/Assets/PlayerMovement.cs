using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    public float moveSpeed = 5f;
    public Color normalColor = Color.white;
    public Color collectColor = Color.green;
    public Color damageColor = Color.red;

    // input action reference (can be assigned in inspector)
    public InputActionProperty horizontalMove;
    public InputActionProperty verticalMove;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = normalColor;
    }

    void OnEnable()
    {
        horizontalMove.action.Enable();
        verticalMove.action.Enable();
    }

    void OnDisable()
    {
        horizontalMove.action.Disable();
        verticalMove.action.Disable();
    }

    void FixedUpdate()
    {
        if (GameController.isGameOver) 
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float moveX = 0;
        float moveY = 0;

        if (horizontalMove.action != null) moveX = horizontalMove.action.ReadValue<float>();
        if (verticalMove.action != null) moveY = verticalMove.action.ReadValue<float>();

        // Legacy/Direct support as fallback or additional check if actions aren't bound
        if (moveX == 0 && moveY == 0 && Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveY = 1;
            else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveY = -1;
            
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1;
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1;
        }
        
        // Gamepad support if actions aren't bound
        if (moveX == 0 && moveY == 0 && Gamepad.current != null)
        {
            Vector2 stick = Gamepad.current.leftStick.ReadValue();
            moveX = stick.x;
            moveY = stick.y;
        }

        Vector2 movement = new Vector2(moveX, moveY);
        if (movement.magnitude > 1) movement.Normalize();

        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Survival only: removing item collection
        /*
        if (collision.tag == "coletavel")
        {
            if(audioSource) audioSource.Play();
            GameController.collect();
            StartCoroutine(FlashColor(collectColor));
            Destroy(collision.gameObject);
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("inimigo"))
        {
            if(audioSource) audioSource.Play(); // Play hit sound
            StartCoroutine(FlashColor(damageColor));
            GameController.LoseLife();
        }
    }

    System.Collections.IEnumerator FlashColor(Color color)
    {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = normalColor;
    }
}
