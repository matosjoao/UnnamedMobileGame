using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class MenuManager : Singleton<MenuManager>
{
    private GameController _gameController;
    private Animator _transaction;

    [Header("Player Status")]
    [SerializeField] private TMP_Text scoreTextField;
    [SerializeField] private TMP_Text lifesTextField;

    [Header("Completed Level Panel")]
    [SerializeField] private TMP_Text levelScoreTextField;
    [SerializeField] private GameObject completedLevelPanel;
    [SerializeField] private ParticleSystem completedLevelEffect;

    private void Awake()
    {
        _transaction = GetComponent<Animator>();
        _gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        if(_gameController != null)
        {
            int score = _gameController.Score;
            int lifes = _gameController.Lifes;

            SetScore(score.ToString());
            SetLifes(lifes.ToString());
        }
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

    public void StarGame()
    {
        _gameController.LoadLevel();
    }

    // TODO::
    // Mostrar algo depois de concluir o nível
    // Animações Audio

    //levelScoreTextField.text = levelScore.ToString();
    //completedLevelPanel.SetActive(true);
    //completedLevelEffect.gameObject.SetActive(true);
    //completedLevelEffect.Play();
}
