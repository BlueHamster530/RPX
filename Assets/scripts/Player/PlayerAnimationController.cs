using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    Animator UpSideAnim;
    Animator DownSideAnim;
    // Start is called before the first frame update
    void Start()
    {
        UpSideAnim = transform.GetChild(0).GetComponent<Animator>();
        DownSideAnim = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetKeyDonw()
    {
        UpSideAnim.SetTrigger("Test");
        DownSideAnim.SetTrigger("Test");
    }

}
