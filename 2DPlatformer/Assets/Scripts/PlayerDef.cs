using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDef : MonoBehaviour
{
    private bool hasEntered;


    private void OnCollisionEnter2D(Collision2D other)
    {
      if (other.gameObject.CompareTag("Enemy") && !hasEntered)
      {
        Destroy(gameObject);
        LevelManager.instance.Respawn();
      }
    }
}
