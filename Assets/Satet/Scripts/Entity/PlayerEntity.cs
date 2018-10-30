using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SSR.Player
{
    [Serializable]
    public class HealthWall
    {
        public int health;
        public List<GameObject> walls;
    }

    public class PlayerEntity : MonoBehaviour
    {
        public List<HealthWall> healthWall;
        private static PlayerEntity _instance = null;
        public GameObject scoreText;
        public int _score;
        public int score {
            get
            {
                return this._score;
            }
            set { 
                scoreText.GetComponent<Text>().text = "Score: " + value;
                this._score = value;
            }
        }

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

            //DontDestroyOnLoad(gameObject);
        }
        // Use this for initialization
        void Start()
        {
            this.score = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UnderAttack(int achievedPoint)
        {
            if (achievedPoint >= 0 && achievedPoint < healthWall.Count)
            {
                healthWall[achievedPoint].health--;
                Debug.Log(healthWall[achievedPoint].health);
                if (healthWall[achievedPoint].health < 0)
                {
                    Debug.Log("@@@");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                else
                {
                    Destroy(healthWall[achievedPoint].walls[healthWall[achievedPoint].health]);
                }
            }
        }
    }
}


