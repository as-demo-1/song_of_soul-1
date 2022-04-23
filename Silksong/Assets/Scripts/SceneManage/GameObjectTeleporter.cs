using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
/// <summary>
/// 负责游戏物体在场景内的传送 单例
/// </summary>
public class GameObjectTeleporter : MonoBehaviour
{
    public static GameObjectTeleporter Instance
    {
        get
        {
           // Debug.Log("get");
            if (instance != null)
                return instance;

            instance = FindObjectOfType<GameObjectTeleporter>();

            if (instance != null)
                return instance;

            GameObject gameObjectTeleporter = new GameObject("GameObjectTeleporter");
            instance = gameObjectTeleporter.AddComponent<GameObjectTeleporter>();

            return instance;
        }
    }

    protected static GameObjectTeleporter instance;

    protected PlayerInput playerInput;//当场景内有玩家角色时 此组件一定在玩家角色上
    public Vector3 playerRebornPoint;//玩家最新的重生点
    public bool Transitioning;

    public CinemachineVirtualCamera virtualCamera;
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        playerInput = FindObjectOfType<PlayerInput>();
    }

    public static void playerReborn()
    {
        //Instance.playerInput.transform.localScale = new Vector3(1, 0, 0);
        Teleport(Instance.playerInput.gameObject,Instance.playerRebornPoint);
    }
    public  void playerEnterScene(SceneEntrance.EntranceTag entranceTag)//在玩家进入新场景时调用该方法
    {
        SceneEntrance entrance = SceneEntrance.GetDestination(entranceTag);
        if(entrance==null)//该场景没有入口 不是有玩家的游戏场景 
        {
            return;
        }
        if (playerInput == null)
            playerInput = FindObjectOfType<PlayerInput>();//

        //playerInput.transform.localScale = new Vector3();角色朝向 暂未考虑
        playerRebornPoint = entrance.transform.position;

        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if(virtualCamera)
        virtualCamera.Follow = playerInput.transform;

        GameManager.Instance.audioManager.setMonstersDefaultHittedAudio();

        Teleport(playerInput.gameObject, entrance.transform.position);

        //show scene name for test,will be delete
        PlayerHpUI.setSceneNameText(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public static void Teleport(GameObject transitioningGameObject, Vector3 destinationPosition)
    {
        Instance.StartCoroutine(Instance.Transition(transitioningGameObject, false, false, destinationPosition, false));
    }

    protected IEnumerator Transition(GameObject transitioningGameObject, bool releaseControl, bool resetInputValues, Vector3 destinationPosition, bool fade)
    {
        Transitioning = true;

        if(playerInput==null)
        playerInput = FindObjectOfType<PlayerInput>();

        if (releaseControl)
        {
            PlayerAnimatorParamsMapping.SetControl(false);
        }

        /*  if (fade)
              yield return StartCoroutine(ScreenFader.FadeSceneOut());*///场景过渡加载暂不考虑 暂用yield return null代替防止没有返回值
        transitioningGameObject.transform.position = destinationPosition;
        yield return null;

   

       /* if (fade)
            yield return StartCoroutine(ScreenFader.FadeSceneIn());*/

        if (releaseControl)
        {
            PlayerAnimatorParamsMapping.SetControl(true);
        }

        Transitioning = false;
    }

}
