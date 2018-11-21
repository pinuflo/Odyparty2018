using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHero : GameActor
{
    protected override void SetActorType()
    {
        _actorType = ActorType.Player;
    }


}
