using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite SwitchOff;
    public Sprite SwitchOn;
    public bool IsOn;
    public SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOn)
        {
            sr.sprite = SwitchOn;
        }
        else
        {
            sr.sprite = SwitchOff;
        }
    }
}
