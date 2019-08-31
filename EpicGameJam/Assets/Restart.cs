using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour
{

   public void Respawn()
    {
        PlayerController.instance.Respawn();
        PlayerController.instance.animator.SetTrigger("BackAliveBaby");
        Spawner.instance.DeleteZombies();
        Spawner.instance.zombies.Clear();
        Spawner.instance.FirstWave();
    }
}
