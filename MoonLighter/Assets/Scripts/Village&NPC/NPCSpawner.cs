using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject NPCspawner;
    public GameObject NPCPrefab;
    public float spawnRateMin = 0.5f;
    public float spawnRateMax = 1f;


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
            var NPC = ShopNPCPool.GetObject(); // ����

            if (NPC != null)
            {
                NPC.transform.position = transform.position;
            }

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        }

    }
}
