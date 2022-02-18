using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(SpriteRenderer))]
public class Bomb : MonoBehaviour
{  
    [SerializeField] private int damage = 50;
    [SerializeField] private int damageRadius = 2;
    [SerializeField] private float explosionDelay = 2f;
    [SerializeField] private float explosionDuration = 0.5f;
    [SerializeField] private ExplosionTile explTile;

    private Tilemap obstacleMap, explMap;
    private Vector3Int bombPosition;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Tilemap _obstacleMap, Tilemap _explMap)
    {
        obstacleMap = _obstacleMap;
        explMap = _explMap;
        bombPosition = _obstacleMap.WorldToCell(transform.position);
        explTile.damage = damage;
        StartCoroutine(Explosion());
    }
  
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(explosionDelay);
        StartCoroutine(SetExplosionTile(bombPosition));
        CheckDirection(Vector3Int.up);
        CheckDirection(Vector3Int.right);
        CheckDirection(Vector3Int.left);
        CheckDirection(Vector3Int.down);

        spriteRenderer.enabled = false;
        Destroy(gameObject, 2f);
    }

    IEnumerator SetExplosionTile(Vector3Int curPos)
    {
        obstacleMap.SetTile(curPos, null);
        explMap.SetTile(curPos, explTile);
        yield return new WaitForSeconds(explosionDuration);
        explMap.SetTile(curPos, null);
    }

    public void CheckDirection(Vector3Int direction)
    {
        Vector3Int curPos;
        for (int i = 1; i <= damageRadius; i++)
        {
            curPos = bombPosition + direction * i;
            if (obstacleMap.GetTile<UndestroyableTile>(curPos) is null)
            {
                if (obstacleMap.GetTile<DestroyableTile>(curPos) is null)
                {
                    StartCoroutine(SetExplosionTile(curPos));
                }
                else
                {
                    StartCoroutine(SetExplosionTile(curPos));
                    break;
                }
            }
            else break;
        }
    }
}
