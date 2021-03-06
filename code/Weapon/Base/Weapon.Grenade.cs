using Sandbox;

public partial class WeaponGrenade : Weapon
{
	public override int ClipSize => -1;
	public override int AmmoMultiplier => 5;
	public override int Bucket => 3;
	public virtual float Delay => 1f;
	public virtual float PrimaryDistance => 0f;
	public virtual float SecondaryDistance => 0f;

	public override bool CanPrimaryAttack() => true;
	public override bool CanSecondaryAttack() => true;

	float Distance;
	TimeSince StartThrow;
	bool Pin;

	public override void AttackPrimary()
	{
		if ( Pin ) return;

		Distance = PrimaryDistance;

		PullPin();
	}

	public override void AttackSecondary()
	{
		if ( Pin ) return;
		
		Distance = SecondaryDistance;

		PullPin();
	}

	public virtual void PullPin()
	{
		if ( Pin ) return;

		Pin = true;
	}

	public override void Simulate( Client owner )
	{
		if ( TimeSinceDeployed < 0.6f )
			return;

		if ( Pin )
		{
			if ( !Input.Down(InputButton.PrimaryAttack) && !Input.Down( InputButton.SecondaryAttack ) )
			{
				StartThrow = 0f;
				Pin = false;

				(Owner as AnimatedEntity).SetAnimParameter( "b_attack", true );
			}
			else
			{

			}
		}
		else if ( StartThrow > 0.1 )
		{
			Throw();
		}
	}

	public virtual void Throw()
	{

	}
}
