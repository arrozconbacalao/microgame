using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject asteroidPrefab, cam, goscreen;
    public float spawnRatePerMinute = 30;
    public float spawnRateIncrement = 1f;
    public float xLimit;
    public float maxLifeTime = 2f;

    private float spawnNext = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (goscreen.activeSelf == false)
        {
            if (Time.time > spawnNext)
            {
                spawnNext = Time.time + 60 / spawnRatePerMinute;
                spawnRatePerMinute += spawnRateIncrement;

                float rand = Random.Range(-xLimit, xLimit);

                Vector3 spawnPosition = new Vector3(rand, 7f, -(1f));

                GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

                Destroy(meteor, maxLifeTime);
            }
        }

    }
}
