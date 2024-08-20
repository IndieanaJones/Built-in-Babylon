using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{
    public static EscapeMenu Instance;
    public bool IsPaused;
    public GameObject EscapeMenuCanvas;

    public AudioSource GameMusic;

    public void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        if (PlayerSpawner.ThePlayerRef == null)
            IsPaused = false;
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            DirectCameraSound.Instance.PlaySound("Pickaxe Contact Non-Rock");
            IsPaused = !IsPaused;
        }
        EscapeMenuCanvas.SetActive(IsPaused);
        GameMusic.volume = IsPaused ? 0.1f : 0.25f;
        GameMusic.volume = (PlayerSpawner.ThePlayerRef == null || AngelTalker.Instance.GameIsOver || WinScreen.Instance.WinScreenUp) ? 0.0f : GameMusic.volume;
        Cursor.lockState = (IsPaused || PlayerSpawner.ThePlayerRef == null || ) ? CursorLockMode.None : CursorLockMode.Locked;
        Time.timeScale = (IsPaused || PlayerSpawner.ThePlayerRef == null) ? 0 : 1;
    }

    public void ContinuePressed()
    {
        DirectCameraSound.Instance.PlaySound("Pickaxe Contact Non-Rock");
        IsPaused = false;
    }

    public void GoToMainMenuPressed()
    {
        DirectCameraSound.Instance.PlaySound("Pickaxe Contact Non-Rock");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
