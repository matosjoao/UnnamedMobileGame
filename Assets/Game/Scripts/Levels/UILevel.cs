using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UILevel : MonoBehaviour
{
    [SerializeField] private TMP_Text levelIdText;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private Transform starParent;

    private Image[] _stars;

    private void Awake()
    {
        _stars = starParent.GetComponentsInChildren<Image>();
    }

    public void SetStars(int score)
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            if (i < score)
            {
                _stars[i].color = Color.white;
            }
            else
            {
                _stars[i].color = new Color32(91, 91, 91, 123);
            }
        }
    }

    public void Lock()
    {
        lockImage.SetActive(true);
        levelIdText.gameObject.SetActive(false);
        levelIdText.text = "";
    }

    public void UnLock()
    {
        lockImage.SetActive(false);
        levelIdText.gameObject.SetActive(true);
    }

    public void SetLevelName(string name)
    {
        levelIdText.text = name;
    }
}
