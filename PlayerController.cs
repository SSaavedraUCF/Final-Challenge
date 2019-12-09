using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d; //physics
    private int count;
    private int lives;
    private float currentTime = 60f;
    private float startingTime= 60f;


    public Text countText; //text related
    public Text winText;
    public Text livesText;
    public Text countdownText;
   
    public float speed;
  


    public AudioSource musicSource;
    public AudioSource effects;
    public AudioClip musicClipOne; //holder for variable 1
    public AudioClip effectOne;
    public AudioClip effectTwo;
   

    private bool facingRight = true; //conditions
    private bool onGround = true;

    
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>(); //looks for object w/ rigidbody 2d
        anim = GetComponent<Animator>(); //checks for sprites

        count = 0;
        lives = 3;
        winText.text = "";
        SetCountText();
        SetLivesText();
        
        currentTime = startingTime;

    }



    // Update is called once per frame
    void FixedUpdate()  //movement
    {

        

        float hozMovement = Input.GetAxis("Horizontal");  //Player Input
        float vertMovement = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(hozMovement, 0);

        rb2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if ( hozMovement == 0)  //Idle Animation
        {

            anim.SetBool("Run", false);
        }

        else
        {

            anim.SetBool("Run", true); // Run Animation
        }

        if (vertMovement > 0) //Jump Animation
        {

            anim.SetBool("IsJumping",true);

        }
        else if (onGround==true ) // Return to idle condition
        {
            anim.SetBool("IsJumping", false);

        }

        if (facingRight == false && hozMovement > 0) //flips player
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        SetTimeText();
    }


    private void OnCollisionStay2D(Collision2D collision) //player can jump but only when they collide with the Ground
    {
        if (collision.collider.tag == "Ground")
        {
            onGround = true;
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                onGround = false;
                effects.clip = effectOne;
                effects.Play();
            }
        }

        
    }

    void OnTriggerEnter2D(Collider2D other) //Coins
    {
       

        if (other.gameObject.CompareTag("PickUp")) // Plus Score
        {
            effects.clip = effectTwo;
            effects.Play();
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Enemy")) // Minus Lives
        {
            anim.SetTrigger("Hurt");
            anim.SetBool("IsJumping", false);
          

            lives = lives - 1;
            SetLivesText();
        }

        if (count == 4 && other.gameObject.CompareTag("PickUp"))
        {
            transform.position = new Vector2(57.39576f, -1.32f);
            lives = 3;
            SetLivesText();

        }


        if (lives == 0) // Lose condition forces the player to stop moving
        { 
            
            Destroy(this);
            
        }
    }

    void SetCountText() //win 
    {
        countText.text = "Count: " + count.ToString(); //Win condition
        if (count >= 8)
        {
           
            winText.text = "You win! Game created by Sebastian Saavedra";
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
    }

    void SetLivesText() //Lose Condition
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives == 0)
        {
            anim.SetTrigger("Hurt");
            winText.text = "You lose!";

        }
   

    }

    void SetTimeText()
    {
        currentTime -= 1 * Time.deltaTime;
        if (countdownText != null)
        {
            countdownText.text = "Time: " + currentTime.ToString("0");
        }
        if (currentTime <= 0)
        {
            currentTime = 0;
            winText.text = "Times Up!";
            Destroy(this);
        }
        if (count >= 8)
        {
            Destroy(countdownText);
        }
   
    }

    void Flip() //flip function
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f); //To mirror projectile summon space
    }
}
