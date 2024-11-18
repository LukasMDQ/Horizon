using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class Stats : Entity
{
    [SerializeField] private GameObject _lostMenu;
    public Image hpBar;
    public Image staminaBar;
    public CanvasGroup hpVignette;

    public float stamina, maxStamina;
    public int damage;
    [FormerlySerializedAs("ChargeRate")] public float chargeRate;

    private bool _loading;

    protected override void Start()
    {
        base.Start();
        stamina = maxStamina;

        // Load player health from PlayerStatsManager
        if (PlayerStatsManager.MaxHP >= 0 && PlayerStatsManager.jewels == 0)
        {
            // Set default values if it's the first time (e.g., first scene load)
            PlayerStatsManager.MaxHP = 100;
            PlayerStatsManager.HP = PlayerStatsManager.MaxHP;
        }

        // Assign PlayerStatsManager values to current player stats
        maxHp = PlayerStatsManager.MaxHP;
        curHp = PlayerStatsManager.HP;

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
        Debug.Log("Buff applied");
    }

    public void MaxLifeUp(int healPower)
    {
        maxHp += healPower;
        PlayerStatsManager.MaxHP = (int)maxHp; // Save the updated max health in PlayerStatsManager
        Debug.Log("Max life increased!");
    }

    public override void TakeDamage(float dmg)
    {
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
        if (curHp <= 0)
        {
            _lostMenu.SetActive(true);
            Time.timeScale = 0.2f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    #endregion

    //----------- UI Management -----------  

    #region UI Management

    private void UpdateHealthHud()
    {
        hpBar.fillAmount = curHp / maxHp;
        hpVignette.alpha = 1 - (curHp / maxHp);
    }

    private void HudUpdate()
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
