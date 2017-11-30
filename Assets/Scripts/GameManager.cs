using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {
    public CanvasGroup menePanel;
    public CanvasGroup gamePanel;
    public CanvasGroup missionSuccessPanel;
    public CanvasGroup missionFailPanel;
    public CrystalController crystalController;
    public Spawner spawner;
    public float delayShowResultTime = 2f;
    public bool isGameStart;

    private void Awake()
    {
        Pause();
    }
    public void Pause()
    {
        Time.timeScale = 0;
        menePanel.gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        menePanel.gameObject.SetActive(false);
        if (isGameStart == false)
            StartCoroutine(DoGameFlow());
    }

    private IEnumerator DoGameFlow()
    {
        isGameStart = true;
        gamePanel.gameObject.SetActive(true);
        yield return StartCoroutine(crystalController.Execute());
        spawner.enabled = false;
        yield return new WaitForSeconds(delayShowResultTime);
        if(crystalController.isDead)
        {
            missionFailPanel.gameObject.SetActive(true);
        }
        else
        {
            missionSuccessPanel.gameObject.SetActive(true);
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }
}
