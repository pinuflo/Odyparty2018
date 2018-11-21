using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEffect : MonoBehaviour
{
    
    public abstract void InitEffect();
    public abstract void KillEffect();

    public abstract void RunEffect();
}
