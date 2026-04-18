using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Data;
using System.IO;
using System;
public class WaveSpawner : MonoBehaviour
{
    // Lets names of enemies map to their prefabs in the scene
    public List<GameObjectMapping> enemyNameMappings; 
    private Dictionary<string, GameObject> enemyNameToPrefab;

    public GameObject player;
    private WaveList waveList;
    [Header("Enter the path relative to Resources **WITHOUT** the file extension (no .json)")]
    public string fileNameWaves = "TestDifficulty";

    private double timer;
    private int currentWaveIndex = 0;
    private int currentSubwaveIndex = 0;

    void Update()
    {
        CheckEnemySpawn();
    }

    void Start()
    {
        // Parse the wave 
        string path = Path.Combine("Waves", fileNameWaves);
    
        UnityEngine.Debug.Log(path);
        TextAsset file = Resources.Load<TextAsset>("TestDifficulty");
        UnityEngine.Debug.Log(file);
        string json = file.text;
        waveList = JsonUtility.FromJson<WaveList>(json);
        foreach(var wave in waveList.waves)
        {
            UnityEngine.Debug.Log(wave);
            UnityEngine.Debug.Log(wave.waveNumber);
            UnityEngine.Debug.Log(wave.subwaves[0]);
            UnityEngine.Debug.Log(wave.subwaves[0].spawnInterval);
        }
        UnityEngine.Debug.Log(waveList);
    
        

        // Create map of enemy names to their prefabs for instantiation
        enemyNameToPrefab = new Dictionary<string, GameObject>();
        foreach (GameObjectMapping o in enemyNameMappings) {
            enemyNameToPrefab[o.key] = o.value; 
        }
        UnityEngine.Debug.Log(enemyNameToPrefab);
    }

    void CheckEnemySpawn()
    {
        timer += Time.deltaTime;

        if (timer >= waveList.waves[currentWaveIndex].subwaves[currentSubwaveIndex].spawnInterval)
        {
            SpawnEnemy(waveList.waves[currentWaveIndex].subwaves[currentSubwaveIndex].type);
            timer -= waveList.waves[currentWaveIndex].subwaves[currentSubwaveIndex].spawnInterval;
        }
    }

    void SpawnEnemy(string enemyName)
    {
        GameObject enemy = Instantiate(enemyNameToPrefab[enemyName]);
        enemy.transform.position = transform.position;
        enemy.GetComponent<EnemyController>().player = player;
    }
}