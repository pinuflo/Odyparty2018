using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActorFlags {

    private Actorinvulnerability _actorInvulnerability;
    private ActorTargetability _actorTargetability;

    ///<summary>
    ///Defines if an actor can be targeted by player inputs
    ///</summary>
    public ActorTargetability Targeteability
    {
        get
        {
            return this._actorTargetability;
        }
        set
        {
            this._actorTargetability = value;
        }
    }

    ///<summary>
    ///Defines if an actor can recieve damage from effect sources
    ///</summary>
    public Actorinvulnerability Invulnerability
    {
        get
        {
            return this._actorInvulnerability;
        }
        set
        {
            this._actorInvulnerability = value;
        }
    }


    public GameActorFlags()
    {
        _actorInvulnerability = Actorinvulnerability.Vulnerable;
        _actorTargetability = ActorTargetability.Targeteable;
    }


}

public enum Actorinvulnerability { Vulnerable, Invulnerable };
public enum ActorTargetability { Targeteable, Untargetable };