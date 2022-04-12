using UnityEngine;

public class Igloo : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerControl playerControl = collision.GetComponent<PlayerControl>();
            if (playerControl != null)
            {
                playerControl.OnFinishLevel();
            }
        }
    }
}
