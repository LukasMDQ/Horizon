namespace FiniteStateMachine
{
    public class SkeletonMeleeStateMachine : SkeletonStateMachine
    {
        protected override void Awake()
        {
            skeletonAttackType = SkeletonAttackType.Melee;
            base.Awake();
        }
    }
}