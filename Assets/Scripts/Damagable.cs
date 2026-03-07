using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damagable
{
    int Health{ get; set; }

    void Damage();
}
