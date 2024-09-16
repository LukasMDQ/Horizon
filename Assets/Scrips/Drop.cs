using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public int value = default;
    [SerializeField] GameObject _effect;
    [SerializeField] bool _reload, _heal, _maxHp;           
    private void OnTriggerEnter(Collider other)
    {       
        if (other.TryGetComponent(out Stats stats))//el efecto del item varia dependiendo del bool
        {  
            if(_reload) stats.Reload(value);            
            if(_heal) stats.Heal(value);
            if(_maxHp) stats.MaxLifeUp(value);
            Destruction();
        }
    }
    void Destruction()//instancia efecto y destruye el prefab
    {
       // Instantiate(_effect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
