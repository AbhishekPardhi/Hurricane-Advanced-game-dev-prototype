using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public Animator Anim;
    public movement MovScript;

    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        /*  Anim.SetBool("isWalking", MovScript.walk);



          if(Input.GetButtonDown("Jump") && MovScript.isGrounded)
          {
              Anim.SetBool("isJumping", true);
          }
          else
          {
              Anim.SetBool("isJumping", false);
          }

          Anim.SetBool("isRunning", MovScript.running);*/
        if (Input.GetButtonDown("Jump") && MovScript.isGrounded)
        {
            Anim.SetBool("jump", true);
        }
        else
        {
            Anim.SetBool("jump", false);
        }
        Anim.SetFloat("Vertical", vertical);
        Anim.SetFloat("Horizontal", horizontal);
       
       
    }
}
