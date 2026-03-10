using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int playerHealth;
    public int ShelterHealth;
    public int HighScore;
    public string PlayerMoodStore ="";

    public SpawnManager spawn;

    public Text playerHP;
    public Text shelterHpText;
    public Text highScoreText;
    public Text playerMoodText;

    private void Start()
    {
        HighScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        spawn.PlayerScore = HighScore;

        playerHP.text ="Players HP" + playerHealth.ToString()+"/50";
        shelterHpText.text = "Shelter HP" + ShelterHealth.ToString() + "/100";
        highScoreText.text = "HighScore " + HighScore.ToString();
        playerMoodText.text = "Player's Mood :" + PlayerMoodStore;
    }
}
