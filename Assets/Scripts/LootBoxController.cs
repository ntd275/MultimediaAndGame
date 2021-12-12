using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxController : MonoBehaviour
{
    public GameObject InSide;
    public Animator animator;
    public float PushForce = 1;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpen)
        {
            if(collision.gameObject.tag == "Player")
            {
                isOpen = true;
                animator.SetTrigger("Open");
                var inside = Instantiate(InSide, transform.position, Quaternion.identity);
                inside.GetComponent<Rigidbody2D>().AddForce(Vector2.up * PushForce, ForceMode2D.Impulse);
            }
        }
    }
}
