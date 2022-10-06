using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake() 
    {
        audioSource.DOFade(0.5f, 3f).SetEase(Ease.InSine);
    }

    private IEnumerator fadeOutBGM()
    {
        audioSource.DOFade(0f, 3f);
        yield return new WaitForSeconds(3f);
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Play()
    {
        StartCoroutine(fadeOutBGM());
    }

    public void Quit()
    {
        Application.Quit();
    }

    private int GetCurrentBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}
