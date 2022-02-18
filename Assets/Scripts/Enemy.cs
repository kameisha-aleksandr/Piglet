using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Enemy : Creature
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 currentMoveDirection;
    private int damage = 20;

    private float nextChangeDir = 0f;

    public int Damage { get => damage; set => damage = value; }

    protected void Start()
    {
        Init();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public override void Init()
    {
        currentHealth = health;
        currentMoveDirection = Vector2.right;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public override void Move()
    {
        CheckMoveDirection(currentMoveDirection);
        anim.SetFloat("hAxis", currentMoveDirection.x);
        anim.SetFloat("vAxis", currentMoveDirection.y);
        rb.MovePosition(rb.position + currentMoveDirection * speed * Time.fixedDeltaTime);
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public void CheckMoveDirection(Vector2 direction)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, direction, 0.7f, LayerMask.GetMask("Impassable"));
        if (hit.collider != null || Time.time > nextChangeDir)
        {

            ChangeMoveDirection(direction);
            nextChangeDir = Time.time + 2f;
        }
    }
    public void ChangeMoveDirection(Vector2 currentDirection)
    {
        currentMoveDirection = new Vector2(currentDirection.y, currentDirection.x);
        if (Random.Range(0, 2) == 1)
        {
            currentMoveDirection *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ExplMap")
            ChangeCurrentHealth(-explTile.damage);
    }
}
