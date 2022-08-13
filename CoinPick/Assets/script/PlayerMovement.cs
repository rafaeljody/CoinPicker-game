using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float ms; // movement speed
    public float jf; // jump force

    //scoring system
    int coinValues = 0;
    public Text score;

    // layer mask -> layers to use in raycast
    public LayerMask ground;


    // exit door
    public GameObject exitDoor;

    void Start()
    {
        // get component
        rb = GetComponent<Rigidbody2D>();
        exitDoor.SetActive(false);
    }

    void Update()
    {
        // movement
        float horiz = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horiz * ms, rb.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetMouseButtonDown(0))
        {
            if (IsGrounded())
            {
                rb.AddForce(new Vector2(0, jf));
            }
            else
            {
                return;
            }
        }

        // exit doors
        if(coinValues == 10)
        {
            exitDoor.SetActive(true);
        }

        
    }

    // player hit coin -> values++
    // player hit diamond -> destroy player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Coin")
        {
            coinValues++;
            score.text = coinValues.ToString();
            Debug.Log(coinValues);
        } else if (collision.gameObject.tag == "End")
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }

    }


    // make it just 1 jump
    bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;


        //raycast
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, ground); 


        // if hit something, return this method as true (isGrounded)
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }

}
