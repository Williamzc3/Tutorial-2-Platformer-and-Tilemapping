using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class playerScript : MonoBehaviour
{
    // Player controller stuff
    private Rigidbody2D rd2d;
    public float speed;
    // Text stuff
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI LifeText;
	public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    private int count;
    private int life;
    private bool facingRight = true;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // Player Movement
        rd2d = GetComponent<Rigidbody2D>();
        // Coin Count
        count = 0;
        SetCountText ();
        winTextObject.SetActive(false);
        // Life Count
        life = 3;
        setLifeText ();
        loseTextObject.SetActive(false);

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Player Movement
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))

        {
          anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))

        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))

        {
          anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))

        {
          anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))

        {
          anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))

        {
          anim.SetInteger("State", 0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void SetCountText()
	{
		CountText.text = "Count: " + count.ToString();

		if (count >= 8) 
		{
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
            speed = 0;
            musicSource.clip = musicClipOne;
            musicSource.Play();
		}
	}

    void setLifeText()
    {
        LifeText.text = "Lives: " + life.ToString();

        if (life == 0)
        {
            loseTextObject.SetActive(true);
            speed = 0;
            anim.SetInteger("State", 3);
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
	{
		// ..and if the GameObject you intersect has the tag 'Coin' assigned to it..
		if (other.gameObject.CompareTag ("Coin"))
		{
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}

        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive (false);
            life = life - 1;
            setLifeText ();
        }

        else if (count == 4)
        {
            transform.position = new Vector3(78.5f, 0f, 0f);
            life = 3;
            setLifeText ();
        }
	}

    // Player jumping
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
            else if (collision.collider.tag == "Ground")
            {
                anim.SetInteger("State", 0);
            }
    }
}

