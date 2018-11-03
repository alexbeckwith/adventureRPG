using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    virtual public bool Attack() {
        return true;
    }

    virtual public bool Attack(int direction) {
        return true;
    }
}
