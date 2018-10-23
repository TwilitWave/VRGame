using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }


    public enum GameState
    {
        FreeMode, 
    }
    private GameState _currentState = GameState.FreeMode;
    public PlayerEntity player;
    public MummyManager mummyManager;
    private GameManager()
    {

    }


    private void Awake()
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
        if (mummyManager == null)
        {
            Instantiate(mummyManager);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameState GetCurrentState()
    {
        return _currentState;
    }

}




