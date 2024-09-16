using TMPro;
using UnityEngine;
//using JetBrains.Annotations;
//using Unity.VisualScripting;

public class Stats : MonoBehaviour
{
    //-------REFERENCE
    //-------TEXT    
    //-------AUDIO
    //-------ANIMATION
    //---------UI-------
    [SerializeField] TextMeshProUGUI textBullet;    
    [SerializeField] GameObject _lostMenu;
    //[SerializeField] private GameObject _winMenu, _lostMenu;
    //[SerializeField] public UI ui;
    //[SerializeField] public cooldown cooldown;
    //public TextMeshProUGUI textScore, textPoints, textHp;   
    //---------STATS----    
    public int score, points, damage = default;
    public float curHp, maxHp = default;    
    public int shieldLvl = default;
    public int bulletCount, maxBulletCount = default;

    public Animator anim;
    private AudioSource _spawnSound = default;
    [SerializeField] private AudioClip[] _sounds = default;
    //-----SHIELD
    public GameObject shield;
    [SerializeField] private GameObject shielUi;
    public float shieldCooldown = default;
    public float shieldTime = default;
    //-----ATTACK    
    void Start()
    {
        shieldCooldown = shieldTime;
        anim = GetComponent<Animator>();
        curHp = maxHp;
        //ui.StartHpBar(curHp);
        //Shield
        //cooldown.Startshield(shieldCooldown);
        if (!_spawnSound) _spawnSound = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (curHp > maxHp) curHp = maxHp;
        //ui.ChangeCurrentHp(curHp);
        //cooldown.ChangeCurshield(shieldCooldown);        
        //textScore.text = score.ToString();
        //textPoints.text = points.ToString();        
        //textHp.text = curHp.ToString();
        ActiveShield();
        ShieldCooldown();
        Death();
        textBullet.text = bulletCount.ToString();
    }

    //--------------Alteraciones-----------    
    public void Heal(int HealPower)//CURAR
    {
        curHp += HealPower;
        Debug.Log("Curado");
    }
    public void Buff(int powerUp)//BUFF
    {
        damage += powerUp;
        Debug.Log("Bufeado");
    }
    public void MaxLifeUp(int HealPower)//AUMENTAR VIDA MAXIMA
    {
        maxHp += HealPower;
        Debug.Log("VIDA MAXIMA AUMENTADA! ");
    }
   
    void Death()//MUERTE
    {
        if (curHp <= 0)
        {
            _lostMenu.SetActive(true);
            Time.timeScale = 0;            
        }
    }

    public void TakeDamage(int dmg)//RECIBIR DAÑO
    {
        curHp -= dmg;        
       // if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[0]);

    }
    public void Reload(int ammoValue)//RECARGAR
    {
        if (bulletCount < maxBulletCount)
            bulletCount += ammoValue;
        Debug.Log("RECARGA");
    }
    //----------Escudo-------       
    public void ActiveShield()//ACTIVAR ESCUDOS
    {

        if (Input.GetKey(KeyCode.Q) && shieldCooldown == shieldTime && shieldLvl >= 1 && !shield.activeSelf)
        {
            _spawnSound.PlayOneShot(_sounds[2]);
            shield.SetActive(true);
            shieldCooldown = 0;
        }
    }
    public void ShieldCooldown()
    {
        //cooldown.ChangeCurshield(shieldCooldown);
        shieldCooldown += Time.deltaTime;
        if (shieldCooldown >= shieldTime) shieldCooldown = shieldTime;
    }
}