using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float health;
    public float speed;

    private float timeElapsed;
    public float stunDelay;

    HealthBar HB;
    public GameObject HbInner;
    public GameObject HbOuter;

    public Animator anim; // added this back in and attached the animator to this
    //public gameobject bloodeffect; // just drag particle effect into this spot

    // Start is called before the first frame update
    void Start()
    {
        HB = new HealthBar(3);
        //anim = GetComponent<Animator>();
        //anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();

        // if enemy has movement this can be used to "stun" when they get hit
        if (timeElapsed <= 0)
        {
            speed = 5;
        }
        else
        {
            speed = 0;
            timeElapsed -= Time.deltaTime;
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
        timeElapsed = stunDelay;
        //Instantiate(bloodeffect, transform.position, Quaternion.identity);
        //play a hurt sound
        HB.DamageHealth(damage);
        health -= damage;
        print("health: " + health);
    }
}
