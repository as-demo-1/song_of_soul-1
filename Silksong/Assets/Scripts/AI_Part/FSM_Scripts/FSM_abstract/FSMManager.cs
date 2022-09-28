using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ״̬���������Ļ���
/// </summary>
/// <typeparam name="T1"></typeparam>  
/// <typeparam name="T2"></typeparam>
public abstract class FSMManager<T1,T2> : MonoBehaviour
{

    public Animator animator;
    public AudioSource audios;
    public Rigidbody2D rigidbody2d;
    public Collision2D collision;
    public DamageableBase damageable;

    /// /// <summary>
    /// ��ǰ״̬
    /// </summary>
    protected FSMBaseState<T1,T2> currentState;
#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public string currentStateName;
    /// <summary>
    /// ����״̬
    /// </summary>
    protected FSMBaseState<T1, T2> anyState;
    public string defaultStateName;
    /// <summary>
    /// ��ǰ״̬������������״̬�б�
    /// </summary>
    public Dictionary<string, FSMBaseState<T1,T2>> statesDic = new Dictionary<string, FSMBaseState<T1,T2>>();

    public void ChangeState(string state)
    {

        if (currentState != null)
        {
            //if (currentStateName == state) return;
            currentState.ExitState(this);
            //Debug.Log(state + "  " + gameObject.name);
        }


        if (statesDic.ContainsKey(state))
        {
            currentState = statesDic[state];
            currentStateName = state;
        }
        else
        {
            Debug.LogError("����״̬������ "+state);
        }
        currentState.EnterState(this);
    }

    public virtual void InitWithScriptableObject()
    {
    }
    public virtual void InitManager()
    {
        if (GetComponent<Animator>())
        {
            animator = GetComponent<Animator>();
        }
        if (GetComponent<AudioSource>())
        {
            audios = GetComponent<AudioSource>();
        }
        if (GetComponent<Rigidbody2D>())
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }
        if(GetComponent<DamageableBase>())
        {
            damageable = GetComponent<DamageableBase>();
        }
        InitWithScriptableObject();
        ////�����ȡ
    }

    protected void Awake()
    {
        statesDic.Clear();
        InitManager();
    }

    protected virtual void Start()
    {
        if (statesDic.Count == 0)
            return;
        //Ĭ��״̬����
       currentStateName = defaultStateName;
        ChangeState(currentStateName);
        if (anyState != null)
            anyState.EnterState(this);

        //// Debug code
        //foreach (var state in statesDic.Values)
        //    foreach (var value in state.triggers)
        //    {
        //        Debug.LogWarning(this + "  " + state + "  " + value + "  " + value.GetHashCode());
        //    }

    }
    private void  FixedUpdate()
    {
        if (currentState != null)
        {
            currentState.FixAct_State(this);
            currentState.TriggerStateInFixUpdate(this);
        }
        else
            Debug.LogWarning("current State not exist");
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.FixAct_State(this);
            anyState.TriggerStateInFixUpdate(this);
        }

    }
#region ColiderEnents
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerEnter2D(this, collision);
            currentState.TriggerStateOnTriggerEnter(this, collision);
        }
        else
            Debug.LogWarning("current State not exist");
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnTriggerEnter2D(this, collision);
            anyState.TriggerStateOnTriggerEnter(this, collision);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerStay2D(this, collision);
            currentState.TriggerStateOnTriggerStay(this, collision);
        }
        else
            Debug.LogWarning("current State not exist");
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnTriggerStay2D(this, collision);
            anyState.TriggerStateOnTriggerStay(this, collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentState != null)
        {
            currentState.OnTriggerExit2D(this, collision);
            currentState.TriggerStateOnTriggerExit(this, collision);
        }
        else
            Debug.LogWarning("current State not exist");
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnTriggerExit2D(this, collision);
            anyState.TriggerStateOnTriggerExit(this, collision);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionEnter2D(this, collision);
            currentState.TriggerStateOnCollisionEnter(this, collision);
        }
        else
            Debug.LogWarning("current State not exist");
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnCollisionEnter2D(this, collision);
            anyState.TriggerStateOnCollisionEnter(this, collision);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionStay2D(this, collision);
            currentState.TriggerStateOnCollisionStay(this, collision);
        }
        else
            Debug.LogWarning("current State not exist");
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnCollisionStay2D(this, collision);
            anyState.TriggerStateOnCollisionStay(this, collision);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (currentState != null)
        {
            currentState.OnCollisionExit2D(this, collision);
            currentState.TriggerStateOnCollisionExit(this, collision);
        }
        else
            Debug.LogWarning("current State not exist");
        if (anyState != null)
        {
            // Debug.Log(anyState.triggers.Count);
            anyState.OnCollisionExit2D(this, collision);
            anyState.TriggerStateOnCollisionExit(this, collision);
        }
    }
