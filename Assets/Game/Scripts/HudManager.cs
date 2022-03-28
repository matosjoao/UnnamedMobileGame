using UnityEngine;
using TMPro;

public class HudManager : Singleton<HudManager>
{
    [SerializeField] private TMP_Text coinsTextField;
    [SerializeField] private TMP_Text lifesTextField;

    public void SetCoins(string value)
    {
        coinsTextField.text = value;
    }
    public void SetLifes(string value)
    {
        lifesTextField.text = value;
    }
}
