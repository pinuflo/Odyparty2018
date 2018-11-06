using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameActor : MonoBehaviour {

    public float _maxHp, _currentHp;
    public float _shield;
    public ActorType _actorType;

    void Awake()
    {
        SetActorType();
    }

    public virtual SetActorType()
    {
        _actorType = ActorType.Undefined;
    }

    public float CurrentHp
    {
        get
        {
            return this._currentHp;
        }
        set
        {
            this._currentHp = value;
        }
    }

    public void DealDamage(float amount, GameActor target, DamageSourceType damageType, DamageSourceColor damageColor)
    {
        if (onDamageDealt != null)
            onDamageDealt(amount, target, damageType, damageColor);
        target.TakeDamage( amount, this, damageType, damageColor);
    }

    public void TakeDamage(float amount, GameActor source, DamageSourceType damageType, DamageSourceColor damageColor)
    {
        if (CurrentHp - amount < 0)
        {
            if (onOverkillDamageTaken != null)
                onOverkillDamageTaken(amount- CurrentHp, source, damageType, damageColor);
            CurrentHp = 0;
        }
        else
        {
            CurrentHp = CurrentHp - amount;
        }

        if (onDamageTaken != null)
            onDamageTaken(amount, source, damageType, damageColor);
    }

    public delegate void DamageDealtDelegate(float amount, GameActor target, DamageSourceType damageType, DamageSourceColor damageColor);
    public DamageDealtDelegate onDamageDealt;

    public delegate void DamageTakenDelegate(float amount, GameActor target, DamageSourceType damageType, DamageSourceColor damageColor);
    public DamageTakenDelegate onDamageTaken;

    public delegate void OverkillDamageDelegate(float overkillAmount, GameActor target, DamageSourceType damageType, DamageSourceColor damageColor);
    public OverkillDamageDelegate onOverkillDamageTaken;

}

public enum DamageSourceType { DirectDamage, IndirectDamage, Undefined };
public enum DamageSourceColor { Red, Green, Blue, All, None };
public enum ActorType { Player, Enemy, Boss, Undefined };