#endregion
    protected virtual void Update()
    {

        if (currentState != null)
        {
            //ִ��״̬����
            currentState.Act_State(this);
            //���״̬�����б�
            currentState.TriggerStateInUpdate(this);
        }
        else
        {
            Debug.LogError("currentStateΪ��");
        }

        if (anyState != null)
        {
           // Debug.Log(anyState.triggers.Count);
            anyState.Act_State(this);
            anyState.TriggerStateInUpdate(this);
        }
    }
}


/// <summary>
///����Enemy״̬������������Ϊ�����SO���ù���
/// </summary>
public class EnemyFSMManager : FSMManager<EnemyStates, EnemyTriggers> 
{
    public List<Enemy_State_SO_Config> stateConfigs;
    public Enemy_State_SO_Config anyStateConfig;
    public GameObject player;
    public bool FaceLeftFirstOriginal;//ԭͼ�Ƿ�����
    public float beatBackRatio = 0;//0��ʾ��������
#if UNITY_EDITOR 
    [DisplayOnly]
#endif
    public bool hasInvokedAnimationEvent=false, isInvokingAnimationEvent=false;

    protected override void Start()
    {
        base.Start();
        SceneLinkedSMB<MonoBehaviour>.Initialise(animator,this);//use smb for animator event,such as wwise event
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Update()
    {
        if (isInvokingAnimationEvent && !hasInvokedAnimationEvent)
            invokeCurrentStateAnimationEvent();
        base.Update();
    }
        
    
    //��SO����
    public override void InitWithScriptableObject()
    {
        if(anyStateConfig!=null)
        {
            //Debug.Log("set anyStateConfig");
            anyState = ObjectClone.CloneObject(anyStateConfig.stateConfig) as FSMBaseState<EnemyStates, EnemyTriggers>;
            anyState.triggers = new List<FSMBaseTrigger<EnemyStates, EnemyTriggers>>();
            for (int k=0;k<anyStateConfig.triggerList.Count; k++)
            {
                anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger<EnemyStates, EnemyTriggers>);
                anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
            }
            anyState.InitState(this);
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<EnemyStates, EnemyTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<EnemyStates, EnemyTriggers>;
            tem.triggers = new List<FSMBaseTrigger<EnemyStates, EnemyTriggers>>();
            for (int k=0;k< stateConfigs[i].triggerList.Count;k++)
            {
                tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger<EnemyStates, EnemyTriggers>);
                tem.triggers[tem.triggers.Count-1].InitTrigger(this);
                //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
            }
            statesDic.Add(stateConfigs[i].StateName, tem);
            tem.InitState(this);
        }
    }


    public void faceLeft()//ʹ��������
    {
        int x = FaceLeftFirstOriginal ? 1 : -1;
        Vector3 tem = transform.localScale;
        transform.localScale = new Vector3(x * Mathf.Abs(tem.x), tem.y, tem.z);
    }
    public void faceRight()
    {
        int x = FaceLeftFirstOriginal ? 1 : -1;
        Vector3 tem = transform.localScale;
        transform.localScale = new Vector3(x * -Mathf.Abs(tem.x), tem.y, tem.z);
    }

    public void turnFace()
    {
        //Debug.Log("turn face");
        Vector3 tem = transform.localScale;
        transform.localScale = new Vector3(-tem.x, tem.y, tem.z);
    }

    public bool currentFacingLeft()
    {
        return FaceLeftFirstOriginal ? transform.localScale.x > 0 : transform.localScale.x < 0;
    }

    /// <summary>
    /// ���ݸ����ٶȸı�������
    /// </summary>
    public void faceWithSpeed()
    {
        if (rigidbody2d.velocity.x < 0)
            faceLeft();
        else faceRight();
    }

    /// <summary>
    ///���ָ�����λ�õ�vector2(��normalized) ��ѡͬʱ�ı���ﳯ��
    /// </summary>
    public Vector2 getTargetDir(bool changeFace=false)
    {
        if (player == null) return new Vector2(int.MaxValue,int.MaxValue);
        Vector2 dir = player.transform.position - transform.position;
        if(changeFace)
        {
            if (dir.x > 0)
            {
                //Debug.Log("dir right");
                faceRight();
            }

            else
            {
                //Debug.Log("dir left");
                faceLeft();
            }
        }
        return dir;
    }

    public bool nearPlatformBoundary(Vector2 checkDir)
    {
        Collider2D collider = GetComponent<Collider2D>();
        float rayToGroundDistance = collider.bounds.extents.y - collider.offset.y;
        rayToGroundDistance += 0.5f;

        Vector3 frontPoint = transform.position + new Vector3((checkDir.x > 0 ? 1 : -1), 0, 0) * (GetComponent<Collider2D>().bounds.size.x * 0.5f);
        var rayHit = Physics2D.Raycast(frontPoint, Vector2.down,100, 1 << LayerMask.NameToLayer("Ground"));
      /* Debug.DrawRay(frontPoint,Vector3.down);
        Debug.Log(rayHit.distance);*/
        if (rayHit.distance > rayToGroundDistance || rayHit.distance==0)//�����Ե
        {
            return true;
        }
        return false;

    }

    public bool hitWall()
    {

        Vector3 frontPoint = transform.position + new Vector3((rigidbody2d.velocity.x > 0 ? 1 : -1), 0, 0) * (GetComponent<Collider2D>().bounds.size.x * 0.5f);
        Vector3 upPoint = transform.position + new Vector3(0, 0.1f, 0);
       // Debug.DrawLine(frontPoint,upPoint);
        if (Physics2D.OverlapArea(upPoint, frontPoint, 1 << LayerMask.NameToLayer("Ground")) != null)
        {
            return true;
        }
        return false;
    }


    public void invokeCurrentStateAnimationEvent()
    {
        Debug.Log("Shoot Invoke");
        EnemyFSMBaseState tem = currentState as EnemyFSMBaseState;
        tem.invokeAnimationEvent();
        hasInvokedAnimationEvent = true;
        
    }
}










