using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameActor : MonoBehaviour {

    private float _maxHp, _currentHp;
    private float _shield;
    protected ActorType _actorType;

    public virtual void SetActorType()
    {
        this._actorType = ActorType.Undefined;
    }

    public float CurrentHp
    {
        get
        {
            return this._currentHp;
        }
    }

    public float MaxHp
    {
        get
        {
            return this._maxHp;
        }
    }

    public ActorType Type
    {
        get
        {
            return this._actorType;
        }
    }

    void Awake()
    {
        SetActorType();
    }

    public void DealDamage(DamageActor damageActor)
    {
        if (onDamageDealt != null)
            onDamageDealt(damageActor);
        damageActor.Target.TakeDamage(damageActor);
    }

    public void TakeDamage(DamageActor damageActor)
    {
        if (CurrentHp - damageActor.DamageAmount < 0)
        {
            if (onOverkillDamageTaken != null)
                onOverkillDamageTaken(damageActor.DamageAmount - CurrentHp, damageActor);
            this._currentHp = 0;
        }
        else
        {
            this._currentHp = this._currentHp - damageActor.DamageAmount;
        }

        if (onDamageTaken != null)
            onDamageTaken(damageActor);
    }

    public delegate void DamageDealtDelegate(DamageActor damageActor);
    public DamageDealtDelegate onDamageDealt;

    public delegate void DamageTakenDelegate(DamageActor damageActor);
    public DamageTakenDelegate onDamageTaken;

    public delegate void OverkillDamageDelegate(float overkillAmount, DamageActor damageActor);
    public OverkillDamageDelegate onOverkillDamageTaken;

}


public enum ActorType { Player, Enemy, Boss, Undefined };


//FLAGS
public enum Actorinvulnerability { Vulnerable, Invulnerable };
public enum ActorTargetability   { Targeteable, Untargetable };
