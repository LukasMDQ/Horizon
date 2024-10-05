using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public int value;
    [SerializeField] GameObject _effect;
    [SerializeField] bool _ammo, _heal, _maxHp, _jewel;
    public ChaliceAnimation chalice;
    private void OnTriggerEnter(Collider other)
    {       
        if (other.TryGetComponent(out Stats stats) && other.CompareTag("Player")) //el efecto del item varia dependiendo del bool
        {  
            if (_ammo) stats.Reload(value);
            if (_jewel)
            {
                stats.AddJewel(value);
                chalice.JewelUpdate();
            }
            if (_heal) stats.Heal(value);
            if (_maxHp) stats.MaxLifeUp(value);
            Destruction();
        }
    }

    private void Destruction() //instancia efecto y destruye el prefab
    {
       // Instantiate(_effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}