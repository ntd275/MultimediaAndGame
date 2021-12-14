using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLv : MonoBehaviour
{
    public AudioSource FinishSound;
    public string NextLv;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FinishSound.Play();
            PlayerPrefs.SetString("Level", "");
            collision.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            StartCoroutine(NextScene(1));
        }
    }

    IEnumerator NextScene(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(NextLv);
    }
}