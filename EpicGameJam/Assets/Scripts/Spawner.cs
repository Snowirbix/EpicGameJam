using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject Thrower;

    public GameObject Runner;

    public GameObject Jumper;

    public GameObject Exploder;
    
    [System.Serializable]
    public class Wave
    {
        public Spawn[] spawns;
    }

    [System.Serializable]
    public class Spawn
    {
        public Transform spawnPoint;
        public int Thrower;
        public int Runner;
        public int Jumper;
        public int Exploder;
    }

    public Wave[] waves;

    public List<GameObject> zombies = new List<GameObject>();

    protected int currentWave = 0;

    private void Awake ()
    {
        instance = this;
    }

    public Light sun;
    protected float transitionTime;
    protected bool transition = false;
    protected bool started = false;

    public void StartWaves ()
    {
        transitionTime = Time.time;
        transition = true;
    }

    public void FirstWave ()
    {
        if (waves.Length > 0)
        {
            foreach (Spawn spawn in waves[0].spawns)
            {
                for (int i = 0; i < spawn.Thrower; i++)
                {
                    zombies.Add(Instantiate(Thrower, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
                for (int i = 0; i < spawn.Runner; i++)
                {
                    zombies.Add(Instantiate(Runner, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
                for (int i = 0; i < spawn.Jumper; i++)
                {
                    zombies.Add(Instantiate(Jumper, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
                for (int i = 0; i < spawn.Exploder; i++)
                {
                    zombies.Add(Instantiate(Exploder, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
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
        zombies.Clear();

        if (waves.Length > currentWave)
        {
            foreach (Spawn spawn in waves[0].spawns)
            {
                for (int i = 0; i < spawn.Thrower; i++)
                {
                    zombies.Add(Instantiate(Thrower, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
                for (int i = 0; i < spawn.Runner; i++)
                {
                    zombies.Add(Instantiate(Runner, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
                for (int i = 0; i < spawn.Jumper; i++)
                {
                    zombies.Add(Instantiate(Jumper, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
                for (int i = 0; i < spawn.Exploder; i++)
                {
                    zombies.Add(Instantiate(Exploder, spawn.spawnPoint.position, spawn.spawnPoint.rotation));
                }
            }
        }
        else
        {
            enabled = false;
        }
    }

    private void Update ()
    {
        if (transition)
        {
            float r = Mathf.Clamp01(Time.time - transitionTime);
            sun.intensity = 1-r;

            if (r == 1)
            {
                transition = false;
                started = true;
                FirstWave();
            }
        }
        if (zombies.Count == 0)
            return;

        foreach (GameObject zombie in zombies)
        {
            if (zombie != null)
                return;
        }

        Debug.Log("NEXT WAVE");
        NextWave();
    }

    public void DeleteZombies()
    {
        foreach(GameObject zombie in zombies)
        {
            Destroy(zombie);
        }
    }
}
