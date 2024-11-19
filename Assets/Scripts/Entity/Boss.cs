using FiniteStateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
public class Boss : Entity
{
    public BossStateMachine bossStateMachine;
    public Image hpBar;
    public Animator grateAnimator;
    public TextMeshProUGUI bossHPText;
    
    private static readonly int GrateOff = Animator.StringToHash("GrateOFF");

    private void Update()
    {
        UpdateHealthHud();
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

    private void UpdateHealthHud()
    {
        hpBar.fillAmount = curHp / maxHp;
        bossHPText.text = $"{curHp}|{maxHp}";
    }

    // ReSharper disable once UnusedMember.Global
    public void StartGrateAnim() // This should be called from an animation event
    {
        grateAnimator.SetTrigger(GrateOff);
    }
}