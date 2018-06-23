using UnityEngine;
using System.Collections;


public class Bubble : MonoBehaviour {

    public int touchs = 0;
    public enum BubbleStatus { stand, touched, death };
    public BubbleStatus currentStatus;
    public int finger = -1;
    private Animator anim;
    public bool canBeTouched = true, marked=false;
    private Vector3 initialPotition;
    float step = 1F;
    public GroupColor bubbleColor;
    public SpriteRenderer sRenderer, starSRenderer;

    void Awake()
    {
        initialPotition = this.gameObject.transform.position;
        anim = this.GetComponent<Animator>();
        step = 30F * Time.deltaTime;
    }

	void Start ()
    {
        currentStatus = BubbleStatus.stand;
        updateAnimation();
        setColor(bubbleColor);
	}

    public void SetPosition(Vector3 newPosition)
    {
        this.gameObject.transform.position = newPosition;
        updateAnimation();
    }

    public void setColor(GroupColor color)
    {
        this.bubbleColor = color;
        
        switch (color)
        {
            case(GroupColor.blue):
            {
                    sRenderer.sprite = (Sprite) Resources.Load<Sprite>("BlueBubble") as Sprite;
                    starSRenderer.sprite = (Sprite)Resources.Load<Sprite>("starBlue") as Sprite;
                    break;
            }
            case (GroupColor.black):
            {
                    sRenderer.sprite = (Sprite)Resources.Load<Sprite>("BlackBubble") as Sprite;
                    starSRenderer.sprite = (Sprite)Resources.Load<Sprite>("starBlack") as Sprite;
                    break;
            }
            case (GroupColor.green):
            {
                    sRenderer.sprite = (Sprite)Resources.Load<Sprite>("GreenBubble") as Sprite;
                    starSRenderer.sprite = (Sprite)Resources.Load<Sprite>("starGreen") as Sprite;
                    break;
            }
            case (GroupColor.red):
            {
                    sRenderer.sprite = (Sprite)Resources.Load<Sprite>("RedBubble") as Sprite;
                    starSRenderer.sprite = (Sprite)Resources.Load<Sprite>("starRed") as Sprite;
                    break;
            }
            case (GroupColor.yellow):
            {
                    sRenderer.sprite = (Sprite)Resources.Load<Sprite>("YellowBubble") as Sprite;
                    starSRenderer.sprite = (Sprite)Resources.Load<Sprite>("starYellow") as Sprite;
                    break;
            }
            case (GroupColor.purple):
            {
                    sRenderer.sprite = (Sprite)Resources.Load<Sprite>("PurpleBubble") as Sprite;
                    starSRenderer.sprite = (Sprite)Resources.Load<Sprite>("starPurple") as Sprite;
                    break;
            }
        }

        starSRenderer.color = new Color(255, 255, 255);
        


    }
	

    void OnEnable()
    {
        EasyTouch.On_TouchStart += On_TouchStart;
        EasyTouch.On_TouchDown += On_TouchDown;
        EasyTouch.On_TouchUp += On_TouchUp;
    }



    void OnDisable()
    {
        EasyTouch.On_TouchDown -= On_TouchDown;
        EasyTouch.On_TouchUp -= On_TouchUp;
        EasyTouch.On_TouchStart -= On_TouchStart;
        StopAllCoroutines();
    }

    void OnDestroy()
    {
        EasyTouch.On_TouchDown -= On_TouchDown;
        EasyTouch.On_TouchUp -= On_TouchUp;
        EasyTouch.On_TouchStart -= On_TouchStart;

        StopAllCoroutines();
    }

    void On_TouchStart(Gesture gesture)
    {
        if (gesture.pickedObject == gameObject)
        {
            touchs = 1;
            statusUpdater();
            finger = gesture.fingerIndex;
        }   
    }

    public void On_TouchDown(Gesture gesture)
    {

       if (gesture.fingerIndex == finger && gesture.pickedObject != gameObject)
       {
                touchs = 0;
                statusUpdater();
                finger = -1;
       }
        
    }

    public void On_TouchUp(Gesture gesture)
    {

        if (gesture.fingerIndex == finger)
        {
            touchs = 0;
            statusUpdater();
            finger = -1;
        }

    }


    void statusUpdater()
    {
        updateAnimation();
        if ( touchs == 1 )
        {
            if (currentStatus != BubbleStatus.death)
                currentStatus = BubbleStatus.touched;

            if(canBeTouched == true)
            {
                TouchInnerBubble(this.bubbleColor);
            }
             
        }
        else
        {
            if (currentStatus != BubbleStatus.death)
                currentStatus = BubbleStatus.stand;
            UntouchInnerBubble(this.bubbleColor);
        }
        
    }

    void updateAnimation()
    {
        try
        {
            switch (currentStatus)
            {
                case BubbleStatus.stand:
                    {
                        anim.SetInteger("Status", 0);
                        break;
                    }
                case BubbleStatus.touched:
                    {
                        anim.SetInteger("Status", 1);

                        break;
                    }
                case BubbleStatus.death:
                    {
                        anim.SetInteger("Status", 2);
                       

                        break;
                    }
            }
        }
        catch
        {

        }

    }

    public delegate void TouchInnerBubbleDelegate(GroupColor color);
    public TouchInnerBubbleDelegate TouchInnerBubble;

    public delegate void UntouchInnerBubbleDelegate(GroupColor color);
    public UntouchInnerBubbleDelegate UntouchInnerBubble;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, initialPotition, step);
    }

    public void kill()
    {

        canBeTouched = false;
        if (anim!=null)
            anim.SetInteger("Status", 1);
        try
        {
            currentStatus = BubbleStatus.death;
            updateAnimation();
            StartCoroutine(killIenumerator());
        }
        catch
        {

        }

    }

    IEnumerator killIenumerator()
    {

        if (anim != null)
            anim.SetInteger("Status", 2);
        yield return new WaitForSeconds(0.8F);
        GameObject.Destroy(this.gameObject);
    }

}
