using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectExpSfx : MonoBehaviour
{
    [SerializeField]
    private AudioSource coinCollectedSfx;

    public void checkCoinCollect(bool coinCollected)
    {
        if(coinCollected == true)
        {
            coinCollectedSfx.Play();
        }
    }
}
