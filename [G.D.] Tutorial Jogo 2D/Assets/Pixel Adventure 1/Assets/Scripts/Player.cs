using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float Speed;
    public float JumpForce;
    public bool isJumping;
    public bool doubleJump;
    
    private Rigidbody2D rig;
    private Animator anim;

    bool isBlowing;

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
        float horizontal_input = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal_input, 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        
        if (horizontal_input > 0f)
        {
            // Olhando para a direita
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (horizontal_input < 0f)
        {
            // Olhando para a esquerda
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,180f, 0f);
        }
        else {
            // Parado
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isBlowing) 
        {
            if (!isJumping) 
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = true;
            anim.SetBool("jump", true);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            isBlowing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.layer == 8)
        {
            isBlowing = false;
        }
    }
}
