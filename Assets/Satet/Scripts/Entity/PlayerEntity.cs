using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEntity : MonoBehaviour {
    public int health = 1;
    private static PlayerEntity _instance = null;
    
    public static PlayerEntity Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerEntity();
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

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UnderAttack()
    {
       
        health--;
        Debug.Log(health);
        if (health == 0)
        {
            Debug.Log("@@@");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

