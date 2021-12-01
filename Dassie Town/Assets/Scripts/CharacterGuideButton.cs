using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterGuideButton : MonoBehaviour
{
    //For the Character Guide
    [Header("Character Bio")]
    public string charName;
    [TextArea(3, 10)]
    public string charBio;

    private Button _button;
    private TextMeshProUGUI _buttonText;
    private TextMeshProUGUI _guideText;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate { DisplayCharacter(); });

        _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        _guideText = GameObject.Find("GuideBodyText").GetComponent<TextMeshProUGUI>();

        _buttonText.text = charName;
    }

    public void DisplayCharacter()
    {
        _guideText.text = charBio;
    }
}
