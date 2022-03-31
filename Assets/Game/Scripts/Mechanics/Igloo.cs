using UnityEngine;
using UnityEngine.SceneManagement;

public class Igloo : MonoBehaviour
{
    private GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerControl playerControl = collision.GetComponent<PlayerControl>();
            CharacterMovement characterMovement = collision.GetComponent<CharacterMovement>();
            if (playerControl != null && characterMovement != null)
            {
                // Parar o Player
                characterMovement.StopImmediately();

                // Completar nível no GameController
                Scene scene = SceneManager.GetActiveScene();
                _gameController.CompleteLevel(scene.name);

                // Adicionar points aos points atuais e vidas no GameController
                _gameController.UpdatePlayerStatus(playerControl.Lifes, playerControl.Coins);

                // Ir para o ecrã principal
                HudManager.Instance.LoadMenu();
            }
        }
    }
}
