using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public bool isJumping;
    private Rigidbody2D rig;
    private Animator anim;

    public Transform attackCheck;
    public float radiusAttack;
    public LayerMask layerEnemy;
    float timeNextAttack;
    public int damage;
    public int health;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(10, 14, true);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Attack();
        Death();
       
    }
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.layer == 8) || collision.gameObject.layer == 9)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }
        if (collision.gameObject.tag == "enemy")
        {
            health -= 1;
            anim.SetTrigger("hurt");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.layer == 8) || collision.gameObject.layer == 9)
        {
            isJumping = true;
            
        }
    }
    void Attack()
    {
        if(timeNextAttack <= 0f)
        {
            if(Input.GetButtonDown("Fire1") && rig.velocity == new Vector2(0f,0f))
            {
                anim.SetTrigger("attack");
                timeNextAttack = 0.2f;
                PlayerAttack();  

            }
        }
        else
        {
            timeNextAttack -= Time.deltaTime;
        }
    }
    void Flip()
    {
        attackCheck.localPosition = new Vector2(-attackCheck.localPosition.x, attackCheck.localPosition.y);
    }

    void PlayerAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackCheck.position, radiusAttack, layerEnemy
            );
        for(int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Pig>().TakeDamage(damage);
        }
    }
    void Death()
    {
        if (health <= 0)
        {
            GameController.instance.dead = true;
            rig.velocity = new Vector2(0, rig.velocity.y);
            anim.SetBool("dead", true);
            StartCoroutine(Die());
            

        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.6f);
        GameController.instance.ShowGameOver();
        Destroy(gameObject);
        
        

    }
}