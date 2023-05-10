using UnityEngine;
using System.Collections;

public class Character : Unit
{
    [SerializeField]
    public Main main;
    private int lives = 5;
    public Joystick joystick;
    public int Lives
    {
        get { return lives; }
        set
        {
           if (value <= 5) lives = value;
            livesBar.Refresh();
           if (lives <= 0)
              {

                   GetComponent<BoxCollider2D>().enabled = false;
                   Invoke("Lose", 1.5f);
               }



           //if (lives <= 0)
           //{
            // Destroy(gameObject);// смерть персонажа !!!!
            // LevelManager.instance.Respawn();
           //}

              //livesBar.Refresh();
        }
    }

  //    Destroy(gameObject);// смерть персонажа !!!!





    private LivesBar livesBar;

    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;

    private bool isGrounded = false;

    private Bullet bullet;

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded) State = CharState.Idle;

        //if (Input.GetButtonDown("Fire1")) Shoot();
        if (joystick.Horizontal !=0) Run();
        if (isGrounded && joystick.Vertical > 0.5f) Jump();
    }

    private void Run()
    {
        Vector3 direction = transform.right * joystick.Horizontal;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

        if (isGrounded) State = CharState.Run;
    }

    private void Jump()
    {
        rigidbody.velocity = Vector2.up * jumpForce;
        //AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Shoot()
    {
        Vector3 position = transform.position; position.y += 0.8F;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }

    public override void ReceiveDamage()
    {
        Lives--;

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(transform.up * 8.0F, ForceMode2D.Impulse);



        //if (lives <= 0)
      //  {
        //    Destroy(gameObject);// смерть персонажа !!!!
//
      //  }
      //  LevelManager.instance.Respawn();

//lives = 5;


    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3F);

        isGrounded = colliders.Length > 1;

        if (!isGrounded) State = CharState.Jump;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

        Bullet bullet = collider.gameObject.GetComponent<Bullet>();
        if (bullet && bullet.Parent != gameObject)
        {
            ReceiveDamage();
        }
    }

    void Lose()
    {
        main.GetComponent<Main>().Lose();
    }
}


public enum CharState
{
    Idle,
    Run,
    Jump
}

//public void RecountHp(int deltaHp)
  //  {
    //    curHp = curHp + deltaHp;
    //    if (deltaHp < 0)
    //    {
      //      StopCoroutine(OnHit());
      //      isHit = true;
      //      StartCoroutine(OnHit());
      //  }
    //    print(curHp);
    //    if (curHp <= 0)
    //    {
    //        GetComponent<CapsuleCollider2D>().enabled = false;
    //        Invoke("Lose", 1.5f);
    //    }
  //  }
