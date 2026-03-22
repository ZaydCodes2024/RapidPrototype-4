using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private float footstepTimer;
    private float footstepTimerMax = .7f;
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0)
        {
            footstepTimer = footstepTimerMax;

            if (playerMovement.IsWalking())
                SoundManager.Instance.PlayFootstepSound(transform.position, 0.5f);
        }
    }
}
