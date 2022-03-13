using UnityEngine;
using TMPro;

public class LevelPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text _popupText;

    public void SetText(string text)
    {
        _popupText.text = text;

        gameObject.SetActive(true);
    }
}
