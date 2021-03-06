using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLv1 : MonoBehaviour
{
    public AudioSource FinishSound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            FinishSound.Play();
            PlayerPrefs.SetString("Level", "");
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            PlayerPrefs.SetInt("Coin",GameObject.Find("CoinUI").GetComponent<CoinUI>().total);
            PlayerPrefs.SetInt("Health", collision.gameObject.GetComponent<PlayerCombat>().Health);
            StartCoroutine(NextScene(1));
        }
    }

    IEnumerator NextScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene("Level2");
    }
}
