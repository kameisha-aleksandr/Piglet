using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "new ExplosionTile", menuName = "Tiles/ExplosionTile")]
public class ExplosionTile : Tile
{
    public int damage = 50;
    public ExplosionTile()
    {   
    }
}
