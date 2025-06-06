using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 1.5f;
    public float heightOffset = 2f;

    private float timer = 0f;
    private bool isSpawning = false;

    public void StartSpawning()
    {
        isSpawning = true;
        timer = 0f; // reset timer để spawn ngay hoặc chờ 1 khoảng tùy bạn
    }

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            float y = Random.Range(-heightOffset, heightOffset);
            Instantiate(pipePrefab, new Vector3(9, y, 0), Quaternion.identity);
            timer = 0f;
        }
    }
}
