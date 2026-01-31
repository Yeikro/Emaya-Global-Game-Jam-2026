using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public PlayerRBController playerController;
    public float pushForceMultiplier = 5f;

    void Start()
    {
        
    }

    public void GetDamage(int damage, float stunDuration = 1.5f)
    {
        Debug.Log("Player received " + damage + " damage.");
        playerController.blockNormalMovement = true;
        playerController.rb.AddForce(playerController.desiredVel * pushForceMultiplier, ForceMode.Force);
        Invoke(nameof(ActivateNormalMovement), stunDuration);
    }

    private void ActivateNormalMovement()
    {
        playerController.blockNormalMovement = false;
    }

    void Update()
    {
        
    }
}
