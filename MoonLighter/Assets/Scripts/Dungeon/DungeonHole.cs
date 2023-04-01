using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonHole : MonoBehaviour
{
    public float mDelayTime = 1.0f;
    public float mFallingDamegeRate = 0.45f;

    private DungeonStage mStage = null;

    public Tilemap mTilemap = null;

    public void SetStage(DungeonStage stage)
    {
        mStage = stage;
    }
 



    //worldPosition : ĳ���� ��ǥ+�Է��� ����Ű ���� + 0.2? ������ ���� Ȧ���� üũ 
    public bool IsHole(Vector3 worldPosition)
    {
        Vector3Int xyz = mTilemap.WorldToCell(worldPosition);
        TileBase tileBase = mTilemap.GetTile(xyz);
        if(tileBase)
        {
            return true;
        }
        return false;
    }
}
