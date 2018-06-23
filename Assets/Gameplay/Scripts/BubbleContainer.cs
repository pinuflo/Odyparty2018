using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class BubbleContainer : MonoBehaviour {

    public List<Bubble> bubbles;
    public List<Bomb> bombs;
    [HideInInspector] public int bubbleAmount;
    private int currentTouches;
    
    private int[] touches, maxTouches, colorStatus;
    public GroupColor currentColor;
    private GameObject[] spawners;
    public bool giveBonus = true, expired=false;
    private int maxColors = OdyConstants.MaxColors;
    public int currentHp = OdyConstants.MaxHP;


    public void selfExpire()
    {
        expired = true;
        foreach (Bubble bubble in bubbles)
        {
            try
            {
                Vector3 pos = bubble.gameObject.transform.position;
                if(bubble.currentStatus != Bubble.BubbleStatus.death)
                {
                    GameObject obj = Instantiate(Resources.Load<GameObject>("explotion"), pos, Quaternion.identity) as GameObject;
                }
                    
                bubble.kill();
            }
            catch
            {

            } 
        }
        foreach (Bomb bomb in bombs)
        {
                if(bomb.currentStatus != BombStatus.death)
                {
                    bomb.timeUp();
                }

        }
    }

    IEnumerator hpDecrease()
    {
        do
        {
            if (currentHp < 4)
                giveBonus = false;
                
            yield return new WaitForSeconds(0.5F);

            currentHp = currentHp - 1;

        } while (currentHp > 0);


        expired = true;
        runBubbleActions();
    

    }


    private void resetInnerObjects()
    {
        bubbles = new List<Bubble>();
        touches = new int[maxColors];
        maxTouches = new int[maxColors];
        colorStatus = new int[maxColors];
        spawners = new GameObject[maxColors];
        spawners[0] = GameObject.Find("Spawner1");
        spawners[1] = GameObject.Find("Spawner2");
        spawners[2] = GameObject.Find("Spawner3");
        spawners[3] = GameObject.Find("Spawner4");
        spawners[4] = GameObject.Find("Spawner5");
        spawners[5] = GameObject.Find("Spawner6");

        for (int i = 0; i < maxColors; i++)
        {
            touches[i] = 0;
            colorStatus[i] = 0;
        }

        foreach (Bubble bubble in FindSceneObjectsOfType(typeof(Bubble)))
        {
            if (bubble.currentStatus != Bubble.BubbleStatus.death && bubble != null)
                bubbles.Add(bubble);
        }

        foreach (Bomb bomb in FindSceneObjectsOfType(typeof(Bomb)))
        {
            if (bomb.currentStatus != BombStatus.death && bomb != null)
                bombs.Add(bomb);
        }

    }

    void Awake()
    {

        resetInnerObjects();

        int currentBubbleCount = 0;
        foreach ( Bubble auxBubble in bubbles)
        {
           
            if (auxBubble.marked == false)
            {
                auxBubble.marked = true;
                currentBubbleCount = currentBubbleCount + 1;
                maxTouches[(int)auxBubble.bubbleColor] = maxTouches[(int)auxBubble.bubbleColor] + 1;
                colorStatus[(int)auxBubble.bubbleColor] = 1;
                runBubbleActions();
            }
        }


    }



	void Start () {
        currentHp = 5;
        setInitialPosition();
        currentColor = GroupColor.none;
        StartCoroutine(hpDecrease());
    }


    void setInitialPosition()
    {
        foreach (Bubble bubble in bubbles)
        {
            int pos = UnityEngine.Random.Range(0, 5);
            bubble.SetPosition(spawners[pos].transform.position);
        }
        foreach (Bomb bomb in bombs)
        {
            int pos = UnityEngine.Random.Range(0, 5);
            bomb.SetPosition(spawners[pos].transform.position);
        }

    }



    void OnEnable()
    {
        foreach (Bubble bubble in bubbles)
        {
            bubble.TouchInnerBubble += TouchInnerBubble;
            bubble.UntouchInnerBubble += UntouchInnerBubble;
        }

        foreach (Bomb bomb in bombs)
        {
            bomb.touchBomb += touchBomb;
        }
    }

    private void touchBomb()
    {
        selfExpire();
		GameplayManager.instance.removeRemainingTime ();
    }

    void OnDisable()
    {
        foreach (Bubble bubble in bubbles)
        {
            bubble.TouchInnerBubble -= TouchInnerBubble;
            bubble.UntouchInnerBubble -= UntouchInnerBubble;
        }

        foreach (Bomb bomb in bombs)
        {
            bomb.touchBomb -= touchBomb;
        }
    }

    void OnDestroy()
    {
        foreach (Bubble bubble in bubbles)
        {
            bubble.TouchInnerBubble -= TouchInnerBubble;
            bubble.UntouchInnerBubble -= UntouchInnerBubble;
        }

        foreach (Bomb bomb in bombs)
        {
            bomb.touchBomb -= touchBomb;
        }
    }





    private void TouchInnerBubble(GroupColor bubbleColor)
    {
        currentTouches = currentTouches + 1;
        
        if (currentColor == GroupColor.none)
        {

            touches[(int)bubbleColor] = touches[(int)bubbleColor] + 1;
            currentColor = bubbleColor;
            runBubbleActions();
            
        }
        else
        {
            if(this.currentColor == bubbleColor)
            {
                touches[(int)bubbleColor] = touches[(int)bubbleColor] + 1;
                runBubbleActions();
            }
        }

    }

    private void UntouchInnerBubble(GroupColor bubbleColor)
    {
        if(currentTouches > 0)
        {
            currentTouches = currentTouches - 1;
            touches[(int)bubbleColor] = touches[(int)bubbleColor] - 1;
        }


        if (touches[(int)bubbleColor] <= 0)
        {
           currentColor = GroupColor.none;
            touches[(int)bubbleColor] = 0;
        }
    }




    void starExplotion(Vector3 aux)
    {

        GameObject obj = Instantiate(Resources.Load<GameObject>("StarExplotion"), aux, Quaternion.identity) as GameObject;
        GameObject splat = Instantiate(Resources.Load<GameObject>("mancha"), aux, Quaternion.identity) as GameObject;
        
    }




    public void runBubbleActions()
    {
        GroupColor cColor = this.currentColor;

        if (expired == true)
        {
            foreach (Bubble bubble in bubbles)
            {
                bubble.kill();
            }

            foreach (Bomb bomb in bombs)
            {
                    if (bomb.currentStatus != BombStatus.death)
                    {
                        bomb.timeUp();
                    }
            }


            if (currentHp <= 0)
            {
                try
                {
                    RoundEnd(this);
                }
                catch
                {
                    //no se asigno delegado
                }
            }



            return;
        }

        if (cColor != GroupColor.none  )
        {
            if (touches[(int)cColor] == maxTouches[(int)cColor])
            {
                foreach (Bubble bubble in bubbles)
                {

                    if (bubble.bubbleColor == cColor)
                    {
                        Vector3 position = bubble.gameObject.transform.position;
                        starExplotion(position);
                        bubble.kill();
                    }

                }
                colorStatus[(int)currentColor] = 0;
                this.currentColor = GroupColor.none;
            }

            if (checkGeneralStatus() == true && this != null)
            {
                RoundEnd(this);
            }

        }



    }



    /// <summary>
    /// Devuelve verdadero si fueron activadas todas las burbujas
    /// </summary>
    /// <returns></returns>
    public bool checkGeneralStatus()
    {
        int aux = 0;
        for (int i = 0; i < maxColors; i++)
        {
            
            aux = aux + colorStatus[i];
        }

        //Debug.Log("aux:" + " =" + aux.ToString());

        if (aux > 0 )
            return false;
        else
            return true;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            selfExpire();
            Debug.Log("auto expire");
        }


    }


    public delegate void RoundEndDelegate(BubbleContainer self);
    public RoundEndDelegate RoundEnd;



}
