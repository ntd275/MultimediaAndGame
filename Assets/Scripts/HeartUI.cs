using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public GameObject[] hearts;
    private GameObject player;
    private int total = 3;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player)
        {
            var health = player.GetComponent<PlayerCombat>().Health;
            total = health;
            for(int i = 0; i < 3; i++)
            {
                if(i < health)
                {
                    hearts[i].GetComponent<Image>().enabled = true;
                }
                else
                {
                    hearts[i].GetComponent<Image>().enabled = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            var health = player.GetComponent<PlayerCombat>().Health;
            if (health != total)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i < health)
                    {
                        hearts[i].GetComponent<Image>().enabled = true;
                    }
                    else
                    {
                        hearts[i].GetComponent<Image>().enabled = false;
                    }
                }
            }
        }
    }
}
