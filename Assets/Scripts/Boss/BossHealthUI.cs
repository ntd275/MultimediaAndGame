using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHeath;
    private int curHeath;
    private GameObject boss;
    private Image fill;
    void Start()
    {
        boss = GameObject.Find("Boss");
        maxHeath = boss.GetComponent<BossCombat>().Health;
        fill = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        curHeath = boss.GetComponent<BossCombat>().Health;
        fill.fillAmount = (float)curHeath / maxHeath;
    }
}
