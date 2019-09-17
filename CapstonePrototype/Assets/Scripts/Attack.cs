using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float timeElapsed; //time since last attack
    public float attackDelay; // time delay between attacks

    public Transform attackCenter;
    public float attackX;
    public float attackY;
    public LayerMask IsEnemy;
    public Sprite attackSprite;
    public Sprite attackSpriteL;
    public Sprite idleSprite;
    public Sprite idleSpriteL;
    public SpriteRenderer playerSpriteRenderer;

    public int damage;
    private float gotSpeed;
    private bool facingRight;
    private bool isAttacking;
    //public Animator attackAnim;

    // Start is called before the first frame update
    void Start()
    {
        playerSpriteRenderer = this.GetComponent<SpriteRenderer>();
        gotSpeed = 0;
        facingRight = true;
        isAttacking = false;

    }

    // Update is called once per frame
    void Update()
    {
        gotSpeed = this.GetComponent<MovePosition_Test>().GetSpeed();
        if (gotSpeed > 0)
        {
            facingRight = true;
        }
        else if (gotSpeed < 0)
        {
            facingRight = false;
        }

        if(facingRight && !isAttacking)
        {
            playerSpriteRenderer.sprite = idleSprite;
        }
        else if(!facingRight && !isAttacking)
        {
            playerSpriteRenderer.sprite = idleSpriteL;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (timeElapsed <= 0) //swapped the two if statements here
            {
                StartCoroutine(AttackAnimCo());
                //playerSpriteRenderer.sprite = attackSprite;
                print("Attack");
                //playerAnim.SetTrigger("attack");
                Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(attackCenter.position, new Vector2(attackX, attackY), 0, IsEnemy);
                for (int i = 0; i < enemiesHit.Length; i++)
                {
                    enemiesHit[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            timeElapsed = attackDelay;
        }
        else
        {
            timeElapsed -= Time.deltaTime;
        }
        
    }

    IEnumerator AttackAnimCo()
    {
        if (facingRight)
        {
            playerSpriteRenderer.sprite = attackSprite;
        }
        else
        {
            playerSpriteRenderer.sprite = attackSpriteL;
        }
        isAttacking = true;
        yield return new WaitForSecondsRealtime(.3f);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCenter.position, new Vector3(attackX, attackY, 1));
    }
}
