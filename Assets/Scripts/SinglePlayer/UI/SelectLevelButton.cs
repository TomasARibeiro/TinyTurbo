using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelButton : MonoBehaviour
{
	[SerializeField] private string _sceneName;

	public void SetSelectedScene()
	{
		GameManager.Instance.OnLevelPicked(SceneManager.GetSceneByName(_sceneName));
		LoadSelectedScene();
	}

	public void LoadSelectedScene()
	{
		SceneManager.LoadScene(_sceneName, LoadSceneMode.Single);
	}
}
