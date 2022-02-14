using System.Collections.Generic;
using UnityEngine;
//Created by Unity from:
//https://assetstore.unity.com/packages/templates/tutorials/2d-game-kit-107098#description

public class PlayerInput : InputComponent
{
    public static PlayerInput Instance
    {
        get { return s_Instance; }
    }

    protected static PlayerInput s_Instance;


    public bool HaveControl { get { return m_HaveControl; } }
    public bool IsFrozen { get; set; }

    private List<Button> buttons = new List<Button>();

    public InputButton sprint = new InputButton(KeyCode.LeftShift, XboxControllerButtons.LeftBumper);
    public InputButton Pick = new InputButton(KeyCode.F, XboxControllerButtons.Y);
    ////TODO:xbox button mapping
    public InputButton teleport = new InputButton(KeyCode.X, XboxControllerButtons.None);
    public InputButton jump = new InputButton(KeyCode.K, XboxControllerButtons.A);
    public InputButton interact = new InputButton(KeyCode.W, XboxControllerButtons.None);
    public InputButton breakMoon = new InputButton(KeyCode.Q, XboxControllerButtons.None);
    public InputButton heal = new InputButton(KeyCode.C, XboxControllerButtons.None);
    public InputButton castSkill = new InputButton(KeyCode.C, XboxControllerButtons.None);
    public InputAxis horizontal = new InputAxis(KeyCode.D, KeyCode.A, XboxControllerAxes.LeftstickHorizontal);
    public InputAxis vertical = new InputAxis(KeyCode.W, KeyCode.S, XboxControllerAxes.LeftstickVertical);
    public InputButton normalAttack = new InputButton(KeyCode.J, XboxControllerButtons.X);
    [HideInInspector]

    protected bool m_HaveControl = true;

    protected bool m_DebugMenuIsOpen = false;

    void Awake()
    {
        if (s_Instance == null)
            s_Instance = this;
        else
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");

        buttons.AddRange(new List<Button>
        {
            horizontal,
            vertical,
            jump,
            interact,
            normalAttack,
            sprint,
            teleport,
            Pick,
            breakMoon,
            heal,
            castSkill,

        });
    }

    void OnEnable()
    {
        if (s_Instance == null)
            s_Instance = this;
        else if (s_Instance != this)
            throw new UnityException("There cannot be more than one PlayerInput script.  The instances are " + s_Instance.name + " and " + name + ".");


    }

    void OnDisable()
    {

        s_Instance = null;
    }

    protected override void GetInputs(bool fixedUpdateHappened)
    {
        if (IsFrozen)//Ӧʹ��ReleaseControls
        {
            return;
        }
        foreach (var button in buttons)
        {
            button.Get(fixedUpdateHappened, inputType);
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            m_DebugMenuIsOpen = !m_DebugMenuIsOpen;
        }
    }

    public override void GainControls()
    {
       // Debug.Log("gainCtrl");
        m_HaveControl = true;

        foreach (var button in buttons)
        {
            button.Enable();
        }
    }

    public override void ReleaseControls(bool resetValues = true)
    {
      //  Debug.Log("releaseCtrl");
        m_HaveControl = false;

        foreach (var button in buttons)
        {
            button.Disable();
        }
    }

}


