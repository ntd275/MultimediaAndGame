using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private GameObject coinUI;
    private void Start()
    {
        coinUI = GameObject.Find("CoinUI");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            coinUI.GetComponent<CoinUI>().total++;
            var animator = gameObject.GetComponent<Animator>();
            animator.Play("Coin_Pick");
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
        }
    }
}
