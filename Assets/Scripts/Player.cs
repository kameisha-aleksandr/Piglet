using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : Creature
{
    [SerializeField] private Tilemap obstacleMap;
    [SerializeField] private Tilemap explosionMap;
    [SerializeField] private GameObject bomb;

    private PlayerInputHandler inputHandler;

    private void Start()
    {
        Init();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Init()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        ChangeCurrentHealth(health);
    }

    public override void Move()
    {
        inputHandler.PlayerMovement(speed);
    }

    public override void Die()
    {
        Time.timeScale = 0;
    }

    public void PlaceBomb()
    {
        GameObject go = Instantiate(bomb, obstacleMap.GetCellCenterWorld(
            obstacleMap.WorldToCell(transform.position)), Quaternion.identity);
        go.GetComponent<Bomb>().Init(obstacleMap, explosionMap);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            ChangeCurrentHealth(-enemy.Damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "ExplMap")
            ChangeCurrentHealth(-explTile.damage);
        if (collider.TryGetComponent(out Enemy enemy))
        {
            ChangeCurrentHealth(-enemy.Damage);
        }
    }
}
