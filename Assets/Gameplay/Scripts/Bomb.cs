using UnityEngine;
using System.Collections;
using System;

public class Bomb : MonoBehaviour {

    private Animator anim;
    private Vector3 initialPotition;
    public BombStatus currentStatus;
    float step = 0.5F;

    void Awake()
    {
        initialPotition = this.gameObject.transform.position;
        anim = this.GetComponent<Animator>();
        currentStatus = BombStatus.stand;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPotition, step);
    }


    public void SetPosition(Vector3 newPosition)
    {
        this.gameObject.transform.position = newPosition;
    }


    public void kill()
    {
        GameObject.Destroy(this.gameObject);
    }



    void OnEnable()
    {
        EasyTouch.On_TouchStart += On_TouchStart;

    }


    public void timeUp()
    {
        currentStatus = BombStatus.death;
        try
        {
            this.anim.SetTrigger("Destroy");
        }
        catch
        {
            Debug.LogWarning("se intento destruir bomba");
        }
        
    }

    public void On_TouchStart(Gesture gesture)
    {
        if (gesture.pickedObject == gameObject)
        {
            currentStatus = BombStatus.death;
            this.anim.SetTrigger("Explode");
            touchBomb();
        }

    }

    void OnDisable()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
        StopAllCoroutines();
    }

    void OnDestroy()
    {

        EasyTouch.On_TouchStart -= On_TouchStart;
        StopAllCoroutines();
    }

    public delegate void TouchBomb();
    public TouchBomb touchBomb;


}

public enum BombStatus { stand,  death };