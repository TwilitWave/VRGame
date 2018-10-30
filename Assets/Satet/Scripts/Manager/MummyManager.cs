using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SSR.Mummy;

public class MummyManager : MonoBehaviour
{
    public Dictionary<int, GameObject> Mummys;
    public List<EmergingPoint> emergingPoints;
    public List<MummyWalkingPath> walkingPaths;
    public GameObject mummyPrefab;
    public List<int> emergingPointsStatus;
    public int availableSpot = 0;
    int id = 0;
    private static MummyManager _instance = null;
    System.Random random = new System.Random();

    public static MummyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MummyManager();
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        Mummys = new Dictionary<int, GameObject>();
        emergingPointsStatus = new List<int>();
        availableSpot = emergingPoints.Count;
        for (int i = 0; i < emergingPoints.Count; i++)
        {
            emergingPointsStatus.Insert(0, 0);
        }
        this.SpawnMummy();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.SpawnMummy();
        }
        if (random.Next(0, 1000) % 1000 == 3)
        {
            this.SpawnMummy();
        }
    }
    public void DestoryMummy(int id)
    {
        Mummys.Remove(id);
    }
    public void SpawnMummy()
    {
        SpawnMummy(random.Next(0, 100));
    }

    public void SpawnMummy(int pathId)
    {
        var path = walkingPaths[pathId % walkingPaths.Count];
        if (path.walkingSeq.Count > 0 && enterToEmergingPoint(path.walkingSeq[0], id + 1)) {
            id++;
            Mummys.Add(id, Instantiate(mummyPrefab, new Vector3(0, 0, 0), transform.rotation));
            Mummys[id].GetComponent<MummyEntity>().SetUp(id, path);
        }
    }

    private void OnSpawnMummy()
    {
        this.SpawnMummy();
        Debug.Log("Receive the spawn mummy");
    }

    

    public bool enterToEmergingPoint(int id, int value)
    {
        if (emergingPointsStatus[id] == value) return true;
        if (emergingPointsStatus[id] == 0)
        {
            emergingPointsStatus[id] = value;

            availableSpot--;
            return true;
        }
        return false;
    }
    public void leaveEmergingPoint(int id)
    {
        emergingPointsStatus[id] = 0;
        availableSpot++;
    }
}
