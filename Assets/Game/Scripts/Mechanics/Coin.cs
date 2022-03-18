using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinAmount = 1;
        
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.transform.tag == "Player")
        {
            PlayerControl playerControl = collision.GetComponent<PlayerControl>();
            if(playerControl != null)
            {
                playerControl.AddCoins(coinAmount);
            }
            Destroy(gameObject);
        }
    }
}
