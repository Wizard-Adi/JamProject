using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("kaam kar ja bhai " +  other.gameObject.tag);

        if (other != null)
        {
            Debug.Log("hit :" + other.gameObject.tag);
        }
    }
}
