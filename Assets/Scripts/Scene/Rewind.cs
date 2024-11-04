using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rewind : MonoBehaviour
{
    protected MementoState _mementoState;

    private void Awake()
    {
        _mementoState = new MementoState();
    }

    public abstract void Save();
    public abstract void Load();
}
