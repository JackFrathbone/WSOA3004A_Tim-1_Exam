using UnityEngine;
using TMPro;

public class Pickup : MonoBehaviour
{
    public int addedPipes;

    private TextMeshPro _text;

    private void Start()
    {
        _text = GetComponentInChildren<TextMeshPro>();

        _text.text = addedPipes.ToString();
    }
}
