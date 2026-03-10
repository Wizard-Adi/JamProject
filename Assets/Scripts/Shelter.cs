using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelter : MonoBehaviour, Damagable
{

    //public SpriteRenderer _shelterSprite;
    public UIManager shelterHP;
    public SpawnManager EnemiesSpawn;
    public PauseMenu destroyinfo;

    private int _enemyDamage;

    //[SerializeField]
    //public GameObject[] Shelters;
    //private int i = 0;

    public int Health { get; set; }

    void Start()
    {
        Health = 100;
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

            //ShowShelter();
        }
    }
    public void Damage()
    {
        _enemyDamage = Random.Range(5, 10);

        Health -= _enemyDamage;
        shelterHP.ShelterHealth = Health;

        if (Health <= 0)
        {
            destroyinfo.ShelterDestroyed = true;

            //Destroy(gameObject);

            EnemiesSpawn.OnShelterDestroy();

            //destroyinfo.ShelterDestroyed = true;

            //Application.Quit();
        }
    }

    //void ShowShelter()
    //{
    //    if (Health >= 8)
    //    {
    //        Shelters[i].SetActive(true);
    //        i++;
    //    }
    //    else if (Health == 7)
    //    {
    //        Shelters[i - 1].SetActive(false);
    //        Shelters[i].SetActive(true);
    //        i++;
    //    }
    //    else if (Health == 6)
    //    {
    //        Shelters[i - 1].SetActive(false);
    //        Shelters[i].SetActive(true);
    //        i++;
    //    }
    //    else if (Health <= 5)
    //    {
    //        Shelters[i - 1].SetActive(false);
    //        Shelters[i].SetActive(true);
    //        i++;
    //    }
    //}
    
}
