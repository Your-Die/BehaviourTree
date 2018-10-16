namespace Chinchillada.BehaviourSelections.BehaviorTree
{
	public class CooldownDecorator : Decorator
	{ 
	    private readonly Timer _cooldownTimer;
	    private readonly bool _failOnCooldown;

        public CooldownDecorator(BehaviourTree tree, IBehavior child, Timer cooldownTimer, bool failOnCooldown) : base(tree, child)
	    {
	        _cooldownTimer = cooldownTimer;
	        _failOnCooldown = failOnCooldown;
	    }


        protected override void Initialize()
        {
            base.Initialize();

            if (!_cooldownTimer.IsActive) //Not on cooldown, start now.
                Child.StartBehaviour();
            else if (!_failOnCooldown) //On cooldown, wait for cooldown.
                _cooldownTimer.Finished += OnCooldownFinished;
            else // On Cooldown, fail.
            {
                Terminate(Status.Failure);
                return;
            }
            _cooldownTimer.Finished += OnCooldownFinished;

            Suspend();
        }

	    public override void Terminate()
	    { 
	        _cooldownTimer.Finished -= OnCooldownFinished;
            base.Terminate();
	    }

	    protected override void OnChildTerminated(IBehavior child, Status status)
	    {
            _cooldownTimer.Start();
            Terminate(status);
	    }

	    private void OnCooldownFinished()
	    {
	        _cooldownTimer.Finished -= OnCooldownFinished;
            Child.StartBehaviour();
        }
    }
}