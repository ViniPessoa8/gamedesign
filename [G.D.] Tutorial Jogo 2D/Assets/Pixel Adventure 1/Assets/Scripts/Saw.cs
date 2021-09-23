using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public float speed;
    public float moveTime;

    private bool dirRight;
    private float timer;

    private GameObject player;

    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Tocou o espinho!");
            GameController.instance.ShowGameOver();
            Destroy(player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }   
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        } 

        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            dirRight = !dirRight;
            timer = 0f;
        }
    }
}
