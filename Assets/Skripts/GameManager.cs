using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string enemyTag = "Enemy";
    public string victorySceneName = "VictoryScene";
    public string defeatSceneName = "LostScene";
    public TextMeshProUGUI textCurEnemy;
    public TextMeshProUGUI textCurBullet;

    private PlayerControler _player;
    private Shooter _shot;

    private void Start()
    {
        _player = FindObjectOfType<PlayerControler>();
        _shot = FindObjectOfType<Shooter>();
    }

    void Update()
    {
        if (AreAllEnemiesDestroyed())
            LoadVictoryScene();

        if(_player.IsAlive == false)
            LoadLostScene();

        textCurBullet.text = _shot.curBullet.ToString();
    }

    bool AreAllEnemiesDestroyed()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        textCurEnemy.text = "Remained: " + enemies.Length;
        return enemies.Length == 0;
    }

    void LoadVictoryScene()
    {
        SceneManager.LoadScene(victorySceneName);
    }

    void LoadLostScene()
    {
        SceneManager.LoadScene(defeatSceneName);
    }
}
