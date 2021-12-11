using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueController : MonoBehaviour
{
    public GameObject ContinueButton;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("Level") != "")
        {
            ContinueButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
