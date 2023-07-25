using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtSfx : MonoBehaviour
{
    [SerializeField]
    private AudioSource playerHurtSfx;

    public void checkIfPlayerHurt(bool playerHurt)
    {
        if (playerHurt == true)
        {
            playerHurtSfx.Play();
        }
    }
}
