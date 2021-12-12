using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public int total;
    public Text text;
    private void Start()
    {
        total = PlayerPrefs.GetInt("Coin");
        text = transform.Find("Text").GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        text.text = total.ToString();
    }
}
