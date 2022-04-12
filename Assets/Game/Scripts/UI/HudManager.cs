using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class HudManager : Singleton<HudManager>
{
    private Animator _transaction;
    
    [Header("Player Status")]
    [SerializeField] private TMP_Text scoreTextField;
    [SerializeField] private TMP_Text lifesTextField;

    private void Awake()
    {
        _transaction = GetComponent<Animator>();
    }

    public void SetScore(string value)
    {
        scoreTextField.text = value;
    }

    public void SetLifes(string value)
    {
        lifesTextField.text = value;
    }

    public void TriggerLevelTransaction()
    {
        _transaction.SetTrigger("Start");
    }
}
