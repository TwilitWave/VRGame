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
        public GameObject FailedPanel;
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
            Debug.Log("@@@" + enabled);
            if (Input.GetKeyDown(KeyCode.P))
            {
                EndGame();
            }
        }

        public void UnderAttack(int achievedPoint)
        {
            if (achievedPoint >= 0 && achievedPoint < healthWall.Count)
            {
                healthWall[achievedPoint].health--;
                if (healthWall[achievedPoint].health < 0)
                {
                    EndGame();
                }
                else
                {
                    Destroy(healthWall[achievedPoint].walls[healthWall[achievedPoint].health]);
                }
            }
        }
        public void EndGame()
        {
            MummyManager.Instance.enabled = false;
            foreach (var item in MummyManager.Instance.Mummys)
                item.Value.GetComponent<MummyEntity>().enabled = false;
            enabled = false;
            FailedPanel.SetActive(true);
        }

        public void ReloadGame()
        {
            Debug.Log("In Reload Scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}


