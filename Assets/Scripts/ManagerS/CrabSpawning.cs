using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MySequence
{
    List<(int, int, int)> waves = new List<(int, int, int)>()
    {
        (2,0,0),(3,0,0),(4,0,0), //9
        (4,0,0),(4,0,0),(5,0,0),  //22
        (6,0,0),(6,0,0),(6,0,0), //
        (7,0,0),(8,0,0),(10,0,0), 
        (10,0,0),(10,0,0),(10,0,0),  //95
        (10,0,0),(15,0,0),(15,0,0),
        (10,0,0),(15,0,0),(15,0,0), 
        (0,1,0),(10,2,0),(15,3,0), // 196
        (20,5,0),(0,10,0),(10,15,0), 
        (2,2,1),(5,5,3),(10,10,5),
        (20,20,5),(0,0,10),(0,0,15), 
        (0,0,20),(0,0,25),(0,0,30),
        (20,20,30),(5,5,40),(0,0,50),(1,0,0)
    };

    public int index = 0;
    public (int,int,int) GetNext()
    {
        if(index<waves.Count)
            return waves[index++];
        return (-1,-1,-1);
    }
}

public class CrabSpawning : MonoBehaviour
{
    public CrabScript crabPrefab, crabPrefabMid, crabPrefabBig, crabPrefabBOSS;
    public float timeBetweenWaves = 15f;

    public static CrabSpawning Instance;

    //spawns small,med,big
    public int x,y,z;
    public int spawnRadiusLowerBound, spawnRadiusUpperBound;

    private MySequence mysq = new MySequence();
    private float timeElapsedFromLastWave;

    private CrabSpawning()
    {
        Instance = this;
    }

    public int GetWave { get => mysq.index - 1; }

    // Start is called before the first frame update
    void Start()
    {
        timeElapsedFromLastWave = timeBetweenWaves-5f;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsedFromLastWave += Time.deltaTime * GameManager.Instance.GameSpeed;
        if (timeElapsedFromLastWave > timeBetweenWaves)
        {
            if (x == -1)
            {
                gameObject.SetActive(false);
                SpawnCrab(crabPrefabBOSS);
                return;
            }
            if (x > 0)
            {
                x--;
                SpawnCrab(crabPrefab);
            }
            if (y > 0)
            {
                y--;
                SpawnCrab(crabPrefabMid);
            }
            if(z > 0)
            {
                z--;
                SpawnCrab(crabPrefabBig);
            }
            //reset timer
            if (x == 0 && y==0 && z==0)
            {
                timeElapsedFromLastWave = 0;
                timeBetweenWaves += 0.75f;
                (x,y,z) = mysq.GetNext();
            }
        }
    }

    void SpawnCrab(CrabScript crab)
    {
        float angle = Random.Range(-Mathf.PI / 4, Mathf.PI/4);
        if (Random.Range(0, 2) == 1)
            angle += Mathf.PI;
        float dist = Random.Range(spawnRadiusLowerBound, spawnRadiusUpperBound);
        Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * dist;
        Instantiate(crab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(Vector3.zero, spawnRadiusLowerBound);
        Gizmos.DrawWireSphere(Vector3.zero, spawnRadiusUpperBound);
    }
}
