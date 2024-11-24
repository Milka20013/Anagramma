using UnityEngine;

public class TileSlot : MonoBehaviour
{
    private Tile attachedTile;

    public void OnTileDestroyed(object tileObj)
    {
        Tile tile = (Tile)tileObj;
        if (attachedTile == tile)
        {
            attachedTile = null;
        }
    }
    public void SetTile(Tile tile)
    {
        attachedTile = tile;
    }

    public bool IsEmpty()
    {
        return attachedTile == null || attachedTile.isDead;
    }
}
