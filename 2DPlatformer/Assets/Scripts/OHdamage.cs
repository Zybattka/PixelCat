using UnityEngine;
using System.Collections;

public class OHdamage : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject)
        {
            Destroy(gameObject);
        }
    }
}
