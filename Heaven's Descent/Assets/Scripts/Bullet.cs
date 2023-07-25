using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject Exp;

    private EnemyDieSfx enemyDieSfx;
    private bool enemyDied = false;
    private GameObject enemyPos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {   
            enemyPos = other.gameObject;
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            StartCoroutine(DeathDelay());
            
            enemyDied = true;
            enemyDieSfx.checkEnemyDied(enemyDied);

            Instantiate(Exp, enemyPos.transform.position, enemyPos.transform.rotation);
        }
        else if (other.tag == "Wall")
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        enemyDieSfx = FindObjectOfType<EnemyDieSfx>();
        StartCoroutine(BulletDespawn());
    }

    IEnumerator BulletDespawn()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }    
     IEnumerator DeathDelay()
    {
        yield return 0;
    }    
}
