using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Reward
{
    public string Name;
    public string Description;
    public bool Unlocked;

    public Reward(string name, string description)
    {
        Name = name;
        Description = description;
        Unlocked = false;
    }
}
