using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigid;

    public float moveForce;
    public float maxSpeed;
    public float jumpForce;

    private bool isGround;
    private bool moveAble;

    private bool flipX = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGround = IsGround();

        float speedRaw = Input.GetAxisRaw("Horizontal") * maxSpeed;

        //방향이 바뀔 때
        if ((rigid.velocity.x >= 0 && speedRaw < 0)) //오른쪽 => 왼쪽
        {
            FlipX(true);
        }
        else if ((rigid.velocity.x <= 0 && speedRaw > 0)) //왼쪽 => 오른쪽
        {
            FlipX(false);
        }

        rigid.velocity = new Vector2(speedRaw, rigid.velocity.y);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Debug.DrawRay(transform.position, Vector2.down * 0.1f, Color.green); //바닥에 닿고 있는지 확인
    }

    private void FlipX(bool _flip)
    {
        flipX = _flip;
        transform.localScale = new Vector3((_flip ? -1 : 1) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void Jump()
    {
        if (IsGround()) //자기자신을 빼고 아래있는것
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private bool IsGround() => Physics2D.Raycast(transform.position, Vector2.down, .1f, (-1) - (1 << LayerMask.NameToLayer("Player")));

}
