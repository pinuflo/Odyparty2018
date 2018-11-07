using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageActor
{
    DamageSourceType _damageSourceType;
    DamageSourceColor _damageSourceColor;
    GameActor _target;
    GameActor _source;
    float _damageAmount;

    public GameActor Source
    {
        get
        {
            return this._source;
        }
    }

    public GameActor Target
    {
        get
        {
            return this._target;
        }
    }

    public float DamageAmount
    {
        get
        {
            return this._damageAmount;
        }
    }

    public DamageSourceType SourceType
    {
        get
        {
            return this._damageSourceType;
        }
    }

    public DamageSourceColor SourceColor
    {
        get
        {
            return this._damageSourceColor;
        }
    }


    public DamageActor()
    {
        _damageSourceType = DamageSourceType.Undefined;
        _damageSourceColor = DamageSourceColor.None;
        _target = null;
        _source = null;
        _damageAmount = 0;
    }
    public DamageActor(GameActor target, GameActor source, float damageAmount, DamageSourceType damageType, DamageSourceColor damageColor )
    {
        _damageSourceType = damageType;
        _damageSourceColor = damageColor;
        _target = target;
        _source = source;
        _damageAmount = damageAmount;
    }

}

public enum DamageSourceType { DirectDamage, IndirectDamage, Undefined };
public enum DamageSourceColor { Red, Green, Blue, All, None };