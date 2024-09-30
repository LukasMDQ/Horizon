using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagTester : MonoBehaviour
{
    private int _counter;
    private readonly string[] _tags = {"FxTemporaire", "Enemy", "Floor", "Wall", "daño", "Static", "WeaponPlayer"};

    private void Start()
    {
        Testing(_tags);
    }

    private void Testing(string[] tagsToCompare)
    {
        foreach (var s in tagsToCompare)
        {
            var theTesterArray = GameObject.FindGameObjectsWithTag(s);

            foreach (var o in theTesterArray)
            {
                _counter++;
            }
        
            print($"Total number of GameObjects with tag {s} is {_counter}");
            
            _counter = 0;
        }
    }
}