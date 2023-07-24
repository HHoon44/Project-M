using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private int Left_dashcount = 0; //대쉬카운트. 2면 대시.
    private int Right_dashcount = 0;

    private bool Left_dash;
    private bool Right_dash;

    public int Force;
    public Rigidbody2D PlayerRB;

    private void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right_dashcount += 1;
        }

        if (Right_dashcount == 1)
        {
            Invoke("R_dashcountZero", 0.2f);
        }

        if (Right_dashcount >= 2)
        {
            Right_dash = true;
            Right_dashcount = 0;
        }

        if (Right_dash == true)
        {
            PlayerRB.velocity = new Vector2(Force, PlayerRB.velocity.y);
            Invoke("R_stop", 0.3f);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left_dashcount += 1;
        }

        if (Left_dashcount == 1)
        {
            Invoke("L_dashcountZero", 0.2f);
        }

        if (Left_dashcount >= 2)
        {
            Left_dash = true;
            Left_dashcount = 0;
        }

        if (Left_dash == true)
        {
            PlayerRB.velocity = new Vector2(-Force, PlayerRB.velocity.y);
            Invoke("L_stop", 0.3f);
        }
    }

    void L_dashcountZero()
    {
        Left_dashcount = 0;
    }

    void R_dashcountZero()
    {
        Right_dashcount = 0;
    }

    void L_stop()
    {
        Left_dash = false;
        PlayerRB.velocity = Vector2.zero;
    }

    void R_stop()
    {
        Right_dash = false;
        PlayerRB.velocity = Vector2.zero;
    }
}