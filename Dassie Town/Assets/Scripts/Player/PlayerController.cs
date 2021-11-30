using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float playerSpeed = 1f;

    private Rigidbody2D _rb;
    private Animator _animator;

    private bool _facingLeft;

    public bool freezeMovement;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!freezeMovement)
        {
            Vector2 currentPos = _rb.position;
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * playerSpeed;
            Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
            _rb.MovePosition(newPos);

            if (horizontalInput != 0 || verticalInput != 0)
            {
                _animator.SetTrigger("Move");
            }
            else
            {
                _animator.SetTrigger("Idle");
            }

            //For setting the player direction based on mouse
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            Vector3 charScale = transform.localScale;
            if(mousePos.x > newPos.x)
            {
                charScale.x = -1;
            }
            else if(mousePos.x < newPos.x)
            {
                charScale.x = 1;
            }
            transform.localScale = charScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickup")
        {
            GetComponentInParent<PipeBuilder>().AddPipes(collision.GetComponent<Pickup>().addedPipes);

            Destroy(collision.gameObject);
        }
    }
}
