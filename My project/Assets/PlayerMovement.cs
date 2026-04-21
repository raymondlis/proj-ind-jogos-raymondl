  using UnityEngine;
using UnityEngine.InputSystem; // 1. Adicione esta linha para usar o novo sistema

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    AudioSource audio;

    public float moveSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        // 2. Substituímos o Input.GetAxis por uma leitura direta do teclado
        Vector2 movement = Vector2.zero;

        // Verifica se existe um teclado conectado
        if (Keyboard.current != null)
        {
            // Movimento Vertical (W/S ou Setas)
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) movement.y = 1;
            else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) movement.y = -1;

            // Movimento Horizontal (A/D ou Setas)
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) movement.x = 1;
            else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) movement.x = -1;
        }

        // 3. Normalizamos o vetor para que o personagem não ande mais rápido na diagonal
        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }

        // Mantendo sua lógica original de movimento
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "coletavel")
        {
            audio.Play();
            GameController.collect();
            Destroy(collision.gameObject);
        }
    }
}
