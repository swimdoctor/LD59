using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Data;
using System.IO;
using System;
using UnityEngine.InputSystem;

public class WaveSpawner : MonoBehaviour
{
    // Lets names of enemies map to their prefabs in the scene
    public List<GameObjectMapping> enemyNameMappings; 
    private Dictionary<string, GameObject> enemyNameToPrefab;

    public GameObject player;
    private WaveList waveList;
    [Header("Enter the path relative to Resources **WITHOUT** the file extension (no .json)")]
    public string fileNameWaves = "TestDifficulty";

    // Wave Tracking
    private double timer;
    private int currentWaveIndex = 0;
    private int currentSubwaveIndex = 0;
    private int enemiesSpawnedInSubwave = 0;
    private bool pauseEnemySpawn = true;

    // PathingManager
    public PathManager pathManager;

    void Update()
    {
        // FIXME: Replace with a button in UI later to start the wave
        // Might want this in some other area, but the call to StartWave() should be in this class
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartWave();
        }
        CheckEnemySpawn();
    }

    void Start()
    {
        // Parse the wave 
        string path = Path.Combine("Waves", fileNameWaves);
        TextAsset file = Resources.Load<TextAsset>(path);
        UnityEngine.Debug.Log(file);
        string json = file.text;
        waveList = JsonUtility.FromJson<WaveList>(json);
    
        // Create map of enemy names to their prefabs for instantiation
        enemyNameToPrefab = new Dictionary<string, GameObject>();
        foreach (GameObjectMapping o in enemyNameMappings) {
            enemyNameToPrefab[o.key] = o.value; 
        }

        print("Press Space to start spawning waves");
    }

    void StartWave() {
        pauseEnemySpawn = false;
        timer = 0;
    }

    void CheckEnemySpawn()
    {
        if(!pauseEnemySpawn){
            timer += Time.deltaTime;

            // If the next enemy's time is up, spawn it in
            if (timer >= waveList.waves[currentWaveIndex].subwaves[currentSubwaveIndex].spawnInterval)
            {
                SpawnEnemy(waveList.waves[currentWaveIndex].subwaves[currentSubwaveIndex].type);
                timer -= waveList.waves[currentWaveIndex].subwaves[currentSubwaveIndex].spawnInterval;
            }
        }
    }

    void SpawnEnemy(string enemyName)
    {
        // Spawn the enemy
        GameObject enemy = Instantiate(enemyNameToPrefab[enemyName]);
        enemy.transform.position = pathManager.getStartingPosition();
        enemy.GetComponent<EnemyController>().player = player;
        enemy.GetComponent<EnemyController>().SetPathVectors(pathManager.getPathVectors());

        // Update the counters
        if(waveList.waves[currentWaveIndex].subwaves[currentSubwaveIndex].count > enemiesSpawnedInSubwave + 1) {
            // If we have more enemies to spawn in the wave, spawn them
            enemiesSpawnedInSubwave++;
        } else {
            enemiesSpawnedInSubwave = 0;
            // otherwise set up next spawn counters for the next wave
            if(waveList.waves[currentWaveIndex].subwaves.Count > currentSubwaveIndex + 1) {
                // Increment if there's another enemy in the subwave left
                currentSubwaveIndex++;
            } else {
                currentSubwaveIndex = 0;
                pauseEnemySpawn = true;
                if(waveList.waves.Count > currentWaveIndex + 1) {
                    // If there's another wave, increment it the counter so the next spawn is in the new wave
                    currentWaveIndex++;
                    pauseEnemySpawn = true;
                    print("End of wave spawns. Press space to start new wave.");
                } else {
                    // FIXME: End of game. There's no more waves
                }
            }
        }
    }
}