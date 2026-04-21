using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Diagnostics;
using System.Data;
using System.IO;
using System;
using UnityEngine.InputSystem;
using TMPro;


public class WaveSpawner : MonoBehaviour
{
    // Lets names of enemies map to their prefabs in the scene
    public List<GameObjectMapping> enemyNameMappings;
    private Dictionary<string, GameObject> enemyNameToPrefab;

    public GameObject player;
    public TMP_Text winText;

    private WaveList waveList;
    [Header("Enter the path relative to Resources **WITHOUT** the file extension (no .json)")]
    public string fileNameWaves = "TestDifficulty";

    // Wave Tracking
    private double timer;
    private int currentWaveIndex = 0;
    public WaveText wave;
    public GameObject bossPrefab;



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
        foreach (GameObjectMapping o in enemyNameMappings)
        {
            enemyNameToPrefab[o.key] = o.value;
        }
        wave.SetText("Wave " + currentWaveIndex);

        print("Press Space to start spawning waves");
    }

    void StartWave()
    {
        wave.SetText("Wave " + (currentWaveIndex + 1));
        for (int i = 0; i < waveList.waves[currentWaveIndex].subwaves.Count; i++)
        {
            StartCoroutine(SpawnWave(waveList.waves[currentWaveIndex].subwaves[i]));
        }
        currentWaveIndex++;

    }

    IEnumerator SpawnWave(Subwave wave)
    {
        yield return new WaitForSeconds(wave.startTime);
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.type);
            yield return new WaitForSeconds((float)wave.spawnInterval);
        }
    }

    void SpawnEnemy(string enemyName)
    {
        // Spawn the enemy
        GameObject enemy = Instantiate(enemyNameToPrefab[enemyName]);
        enemy.transform.position = pathManager.getStartingPosition();
        enemy.GetComponent<EnemyController>().player = player;
        enemy.GetComponent<EnemyController>().SetPathVectors(pathManager.getPathVectors());
        WinCon wc = enemy.GetComponent<WinCon>();
        if (wc!=null)
        {
            wc.messageText = winText;
            Camera.main.GetComponent<MusicScript>().swapSong(1);
        }
    }
}
