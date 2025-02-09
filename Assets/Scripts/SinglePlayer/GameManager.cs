using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { Idle, InMenu, CountDown, Racing, RaceOver};

public class GameManager : MonoBehaviour
{
    //static instance so the other scripts can access it
    public static GameManager Instance = null;

    public static event Action E_CameraSwitch; //swap cameras from track to player and vice versa
    public static event Action E_SpawnPlayer; //spawn the player

    #region Race Events
    public static event Action E_CountDownBegin;
    public static event Action E_RaceBegin;
	public static event Action E_CheckPointPassed; //passed a checkpoint
    public static event Action E_LapFinished; //lap finished
    public static event Action E_RaceOver; //the race is over
    public static event Action E_GamePaused;
	#endregion

	#region In Race Vars
	public int CurrentCheckPoint = 0;
    public int CurrentLap = 1;
    public int MaxCheckPoints = 0;
    public int MaxLaps = 0;
    public float RaceStartTime = 0f;
    public float RaceFinishTime = 0f;
    #endregion

    #region Menu Loaders
    private Scene _selectedLevel;
	#endregion

	private GameStates _currentState = GameStates.Idle;
    private GameStates _previousState = GameStates.Idle;

	private void Awake()
	{
		if (Instance == null)
            Instance = this; 
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
	}

	void Update()
    {
        Debug.Log("Current State: " + _currentState);

        if (_currentState == GameStates.InMenu)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        /*if (Input.GetKeyDown(KeyCode.E))
        {
            E_CameraSwitch?.Invoke();
			E_SpawnPlayer?.Invoke();
        }*/

        //player pauses/unpauses the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentState == GameStates.Racing) //check if the player isnt already in the menu
            {
                _previousState = _currentState;
				_currentState = GameStates.InMenu;
				E_GamePaused?.Invoke();
			}
            else if (_currentState == GameStates.InMenu) //if he is, return to the previous state
            {
                _currentState = _previousState;
				_currentState = GameStates.Racing;
				E_GamePaused?.Invoke();
            }
        }
    }

	public void OnStartedLevel()
	{
        _previousState = _currentState;
		_currentState = GameStates.Idle;
        Debug.Log("Level initiated");
	}

    public void OnCountDownStart()
    {
		Debug.Log("CountDown Started!");

		_previousState = _currentState;
		_currentState = GameStates.CountDown;

		E_CountDownBegin?.Invoke();
	}

    public void OnRaceStart()
    {
		Debug.Log("Race Started!");

		_previousState = _currentState;
		_currentState = GameStates.Racing;

		E_CameraSwitch?.Invoke();
        E_SpawnPlayer?.Invoke();
        E_RaceBegin?.Invoke();
        RaceStartTime = Time.time;
    }

    public void OnRaceLoad(int maxLaps, int maxCheckPoints)
    {
        CurrentCheckPoint = 0;
        CurrentLap = 1;
        MaxCheckPoints = maxCheckPoints;
        MaxLaps = maxLaps;
    }

    public void OnCheckPointPassed(int checkPointNumber)
    {
        Debug.Log("Passed CheckPoint Number " + checkPointNumber);

        CurrentCheckPoint = checkPointNumber;
        E_CheckPointPassed?.Invoke();
    }

    public void OnLapFinished()
    {
        if (CurrentLap == MaxLaps)
        {
            OnRaceFinished();
            return;
        }

        CurrentLap++;
        ResetCheckPoints();
        E_LapFinished?.Invoke();
  //      CurrentLap++;
		//ResetCheckPoints();
		//E_LapFinished?.Invoke();

  //      if (CurrentLap == MaxLaps) {
  //          OnRaceFinished();
  //      }
    }

    public void ResetCheckPoints()
    {
        CurrentCheckPoint = 0;
    }

    public void OnRaceFinished()
    {
        _previousState = _currentState;
        _currentState = GameStates.RaceOver;
        RaceFinishTime = Time.time;
        
        float finalTime = GetRaceTime();
        SaveHighScore(finalTime);

        E_CameraSwitch.Invoke();
        E_RaceOver?.Invoke();
    }

    public void OnRacePaused()
    {
        if (_currentState == GameStates.InMenu)
        {
            _previousState = _currentState;
            _currentState = GameStates.Racing;
        }
        else if (_currentState == GameStates.Racing)
        {
            _previousState = _currentState;
            _currentState = GameStates.InMenu;
        }
    }

	#region Menu Loaders
    //make picked scene the current level
    public void OnLevelPicked(Scene scene)
    {
        _selectedLevel = scene;
        OnStartedLevel();
    }

    public void OnExitRace()
    {
        _previousState = _currentState;
        _currentState = GameStates.Idle;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public Scene GetSelectedScene()
    {
        return _selectedLevel;
    }
    #endregion

	public GameStates GetGameState()
    {
        return _currentState;
    }

    public float GetRaceTime()
    {
        if (_currentState == GameStates.RaceOver)
        {
            return RaceFinishTime - RaceStartTime;
        }
        else return Time.time - RaceStartTime;
    }

	#region SAVING AND LOADING
    public void SaveHighScore(float newRaceTime)
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        float savedHighScore = PlayerPrefs.GetFloat(currentSceneName, float.MaxValue);

        if (newRaceTime < savedHighScore)
        {
            PlayerPrefs.SetFloat(currentSceneName, newRaceTime);
            PlayerPrefs.Save();
            Debug.Log("New High Score saved for " + currentSceneName + ": " + newRaceTime);
        }
        else
        {
            Debug.Log("New race time is slower, not saving.");
        }
    }

    public float GetHighScoreForCurrentScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        float highScore = PlayerPrefs.GetFloat(currentSceneName, float.MaxValue);

        if (highScore == float.MaxValue)
        {
            Debug.Log("No High Score for " + currentSceneName);
        }

        return highScore;
    }
    #endregion
}
