using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MenuStates { MainMenu, GameModeMenu, CampaignMenu, TimeTrialMenu, OutlastMenu}; //SettingsMenu

public class MainMenuHandler : MonoBehaviour
{
	private MenuStates _currentState = MenuStates.MainMenu;
	private MenuStates _previousState = MenuStates.MainMenu;
	#region Menus
	[SerializeField] private GameObject _mainMenuPrefab;
	[SerializeField] private GameObject _gameModeMenu;
	[SerializeField] private GameObject _campaignMenuPrefab;
	[SerializeField] private GameObject _timeTrialMenuPrefab;
	[SerializeField] private GameObject _outlastMenuPrefab;
	//[SerializeField] private GameObject _settingsMenuPrefab;
	#endregion

	private void Awake()
	{
		LoadMainMenu();
	}

	public void LoadMainMenu()
	{
		_previousState = _currentState;
		_currentState = MenuStates.MainMenu;
		MenuObjectActivator(_mainMenuPrefab);
	}

	public void LoadGameModeMenu()
	{
		_previousState = _currentState;
		_currentState = MenuStates.GameModeMenu;
		MenuObjectActivator(_gameModeMenu);
	}

	/*public void LoadSettingsMenu()
	{
		_previousState = _currentState;
		_currentState = MenuStates.SettingsMenu;
		MenuObjectActivator(_settingsMenuPrefab);
	}*/

	public void LoadCampaignMenu()
	{
		_previousState = _currentState;
		_currentState = MenuStates.CampaignMenu;
		MenuObjectActivator(_campaignMenuPrefab);
	}

	public void LoadTimeTrialMenu()
	{
		_previousState = _currentState;
		_currentState = MenuStates.TimeTrialMenu;
		MenuObjectActivator(_timeTrialMenuPrefab);
	}

	public void LoadOutlastMenu()
	{
		_previousState = _currentState;
		_currentState = MenuStates.OutlastMenu;
		MenuObjectActivator(_outlastMenuPrefab);
	}

	public void GoToMainMenu()
	{
		_previousState = _currentState;
		_currentState = MenuStates.MainMenu;
		MenuObjectActivator (_mainMenuPrefab);
	}

	public void ReturnButton()
	{
		switch (_previousState)
		{
			case MenuStates.MainMenu:
				_previousState = _currentState;
				_currentState = MenuStates.MainMenu;
				MenuObjectActivator(_mainMenuPrefab);
				break;
			case MenuStates.GameModeMenu:
				_previousState = _currentState;
				_currentState = MenuStates.GameModeMenu;
				MenuObjectActivator(_gameModeMenu);
				break;
			/*case MenuStates.SettingsMenu:
				_previousState = _currentState;
				_currentState = MenuStates.SettingsMenu;
				MenuObjectActivator(_settingsMenuPrefab);
				break;*/
			case MenuStates.CampaignMenu:
				_previousState = _currentState;
				_currentState = MenuStates.CampaignMenu;
				MenuObjectActivator(_campaignMenuPrefab);
				break;
			case MenuStates.TimeTrialMenu:
				_previousState = _currentState;
				_currentState = MenuStates.TimeTrialMenu;
				MenuObjectActivator(_timeTrialMenuPrefab);
				break;
			case MenuStates.OutlastMenu:
				_previousState = _currentState;
				_currentState = MenuStates.OutlastMenu;
				MenuObjectActivator(_outlastMenuPrefab);
				break;
		}
	}

	//exit the game
	public void ExitGameButton()
	{
		Application.Quit();
	}

	private void MenuObjectActivator(GameObject activeMenu)
	{
		_mainMenuPrefab.SetActive(false);
		_gameModeMenu.SetActive(false);
		_campaignMenuPrefab.SetActive(false);
		_timeTrialMenuPrefab.SetActive(false);
		_outlastMenuPrefab.SetActive(false);
		//_settingsMenuPrefab.SetActive(false);

		activeMenu.SetActive(true);
	}
}
