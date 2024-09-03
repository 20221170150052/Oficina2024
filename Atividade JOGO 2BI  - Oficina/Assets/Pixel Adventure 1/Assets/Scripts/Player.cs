using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    private Rigidbody2D rig;
    private Animator anim;
    public bool isJumping;
    public bool doubleJump;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),0f,0f);
        transform.position += movement * Time.deltaTime * Speed;
        if(Input.GetAxis("Horizontal")> 0f)
        {
            anim.SetBool("Bool Walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }
        if(Input.GetAxis("Horizontal")< 0f)
        {
            anim.SetBool("Bool Walk", true);
             transform.eulerAngles = new Vector3(0f,180f,0f);
        }
        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("Bool Walk", false);
        }
    }
    void Jump()
    {

    if(Input.GetButtonDown("Jump"))
    {
        if(!isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            doubleJump = true;
            anim.SetBool("Bool Jump", true);
        }
        else
        {
            if (doubleJump)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = false;
            }
        }
        
    }    
    }

    void OnCollisionEnter2D(Collision2D colisor)
    {
        if(colisor.gameObject.layer == 6)
        {
            isJumping = false;
            anim.SetBool("Bool Jump", false);
        }
        if(colisor.gameObject.tag == "Spike")
        {
          Debug.Log("Tocou o espinho!");
        }
    }

    void OnCollisionExit2D(Collision2D colisor )
    {
         if (colisor.gameObject.layer == 6)
        {
            isJumping = true;
        }
    }
}
