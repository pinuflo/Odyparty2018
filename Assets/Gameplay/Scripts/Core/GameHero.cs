using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHero : GameActor
{
    public override void SetActorType()
    {
        _actorType = ActorType.Player;

        DamageActor actor = new DamageActor();
        

    }


}
