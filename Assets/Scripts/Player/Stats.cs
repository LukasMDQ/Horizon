using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using JetBrains.Annotations;
//using Unity.VisualScripting;
// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

// ReSharper disable once CheckNamespace
public class Stats : MonoBehaviour
{
    //-------REFERENCE
    //-------TEXT    
    //-------AUDIO
    //-------ANIMATION
    //---------UI-------
    [SerializeField] private TextMeshProUGUI textBullet;
    // ReSharper disable once InconsistentNaming
    [SerializeField] private GameObject _lostMenu;
    //[SerializeField] private GameObject _winMenu, _lostMenu;
    //[SerializeField] public UI ui;
    //[SerializeField] public cooldown cooldown;
    //public TextMeshProUGUI textScore, textPoints, textHp;   
    //---------STATS----    
    public int score, points, damage;
    public float curHp, maxHp;
    public int jewels;
    public float stamina, maxStamina;
    public int shieldLvl;
    public int bulletCount, maxBulletCount;

    public Animator anim;
    private AudioSource _spawnSound;
    // ReSharper disable once InconsistentNaming
    [SerializeField] private AudioClip[] _sounds;
    //-----SHIELD
    public GameObject shield;
    [SerializeField] private GameObject shieldUi;
    public float shieldCooldown;
    public float shieldTime;
    //-----ATTACK    
    public Image hpBar;
    public Image staminaBar;
    public float hpBarAmount = 100f;
    public float staminaBarAmount = 100f;
    public float ChargeRate;
    public CanvasGroup hpVignette;
    //--------HUD
    private void Start()
    {
        shieldCooldown = shieldTime;
        anim = GetComponent<Animator>();
        curHp = maxHp;
        stamina = maxStamina;
        //ui.StartHpBar(curHp);
        //Shield
        //cooldown.Startshield(shieldCooldown);
        if (!_spawnSound) _spawnSound = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (curHp > maxHp) curHp = maxHp;
        if (stamina > maxStamina) stamina = maxStamina;
        if (stamina <= 0) stamina = 0;
        //ui.ChangeCurrentHp(curHp);
        //cooldown.ChangeCurshield(shieldCooldown);        
        //textScore.text = score.ToString();
        //textPoints.text = points.ToString();        
        //textHp.text = curHp.ToString();
        ActiveShield();
        ShieldCooldown();
        Death();
        UIUpdate();
        textBullet.text = bulletCount.ToString();
    }

    //--------------Alteraciones-----------    
    public void Heal(int healPower)//CURAR
    {
        curHp += healPower;
        Debug.Log("Curado");
    }
    public void AddJewel(int jewelCount)//AÑADIR GEMA
    {
        jewels += jewelCount;
        Debug.Log("gema añadida al grial");
    }
    public void Buff(int powerUp)//BUFF
    {
        damage += powerUp;
        Debug.Log("Bufeado");
    }
    public void MaxLifeUp(int healPower)//AUMENTAR VIDA MAXIMA
    {
        maxHp += healPower;
        Debug.Log("VIDA MAXIMA AUMENTADA! ");
    }

    public void Death()//MUERTE
    {
        if (curHp <= 0)
        {
            _lostMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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

    //----------UI-------   

    public void UIUpdate()
    {
        hpBar.fillAmount = curHp / 100f;
        hpVignette.alpha = 1 - (curHp / maxHp);
        staminaBar.fillAmount = stamina / 100f; 
    }
}