using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : Entity
{
    [SerializeField] private TextMeshProUGUI textBullet; // TODO delete this later
    [SerializeField] private GameObject _lostMenu;
    public Image hpBar;
    public Image staminaBar;
    public CanvasGroup hpVignette;

    public int jewels;
    public float stamina, maxStamina;
    public int damage;
    public float ChargeRate;

    protected override void MyStart()
    {
        stamina = maxStamina;

        // Load player health from PlayerStatsManager
        if (PlayerStatsManager.MaxHP == 0)
        {
            // Set default values if it's the first time (e.g., first scene load)
            PlayerStatsManager.MaxHP = 100;
            PlayerStatsManager.HP = PlayerStatsManager.MaxHP;
        }

        // Assign PlayerStatsManager values to current player stats
        maxHp = PlayerStatsManager.MaxHP;
        curHp = PlayerStatsManager.HP;

        UpdateHealthUI();
    }

    private void Update()
    {
        UpdateStamina();
        UIUpdate();
    }

    //----------- Stamina Management -----------  
    private void UpdateStamina()
    {
        if (stamina > maxStamina) stamina = maxStamina;
        if (stamina < 0) stamina = 0;
    }

    //----------- Player-Specific Actions -----------  
    public void AddJewel(int jewelCount)
    {
        jewels += jewelCount;
        Debug.Log("Jewel added to grial");
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
        UpdateHealthUI();
    }

    public void Heal(int healPower)
    {
        curHp += healPower;
        if (curHp > maxHp) curHp = maxHp;

        // Update PlayerStatsManager
        PlayerStatsManager.HP = (int)curHp;
        UpdateHealthUI();
    }

    //----------- Death Management -----------  
    public override void Death()
    {
        if (curHp <= 0)
        {
            _lostMenu.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //----------- UI Management -----------  
    private void UpdateHealthUI()
    {
        hpBar.fillAmount = curHp / maxHp;
        hpVignette.alpha = 1 - (curHp / maxHp);
    }

    private void UIUpdate()
    {
        staminaBar.fillAmount = stamina / maxStamina;
        UpdateHealthUI();
    }
}
