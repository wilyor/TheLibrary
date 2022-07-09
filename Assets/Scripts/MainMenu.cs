using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playercamera;

    private void Start()
    {
        CameraSwitcher.Register(playercamera);
        CameraSwitcher.SwitchCamera(playercamera);
    }
    public void StartGame()
    {
        CameraSwitcher.SwitchCamera(playercamera);
        StartCoroutine(StartAfterDelay());
    }

    IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(1);
        GameObject.Find("Canvas").GetComponent<LibrarySceneCanvasManager>().ShowCurtain();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Library");

    }

    public void Exit()
    {
        Application.Quit();
    }
}
