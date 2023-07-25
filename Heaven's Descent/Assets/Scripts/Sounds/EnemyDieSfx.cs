using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieSfx : MonoBehaviour
{
  
    [SerializeField]
    private AudioSource enemyDiedSfx;

    // Accessed by the bullet script, where when an enemy is destroyed the sfx is played
    public void checkEnemyDied(bool enemyDied)
    {
        if (enemyDied == true)
        {
            enemyDiedSfx.Play();
        }
    }
}
