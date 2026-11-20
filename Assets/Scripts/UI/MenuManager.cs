using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _menus;
    [SerializeField] private EMenu _startingMenu;
    [SerializeField] private GamePause _pauseManager;
    [SerializeField] private PlayerInput _playerInput;

    private EMenu _currentMenu;
    private EMenu _lastMenu;

    public void OpenMenu(EMenu menu)
    {
        if (menu == EMenu.none)
        {
            CloseMenus();
            return;
        }

        for (int i = 0; i < _menus.Count; i++)
        {
            if (i == (int)menu - 1)
            {
                _menus[i].SetActive(true);
            }
            else
            {
                _menus[i].SetActive(false);
            }
        }

        _playerInput.SwitchCurrentActionMap("Menu");
        _lastMenu = _currentMenu;
        _currentMenu = menu;
        _pauseManager.PauseGame();
    }

    public void OpenMenu(int menuIndex)
    {
        OpenMenu((EMenu)menuIndex);
    }

    public void GoToLastMenu()
    {
        CloseMenus();
        OpenMenu(_lastMenu);
    }

    public void CloseMenus()
    {
        foreach (GameObject menu in _menus)
        {
            menu.SetActive(false);
        }

        _playerInput.SwitchCurrentActionMap("Walking");
        _currentMenu = EMenu.none;
        _pauseManager.UnpauseGame();
    }

    private void Start()
    {
        OpenMenu(_startingMenu);
    }
}
