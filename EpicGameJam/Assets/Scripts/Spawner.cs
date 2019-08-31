using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    
    [System.Serializable]
    public class Wave
    {
        public GameObject[] zombies;
    }

    public Wave[] waves;

    protected int currentWave = 0;

    private void Awake ()
    {
        instance = this;
    }

    public void FirstWave ()
    {
        if (waves.Length > 0)
        {
            foreach (GameObject zombie in waves[0].zombies)
            {
                zombie.SetActive(true);
            }
        }
        else
        {
            enabled = false;
        }
    }

    public void NextWave ()
    {
        currentWave++;

        if (waves.Length > currentWave)
        {
            foreach (GameObject zombie in waves[0].zombies)
            {
                zombie.SetActive(true);
            }
        }
        else
        {
            enabled = false;
        }
    }

    private void Update ()
    {
        foreach (GameObject zombie in waves[currentWave].zombies)
        {
            if (zombie != null)
                return;
        }

        NextWave();
    }
}
