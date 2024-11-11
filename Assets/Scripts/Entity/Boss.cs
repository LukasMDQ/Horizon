using FiniteStateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Entity
{
    public BossStateMachine bossStateMachine;
    public Image hpBar;
    public Animator grateAnimator;
    public TextMeshProUGUI bossHPText;

    private void Update()
    {
        UpdateHealthUI();
    }
    protected override void MyStart()
    {

    }

    public override void Death()
    {
        if (_sounds.Length > 0) _spawnSound.PlayOneShot(_sounds[1]); // 1 = deathSound
        if(_destroyEffect != null && _drops != null)
        {
            var myTransform = transform;
            Instantiate(_destroyEffect, myTransform.position, myTransform.rotation);
        }
        bossStateMachine.SetBossAnimations(BossStateMachine.BossAnimationsType.Death);
    }

    private void UpdateHealthUI()
    {
        hpBar.fillAmount = curHp / maxHp;
        bossHPText.text = $"{curHp*2}|{maxHp*2}";
    }

    public void startGrateAnim()
    {
        grateAnimator.SetTrigger("GrateOFF");
    }
}