/// <summary>
///����NPC״̬������������Ϊ�����SO���ù���
/// </summary>
public class NPCFSMManager : FSMManager<NPCStates, NPCTriggers>
{
    public List<NPC_State_SO_Config> stateConfigs;
    public Enemy_State_SO_Config anyStateConfig;
    public override void InitWithScriptableObject()
    {
        if (anyStateConfig != null)
        {
            anyState = (FSMBaseState<NPCStates, NPCTriggers>)ObjectClone.CloneObject(anyStateConfig.stateConfig);
            anyState.triggers = new List<FSMBaseTrigger<NPCStates, NPCTriggers>>();
            for (int k = 0; k < anyStateConfig.triggerList.Count; k++)
            {
                anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger<NPCStates, NPCTriggers>);
                anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
            }
            anyState.InitState(this);
        }
        for (int i = 0; i < stateConfigs.Count; i++)
        {
            FSMBaseState<NPCStates, NPCTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<NPCStates, NPCTriggers>;
            tem.triggers = new List<FSMBaseTrigger<NPCStates, NPCTriggers>>();
            for (int k = 0; k < stateConfigs[i].triggerList.Count; k++)
            {
                tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger<NPCStates, NPCTriggers>);
                tem.triggers[tem.triggers.Count - 1].InitTrigger(this);
                //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
            }
            statesDic.Add(stateConfigs[i].name, tem);
            tem.InitState(this);
        }
    }
}
/// <summary>
/// ����Player״̬����������Ĭ��û�����SO���ù��ܣ�
/// ����Ҫ��
/// ����ȡ���������ע��
/// Ȼ���Player_State_SO_Config�ű���ȡ������Player_State_SO_Config���ע�ͼ��ɡ�
/// 
/// </summary>
public class PlayerFSMManager : FSMManager<PlayerStates, PlayerTriggers> 
{
    //public List<Player_State_SO_Config> stateConfigs;
    //public Player_State_SO_Config anyStateConfig;
    //public override void InitWithScriptableObject()
    //{
    //    if (anyStateConfig != null)
    //    {
    //        anyState = (FSMBaseState<PlayerStates, PlayerTriggers>)ObjectClone.CloneObject(anyStateConfig.stateConfig);
    //        anyState.triggers = new List<FSMBaseTrigger<PlayerStates, PlayerTriggers>>();
    //        for (int k = 0; k < anyStateConfig.triggerList.Count; k++)
    //        {
    //            anyState.triggers.Add(ObjectClone.CloneObject(anyStateConfig.triggerList[k]) as FSMBaseTrigger<PlayerStates, PlayerTriggers>);
    //            anyState.triggers[anyState.triggers.Count - 1].InitTrigger(this);
    //            //Debug.Log(this.gameObject.name+"  "+ anyState.triggers[anyState.triggers.Count - 1]+"  "+anyState.triggers[anyState.triggers.Count - 1].GetHashCode());
    //        }
    //        anyState.InitState(this);
    //    }
    //    for (int i = 0; i < stateConfigs.Count; i++)
    //    {
    //        FSMBaseState<PlayerStates, PlayerTriggers> tem = ObjectClone.CloneObject(stateConfigs[i].stateConfig) as FSMBaseState<PlayerStates, PlayerTriggers>;
    //        tem.triggers = new List<FSMBaseTrigger<PlayerStates, PlayerTriggers>>();
    //        for (int k = 0; k < stateConfigs[i].triggerList.Count; k++)
    //        {
    //            tem.triggers.Add(ObjectClone.CloneObject(stateConfigs[i].triggerList[k]) as FSMBaseTrigger<PlayerStates, PlayerTriggers>);
    //            tem.triggers[tem.triggers.Count - 1].InitTrigger(this);
    //            //Debug.Log(this.gameObject.name + "  " + tem.triggers[tem.triggers.Count - 1] + "  " + tem.triggers[tem.triggers.Count - 1].GetHashCode());
    //        }
    //        statesDic.Add(stateConfigs[i].stateType, tem);
    //        tem.InitState(this);
    //    }
    //}
}

