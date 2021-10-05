using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] float playerSpeed = 1f;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = _rb.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * playerSpeed;
        Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        _rb.MovePosition(newPos);
    }
}
