using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyManager : MonoBehaviour {
    public Dictionary<int, GameObject> Mummys;
    public List<EmergingPoint> emergingPoints;
    public List<MummyWalkingPath> walkingPaths;
    public GameObject mummyPrefab;
    int id = 0;
    private static MummyManager _instance = null;

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
    void Start () {
        Mummys = new Dictionary<int, GameObject>();

    }

    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            id++;
            Mummys.Add(id, Instantiate(mummyPrefab, new Vector3(0, 0, 0), transform.rotation));
            Mummys[id].GetComponent<MummyEntity>().SetUp(id, walkingPaths[id % walkingPaths.Count]);
        }
        
    }
    public void DestoryMummy(int id)
    {
        Mummys.Remove(id);
    }
}
