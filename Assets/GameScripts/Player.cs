using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;
    public bool isGrounded;

    //Stats
    public int curHealth;
    public int maxHealth = 5; //Change as needed

    private Rigidbody2D rigid_body;
    private Animator anim;
    private gameMaster gm;


	// Use this for initialization
	void Start () {
        rigid_body = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();

        curHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<gameMaster>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Grounded", isGrounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetButtonDown("Jump") && isGrounded )
        {
            rigid_body.velocity = new Vector2(rigid_body.velocity.x, 0f);
            rigid_body.AddForce(Vector2.up * jumpPower);
           // this.isGrounded = false;
        }
        if (curHealth > maxHealth)
            curHealth = maxHealth;
        if (curHealth <= 0)
            Die();

    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        //Fake friction / Easing the x speed of our player
        Vector3 easeVelocity = rigid_body.velocity;
        easeVelocity.y = rigid_body.velocity.y;
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.95f;

        if (isGrounded) rigid_body.velocity = easeVelocity;

        rigid_body.AddForce((Vector2.right * speed) * h);
        // Max Speed going right
        if (rigid_body.velocity.x > maxSpeed)
        {
            rigid_body.velocity = new Vector2(maxSpeed, rigid_body.velocity.y);
        }
        // Max Speed going left
        if (rigid_body.velocity.x < -maxSpeed)
        {
            rigid_body.velocity = new Vector2(-maxSpeed, rigid_body.velocity.y);
        }
    }

    void Die()
    {
        //Restart
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
        gameObject.GetComponent<Animation>().Play("Player_Hit");
        if (curHealth < 0)
        {
            curHealth = 0;
        }
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector3 knockbackDir)
    {
        float timer = 0;
        //float vel_dir = rigid_body.velocity.x;
        //Debug.Log("Velocity on hit is: " + vel_dir);
        //Debug.Log("knockbackDir x is: " + knockbackDir.x);
        float xdiff = Mathf.Abs(rigid_body.velocity.x) / rigid_body.velocity.x;
        //Debug.Log("xdiff is: " + xdiff);
        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            //Debug.Log("x_vel is: " + rigid_body.velocity.x);
            //Debug.Log("y_vel is: " + rigid_body.velocity.y);
            rigid_body.velocity = new Vector2(0f, 0f);
            rigid_body.AddForce(new Vector2(xdiff * -1 * knockbackPwr, 
                knockbackPwr));
        }
        yield return 0;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Fruit"))
        {
            Destroy(col.gameObject);
            gm.points += 1;
        }
    }
}
