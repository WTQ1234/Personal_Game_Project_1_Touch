using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public bool can_walk = true;

    void Awake()
    {
        Instance = this;
    }
}
