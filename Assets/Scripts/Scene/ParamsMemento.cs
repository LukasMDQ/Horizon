using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamsMemento 
{
    public object[] parameters;

    public ParamsMemento(params object[] p)
    {
        parameters = p;
    }
}
