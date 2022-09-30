using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int sceneIndex;
    public int playerHealth = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public TextMeshProUGUI healthText;

    [SerializeField]private PlayerController playerController;

    void Awake() 
    {
        var gameManagerNum = FindObjectsOfType<GameManager>().Length;

        if (gameManagerNum > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() 
    {
        healthText = FindObjectOfType<TextMeshProUGUI>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update() 
    {
        healthText.text = "Health: " + playerHealth;

        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < playerHealth; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    public void TakeDamage()
    {
        playerHealth -= 1;
    }

    public void ProcessPlayerDeath()
    {
        if (playerHealth > 0)
        {
            RestartScene();
        }
        else
        {
            EscapeToMainMenu();
        }
    }

    public void EscapeToMainMenu()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        DOTween.KillAll();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (sceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
        DOTween.KillAll();
    }
}
