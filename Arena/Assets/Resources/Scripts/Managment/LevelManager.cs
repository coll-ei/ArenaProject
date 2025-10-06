using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("For spawn")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] enemiesPrefab;

    [Header("For wave")]
    [SerializeField] private int _countPerWave;
    [SerializeField] private int _waveBuff;
    [SerializeField] private float _timeBetweenWave;
    private int _enemysAlive;
    private int _wave;

    [Header("UI")]
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private Text _aliveEnemytext;

    [SerializeField] private Text _countWaveText;
    [SerializeField] private Text _enemiesCountText;
    [SerializeField] private GameObject _newWaveMenu;

    public static LevelManager Instance;


    private void Awake()
    {
        Time.timeScale = 1.0f;

        Instance = this;
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        _newWaveMenu.SetActive(true);

        _wave++;
        _countPerWave += _waveBuff;

        _countWaveText.text = "Wave: " + _wave.ToString();
        _enemiesCountText.text ="Enemies on " +
            "that wave: " +  _countPerWave.ToString();

        yield return new WaitForSeconds(_timeBetweenWave);

        _newWaveMenu.SetActive(false);

        for (int i = 0; i < _countPerWave; i++)
        {
            GameObject enemyForSpawn = enemiesPrefab[RandomNum(0, enemiesPrefab.Length)];
            Vector3 positionForSpawn = spawnPoints[RandomNum(0, spawnPoints.Length)].position;

            Instantiate(enemyForSpawn, positionForSpawn, Quaternion.identity);
        }

        _enemysAlive = _countPerWave;
        _aliveEnemytext.text = "Alive enemies: " + _enemysAlive.ToString();

    }
   

    private int RandomNum(int first, int last)
    {
        return UnityEngine.Random.Range(first, last);
    }

    public void CheckCount()
    {
        _enemysAlive--; 
        _aliveEnemytext.text = "Alive enemies: " + _enemysAlive.ToString();
        if (_enemysAlive == 0) StartCoroutine(Spawn());
    }

    public void PlayerDeath()
    {
        Time.timeScale = 0;
        _deathMenu.SetActive(true);
    }

    public void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
}
