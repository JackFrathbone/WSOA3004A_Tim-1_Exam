using UnityEngine;

public enum Direction
{
    LeftUp,
    LeftDown,
    RightUp,
    RightDown
};

public class OutputOutlet : MonoBehaviour
{
    public Direction outletDirection;
    public Sprite LeftUp, leftDown, rightUp, rightDown;

    public OutputMain masterOutput;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        switch (outletDirection)
        {
            case Direction.LeftUp:
                _spriteRenderer.sprite = LeftUp;
                break;
            case Direction.LeftDown:
                _spriteRenderer.sprite = leftDown;
                break;
            case Direction.RightUp:
                _spriteRenderer.sprite = rightUp;
                break;
            case Direction.RightDown:
                _spriteRenderer.sprite = rightDown;
                break;
            default:
                Debug.Log("Switch Error in Outlet");
                break;
        }
    }
}
