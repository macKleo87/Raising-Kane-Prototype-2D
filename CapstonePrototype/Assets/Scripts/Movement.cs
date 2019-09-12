using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb2d;
    public Transform pos;
    public int speed;

    HealthBar HB;
    public GameObject HbInner;
    public GameObject HbOuter;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.freezeRotation = true;
        pos = player.GetComponent<Transform>();
        HB = new HealthBar(20);

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 curMov = new Vector2();
        rb2d.GetVector(curMov);
        curMov.x = -curMov.x;
        curMov.y = -curMov.y;
        rb2d.AddForce(curMov);

        //Basic Movement
        Vector2 movement = new Vector2();

        if (Input.GetAxis("Horizontal") > 0 && checkEdge2(new Vector2(7.5f, -3.5f)))
        {
            movement.x = speed;
        }
        if (Input.GetAxis("Horizontal") < 0 && checkEdge(new Vector2(-7.5f, -1.5f)))
        {
            movement.x = -speed;
        }
        if (Input.GetAxis("Vertical") > 0 && checkEdge(new Vector2(-7.5f, -1.5f)))
        {
            movement.y = speed;
        }
        if (Input.GetAxis("Vertical") < 0 && checkEdge2(new Vector2(7.5f, -3.5f)))
        {
            movement.y = -speed;
        }


        depthScaling();
        rb2d.AddForce(movement);

        if(Input.GetKey(KeyCode.Q))
        {
            HB.DamageHealth(1);
        }

        UpdateHealth();

    }

    //Scaling
    void depthScaling()
    {
        Vector3 scale = new Vector3(1, 1, 1);
        float baseScale = 0.1f;
        if (pos.position.y > 0)
        {
            float scalefactor = 3 - pos.position.y;
            scale = new Vector3(baseScale * scalefactor, baseScale * scalefactor, baseScale * scalefactor);
        }
        else if (pos.position.y < 0)
        {
            float scalefactor = 3 - pos.position.y;
            scale = new Vector3(baseScale * scalefactor, baseScale * scalefactor, baseScale * scalefactor);

        }
        scale.x = Mathf.Clamp(scale.x, 0.2f, 1);
        scale.y = Mathf.Clamp(scale.y, 0.2f, 1);
        scale.z = Mathf.Clamp(scale.z, 0.2f, 1);

        pos.localScale = scale;
    }

    bool checkEdge(Vector2 edge)
    {
        if (pos.position.y < edge.y)
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    bool checkEdge2(Vector2 edge)
    {
        if (pos.position.y > edge.y)
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    void UpdateHealth()
    {
        Vector3 curScale = HbInner.transform.localScale;
        curScale.x = HB.GetHealthPercent();
        HbInner.transform.localScale = curScale;
    }
}



