using UnityEngine;

public class InputVisual : MonoBehaviour
{
    public  SpriteRenderer sprite;
    public Sprite originalSprite;
    public Sprite conditionalSprite;

    private void Start()
    {
        originalSprite = sprite.sprite;
    }
}
