using UnityEngine;
using UnityEngine.InputSystem;  

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

   
    public InputActionReference moveAction;

    Rigidbody2D rb;
    Vector2 input;

    public Vector2 LastMoveDir { get; private set; } = Vector2.up;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;          
        rb.freezeRotation = true;
    }

    void OnEnable()
    {
        if (moveAction != null) moveAction.action.Enable();
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.action.Disable();
    }

    void Update()
    {
      
        input = moveAction != null ? moveAction.action.ReadValue<Vector2>() : ReadKeyboard();

        if (input.sqrMagnitude > 0.01f)
            LastMoveDir = input.normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime);
    }

  
    static Vector2 ReadKeyboard()
    {
        var k = Keyboard.current;
        if (k == null) return Vector2.zero;

        float x = (k.dKey.isPressed ? 1 : 0) - (k.aKey.isPressed ? 1 : 0);
        float y = (k.wKey.isPressed ? 1 : 0) - (k.sKey.isPressed ? 1 : 0);

        if (x == 0 && y == 0)
        {
            x = (k.rightArrowKey.isPressed ? 1 : 0) - (k.leftArrowKey.isPressed ? 1 : 0);
            y = (k.upArrowKey.isPressed ? 1 : 0) - (k.downArrowKey.isPressed ? 1 : 0);
        }

        return new Vector2(x, y);
    }
}
