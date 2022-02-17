using System.Collections;
using System.Collections.Generic;
using Asteroids.Scripts.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Managers
{
    public class GameManager : MonoBehaviour
    {
        public Text pointsText;
        public Text livesText;
        public Text gameOverPointsText;
        public AsteroidsBootstrapper asteroidsBootstrapper;
        public GameObject gameOverScreen;

        private enum menuScreen { Init, Game, GameOver };
        private static GameManager _instance;


        private static int _points = 0;
        private static int _lives = 3;
        private static menuScreen gameState = menuScreen.Game;

        public static int points
        {
            get => _points;
            set
            {
                _points = value;
            }
        }

        public static int lives
        {
            get => _lives;
            set
            {
                _lives = value;
            }
        }

        public void StartGame()
        {

            Debug.Log("<color=orange> Screen </color>: " + gameState);
            Debug.Log("<color=yellow> Start Game !! </color>");
            livesText.text = "Lives: " + _lives.ToString();
            pointsText.text = "Points: " + _points.ToString();

        }

        public void ResetGame()
        {

            Debug.Log("<color=red>Reset Game</color>");
            _points = 0;
            _lives = 3;
            UpdateLives();
            UpdatePoints();
            if (gameOverScreen.activeSelf) gameOverScreen.SetActive(false);

        }

        public void UpdateLives(int _value = 0)
        {
            _lives -= _value;
            livesText.text = "Lives: " + _lives.ToString();
            if (_value != 0) Debug.Log("<color=red>Lives:</color>You lost a live");
            if (_lives >= 1)
            {
                Invoke("StartAgain", 2f);
            }
            else
            {
                gameState = menuScreen.GameOver;
                gameOverScreen.SetActive(true);
                gameOverPointsText.text = "You lose !! Points: " + _points.ToString();
                Debug.Log("<color=red>Lives:</color>You lost all live");
            }
        }

        private void StartAgain()
        {
            asteroidsBootstrapper.StartGame();
        }


        public void UpdatePoints(int _value = 0)
        {
            _points += _value;
            pointsText.text = "Points: " + _points.ToString();
            if (_value != 0) Debug.Log("<color=green>Points :</color>" + _points);
        }


        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = typeof(GameManager).Name;
                        _instance = go.AddComponent<GameManager>();
                        DontDestroyOnLoad(go);
                    }
                }
                return _instance;
            }
        }


        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }

        }

    }
}
