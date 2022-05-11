using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemy;
    float randx;
    Vector2 whereToSpawn;
    public float SpawnRate;
    float nextSpawn = 0.0f;
    List<float> randy = new List<float>();
    
    

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + SpawnRate;
            SpawnObject();
        }
        if(GameController.instance.dead == true)
        {
            Destroy(gameObject);
        }
        if (GameController.instance.totalScore == 100)
        {
            Destroy(gameObject);
        }
    }
    void SpawnObject()
    {
        randx = Random.Range(-9.35f, 9.35f);
        randy.Add(-7.3f);
        randy.Add(-12.35f);
        randy.Add(-17.35f);
        int randomy = Random.Range(0, randy.Count - 1);
        float rady = randy[randomy];
        whereToSpawn = new Vector2(randx, rady);
        Instantiate(enemy, whereToSpawn, Quaternion.identity);
    }
}
