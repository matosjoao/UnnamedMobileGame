using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Animator))]
public class HudManager : Singleton<HudManager>
{
    private Animator _transaction;
    private GameController _gameController;

    [SerializeField] private TMP_Text coinsTextField;
    [SerializeField] private TMP_Text lifesTextField;
    [SerializeField] float transactionTime = 1.0f;

    private void Awake()
    {
        _transaction = GetComponent<Animator>();
        _gameController = FindObjectOfType<GameController>();
    }

    public void SetCoins(string value)
    {
        coinsTextField.text = value;
    }

    public void SetLifes(string value)
    {
        lifesTextField.text = value;
    }

    public void LoadLevel()
    {
        // Ir buscar o level actual
        Level level = _gameController.GetCurrentLevel();

        // Iniciar level
        StartCoroutine(LoadLevel(level.LevelName));
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel("Menu"));
        
        // TODO::
        // Mostrar algo depois de concluir o nível
        // Animações Audio
    }

    IEnumerator LoadLevel(string levelName)
    {
        // Iniciar transição
        _transaction.SetTrigger("Start");
        // Aguardar animação
        yield return new WaitForSeconds(transactionTime);
        // Carregar novo nível
        SceneManager.LoadScene(levelName);
    }
}
