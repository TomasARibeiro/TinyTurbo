using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates { countDown, racing, raceOver};

public class GameManager : MonoBehaviour
{
    //static instance so the other scripts can access it
    public static GameManager instance = null;

    public static event Action E_CameraSwitch; //swap cameras from track to player and vice versa
    public static event Action E_SpawnPlayer; //spawn the player

    //race specific events
    public static event Action E_CheckPointPassed;
    public static event Action E_LapFinished;
    public static event Action E_RaceOver; //the race is over

    #region In Race Vars
    public int CurrentCheckPoint = 0;
    public int CurrentLap = 1;
    public int MaxCheckPoints = 0;
    public int MaxLaps = 0;
	#endregion

	GameStates currentState = GameStates.countDown;

	private void Awake()
	{
		if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            E_CameraSwitch?.Invoke();
			E_SpawnPlayer?.Invoke();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LevelStart();
    }

	void LevelStart()
	{
		currentState = GameStates.countDown;
        Debug.Log("Level started");
	}

    public void OnRaceStart()
    {
		Debug.Log("Race Started!");

        E_CameraSwitch?.Invoke();
        E_SpawnPlayer?.Invoke();
		currentState = GameStates.racing;
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
        CurrentLap++;
		ResetCheckPoints();
		E_LapFinished?.Invoke();

        if (CurrentLap == MaxLaps) {
            OnRaceFinished();
        }
    }

    public void ResetCheckPoints()
    {
        CurrentCheckPoint = 0;
    }

    public void OnRaceFinished()
    {
        currentState = GameStates.raceOver;
        E_CameraSwitch.Invoke();
        E_RaceOver?.Invoke();
    }

    public GameStates GetGameState()
    {
        return currentState;
    }
}
