using UnityEngine;
using Cinemachine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private CinemachineVirtualCamera[] cameras;

    private void Start()
    {
        GameObject playerObj = Instantiate(player, transform.position, Quaternion.identity);
        playerObj.SetActive(true);
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
