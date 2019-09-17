using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    //public Rigidbody2D rb2d;

    public float speed;
    public float stop_dist;

    private Transform player;

    //public float stunDelay;

    HealthBar HB;
    public GameObject HbInner;
    public GameObject HbOuter;

    public EnemySpawner Spawner;

    private float timeElapsed; //time since last attack
    public float attackDelay; // time delay between attacks

    public Animator anim; // added this back in and attached the animator to this
    //public gameobject bloodeffect; // just drag particle effect into this spot

    // Start is called before the first frame update
    void Start()
    {
        HB = new HealthBar(3);
        //anim = GetComponent<Animator>();
        //anim.SetBool("isRunning", true);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


        timeElapsed = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();

        if (Vector2.Distance(transform.position, player.position) > stop_dist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, 0 * Time.deltaTime);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void UpdateHealth()
    {
        Vector3 curScale = HbInner.transform.localScale;
        curScale.x = HB.GetHealthPercent();
        HbInner.transform.localScale = curScale;
    }

    public void TakeDamage(int damage)
    {
        anim.SetTrigger("take_damage"); // added this as well
        //timeElapsed = stunDelay;
        //Instantiate(bloodeffect, transform.position, Quaternion.identity);
        //play a hurt sound
        HB.DamageHealth(damage);
        health -= damage;
        print("health: " + health);
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.collider.name == "Player" && timeElapsed <= 0)
        {
            coll.collider.GetComponent<Movement>().HB.DamageHealth(5);
            timeElapsed = attackDelay;
        } else
        {
            timeElapsed -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.name == "Player" && timeElapsed <= 0)
        {
            timeElapsed = attackDelay;
        }
    }
}
