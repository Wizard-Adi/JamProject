using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour, Damagable
{

    //public SpriteRenderer _shelterSprite;
    public UIManager shelterHP;
    public SpawnManager EnemiesSpawn;

    [SerializeField]
    public GameObject[] Shelters;
    private int i = 0;

    public int Health { get; set; }

    void Start()
    {
        Health = 10;
        shelterHP.ShelterHealth = Health;

        //_shelterSprite = GetComponent<SpriteRenderer>();
    }

    //void Update()
    //{
    //    if (Health == 5)
    //    {
    //        _shelterSprite.transform.localScale = new Vector2(2f, 2f);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "EnemyAttack")
        {
            Damage();
            ShowShelter();
        }
    }
    public void Damage()
    {
        Health--;
        shelterHP.ShelterHealth = Health;

        if (Health <= 0)
        {
            Destroy(gameObject);
            EnemiesSpawn.OnShelterDestroy();
            Application.Quit();
        }
    }

    void ShowShelter()
    {
        if (Health >= 8)
        {
            Shelters[i].SetActive(true);
            i++;
        }
        else if (Health == 7)
        {
            Shelters[i - 1].SetActive(false);
            Shelters[i].SetActive(true);
            i++;
        }
        else if (Health == 6)
        {
            Shelters[i - 1].SetActive(false);
            Shelters[i].SetActive(true);
            i++;
        }
        else if (Health <= 5)
        {
            Shelters[i - 1].SetActive(false);
            Shelters[i].SetActive(true);
            i++;
        }
    }
    
}
