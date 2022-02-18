using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private FixedJoystick joistick;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveVelocity;
    private float hAxis, vAxis;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void PlayerMovement(float speed)
    {  
        hAxis = Mathf.Round(joistick.Horizontal);//Input.GetAxisRaw("Horizontal");
        vAxis = Mathf.Round(joistick.Vertical);//Input.GetAxisRaw("Vertical");
        anim.SetFloat("hAxis", hAxis);
        anim.SetFloat("vAxis", vAxis);   
        moveVelocity = new Vector2(hAxis, vAxis).normalized * speed;
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
