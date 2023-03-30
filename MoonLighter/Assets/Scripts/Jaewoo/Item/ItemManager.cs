using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : GSingleton<ItemManager>
{
    public Inventory mInventory;
    //프리팹 배열
    public GameObject[] mItemPrefab = new GameObject[6];
    //public Object[] mListTest = new Object[6];
   
    public int mDropCount = 3;
    
    //프리팹 로드
    public override void Awake()
    {
        //mListTest = Resources.LoadAll("Prefabs_Jaewoo/Item");
        mItemPrefab[0] =Resources.Load("Prefabs_Jaewoo/Item/CastingWreckage") as GameObject;
        mItemPrefab[1] =Resources.Load("Prefabs_Jaewoo/Item/Cloth") as GameObject;
        mItemPrefab[2] =Resources.Load("Prefabs_Jaewoo/Item/HardenedSteel") as GameObject;
        mItemPrefab[3] =Resources.Load("Prefabs_Jaewoo/Item/IronRod") as GameObject;
        mItemPrefab[4] =Resources.Load("Prefabs_Jaewoo/Item/RuneTool") as GameObject;
        mItemPrefab[5] =Resources.Load("Prefabs_Jaewoo/Item/WaterBall") as GameObject;
        base.Awake();
    }

    protected override void Init(){
        base.Init();
    }
    public override void Start()
    {        
        base.Start();
    }
    //ItemManager.Instance.DropItem( 위치)
    public void DropItem(Vector3 position)
    {
        for(int index = 0; index <Random.Range(1, 4) ; index ++)
        {
            float RandomX = Random.Range(0, 1f);
            float RandomY = Random.Range(0, 1f);
            int itemRandom = Random.Range(0, mItemPrefab.Length);
            GameObject dropItem = mItemPrefab[itemRandom];
            GameObject item = Instantiate(dropItem);
            item.gameObject.transform.position = new Vector3(position.x + RandomX,position.y + RandomY,0);
        }              
    }

   

}
