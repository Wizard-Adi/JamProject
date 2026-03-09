using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int playerHealth;
    public int ShelterHealth;

    public Text playerHP;
    public Text shelterHpText;

    // Update is called once per frame
    void Update()
    {
        playerHP.text ="Players HP" + playerHealth.ToString()+"/100";
        shelterHpText.text = "Shelter HP" + ShelterHealth.ToString() + "/100";

    }
}
