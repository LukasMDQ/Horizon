using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
//TP2 - Hernandez Lucas - Luchetti, Nicolás - Paiva, Lidia - Ahumada, Leandro

public class Stats : Entity,IinstaKill
{
    [SerializeField] private GameObject _lostMenu;
    public Image hpBar;
    public Image hpBarLerp;
    public Image staminaBar;
    public CanvasGroup hpVignette;

    public float stamina, maxStamina;
    public int damage;
    [FormerlySerializedAs("ChargeRate")] public float chargeRate;

    private bool _loading;
    private float lerpSpeed = 0.05f; 

    protected override void Start()
    {

        stamina = maxStamina;
        base.Start();

        // Assign PlayerStatsManager values to current player stats
        Debug.Log($"PlayerStatsManager.MaxHP;{PlayerStatsManager.MaxHP}");
        Debug.Log($"PlayerStatsManager.HP;{PlayerStatsManager.HP}");
        Debug.Log($"maxHp;{maxHp}");
        Debug.Log($"curHp;{curHp}");
        Debug.Log($"PlayerStatsManager;{PlayerStatsManager.Damage}");
        maxHp = PlayerStatsManager.MaxHP;
        curHp = PlayerStatsManager.HP;
        damage = PlayerStatsManager.Damage;

        UpdateHealthHud();
    }

    private void Update()
    {
        UpdateStamina();
        HudUpdate();
    }

    //----------- Stamina Management -----------  
    #region Stamina Management

    private void UpdateStamina()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        if (stamina < 0) stamina = 0;
    }
    #endregion

    //----------- Player-Specific Actions -----------  
    #region Player-Specific Actions
    public void AddJewel(int jewelCount)
    {
        PlayerStatsManager.jewels += jewelCount;
        Debug.Log("Jewel added to chalice");
    }

    public void Buff(int powerUp)
    {
        damage += powerUp;
        PlayerStatsManager.Damage = damage;
        Debug.Log($"Buff applied {PlayerStatsManager.Damage}");
    }

    public void Debuff(int debuffValue)
    {
        damage -= debuffValue;
        PlayerStatsManager.Damage = damage;
        Debug.Log($"Debuff applied {PlayerStatsManager.Damage}");
    }

    public void MaxLifeUp(int healPower)
    {
        maxHp += healPower;
        PlayerStatsManager.MaxHP = (int)maxHp; // Save the updated max health in PlayerStatsManager
        Debug.Log("Max life increased!");
    }

    public override void TakeDamage(float dmg)
    {
        Debug.Log("Entra a TakeDamage");
        if (PlayerStatsManager.isInvulnerable) return;

        base.TakeDamage(dmg);

        // Save current HP to PlayerStatsManager
        PlayerStatsManager.HP = (int)curHp;
        UpdateHealthHud();
    }

    public override void Heal(int healPower)
    {
        base.Heal(healPower);
        // Update PlayerStatsManager
        PlayerStatsManager.HP = (int)curHp;
        UpdateHealthHud();
    }
    #endregion

    //----------- Death Management -----------  

    #region Death Management
    public override void Death()
    {
        StartCoroutine(LostMenuManager());
    }

    private IEnumerator LostMenuManager() //Setea Menú Derrota y hace un efecto de slowmotion
    {
        _lostMenu.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.2f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerStatsManager.HP = PlayerStatsManager.MaxHP;
    }
    #endregion

    //----------- UI Management -----------  

    #region UI Management

    private void UpdateHealthHud() //Update Barra de Vida, Viñeta de sangre
    {
        hpBar.fillAmount = curHp / maxHp;
        hpBarLerp.fillAmount = Mathf.Lerp(hpBarLerp.fillAmount, curHp / maxHp, lerpSpeed);
        hpVignette.alpha = 1 - (curHp / maxHp);
    }

    private void HudUpdate() //Update Stamina y llama UpdateHealthHud
    {
        staminaBar.fillAmount = stamina / maxStamina;
        UpdateHealthHud();
    }
    #endregion

    //---------- Data Management ----------
    #region Data Management
    public override void Save()
    {
        if (_loading)
            return;

        var myTransform = transform;
        _mementoState.Rec(myTransform.position, myTransform.rotation);
    }

    public override void Load()
    {
        if (_mementoState.IsRemember())
        {
            StartCoroutine(CoroutineLoad());
        }
    }


    private IEnumerator CoroutineLoad()
    {
        var waitForSeconds = new WaitForSeconds(0.01f);
        _loading = true;

        while (_mementoState.IsRemember())
        {
            var data = _mementoState.Remember();

            transform.SetPositionAndRotation((Vector3)data.parameters[0], (Quaternion)data.parameters[1]);
            yield return waitForSeconds;
        }

        _loading = false;
    }
    #endregion
}
