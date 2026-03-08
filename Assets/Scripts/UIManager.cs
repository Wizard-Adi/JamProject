using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int playerHealth;

    public Text playerHP;

    // Update is called once per frame
    void Update()
    {
        playerHP.text ="Players HP" + playerHealth.ToString()+"/100";
    }
}
