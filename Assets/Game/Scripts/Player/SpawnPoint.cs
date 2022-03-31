using UnityEngine;
using Cinemachine;

public class SpawnPoint : MonoBehaviour
{
    private GameController _gameController;

    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void Start()
    {
        GameObject playerObj = Instantiate(player, transform.position, Quaternion.identity);
        playerObj.SetActive(true);
        playerObj.GetComponent<PlayerControl>().Lifes = _gameController.Lifes;

        Transform childTrans = playerObj.transform.Find("CameraTarget");
        if (childTrans != null)
        {
            foreach(CinemachineVirtualCamera cam in cameras)
            {
                cam.Follow = childTrans;
            }
        }
    }
}
