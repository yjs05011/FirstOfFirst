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
 
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        StartCoroutine(Falling(other));
    //        Debug.Log("Player Fall");

    //        other.transform.position = mStage.GetEntryPosition();

    //    }
    //}

    //IEnumerator Falling(Collider2D other)
    //{
      
    //    yield return new WaitForSeconds(mDelayTime);

    //    PlayerAct player = other.GetComponent<PlayerAct>();
    //    float falldamage = player.GetPlayerMaxHp() * mFallingDamegeRate;

    //    player.OnFalling(falldamage);

    //}

    //worldPosition : 캐릭터 좌표+입력한 방향키 방향 + 0.2? 정도의 값이 홀인지 체크 
    public bool IsHole(Vector3 worldPosition/**/)
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
