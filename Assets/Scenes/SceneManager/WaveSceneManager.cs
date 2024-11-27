using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSceneManager : MonoBehaviour
{
    // Escena de la oleada actual
    private string currentWaveScene;

    // Número de oleada inicial
    [SerializeField] private int initialWaveNumber;
    private void Start()
    {
        // Inicia la oleada inicial al arrancar el juego
        if (initialWaveNumber > 0)
        {
            StartWave(initialWaveNumber);
        }
    }

    // Método público para iniciar una oleada
    public void StartWave(int waveNumber)
    {
        // Nombre de la escena de la oleada
        string waveSceneName = $"Oleada_{waveNumber}";

        // Comprueba si ya hay una oleada cargada y la descarga
        if (!string.IsNullOrEmpty(currentWaveScene))
        {
            UnloadCurrentWave();
        }

        // Carga la nueva escena de oleada
        LoadWaveScene(waveSceneName);
    }

    // Método público para finalizar la oleada actual
    public void EndWave()
    {
        // Comprueba si hay una oleada activa y la descarga
        if (!string.IsNullOrEmpty(currentWaveScene))
        {
            UnloadCurrentWave();
        }
    }

    // Carga una escena de oleada
    private void LoadWaveScene(string waveSceneName)
    {
        SceneManager.LoadSceneAsync(waveSceneName, LoadSceneMode.Additive);
        currentWaveScene = waveSceneName;
        Debug.Log($"Cargando oleada: {waveSceneName}");
    }

    // Descarga la escena de oleada actual
    private void UnloadCurrentWave()
    {
        if (!string.IsNullOrEmpty(currentWaveScene))
        {
            SceneManager.UnloadSceneAsync(currentWaveScene);
            Debug.Log($"Descargando oleada: {currentWaveScene}");
            currentWaveScene = null;
        }
    }
}
