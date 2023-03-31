using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    
    public float spawnRateMin;
    public float spawnRateMax;
    public Vector3 mMinimumPosition;
    public Vector3 mMaximumPosition;
    public Vector3 mDoorPosition;
    public Vector3 mCasherPosition;
    private float spawnRate;
    private float timeAfterSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);


    }

    // Update is called once per frame
    void Update()
    {

        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;
            var NPC = ShopNPCPool.GetObject(); // ¼öÁ¤
            NPC.GetComponent<ShopNPC>().mMinimumPosition = mMinimumPosition;
            NPC.GetComponent<ShopNPC>().mMaximumPosition = mMaximumPosition;
            NPC.GetComponent<ShopNPC>().mDoorPosition = mDoorPosition;
            NPC.GetComponent<ShopNPC>().mCasherPosition = mCasherPosition;

            if (NPC != null)
            {
                NPC.transform.position = transform.position;
            }

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        }

    }
}
