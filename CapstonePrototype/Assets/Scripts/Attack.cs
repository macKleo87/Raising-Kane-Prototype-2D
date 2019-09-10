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

    public int damage;

    //public Animator attackAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            if (timeElapsed <= 0) //swapped the two if statements here
            {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCenter.position, new Vector3(attackX, attackY, 1));
    }
}
