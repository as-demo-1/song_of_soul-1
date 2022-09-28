using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //���˵�
    public Button[] mainButtons;
    //�����˵����
    public List<GameObject> SecondPanel;
    //��ͼ
    public Map map;
    //װ��
    public Equip equip;
    //����
    public Talisman talisman;
    //ͼ��
    public Picture picture;
    //�ɾ�
    public Achievement achievement;
    //ѡ��
    public Options options;

    //��ǰѡ�����˵�����
    private int mainButtonIndex = 0;
    //��ǰѡ���Ӳ˵�����
    private int SecondButtonIndex = 0;
    //
    private int ThirdButtonIndex = 0;

    //�Ƿ�Ϊһ���˵�
    private bool childPanelExpanded = true;

    //�Ƿ�Ϊ�����˵�
    private bool isSecond = false;

    //�Ƿ�Ϊ�����˵�
    private bool isThird = false;

    public Dictionary<int, List<Button>> dicMenu;

    Dictionary<int, List<Button>> dicMenuEquip;

    Dictionary<int, List<Button>> dicMenuPic;

    public List<List<Button>> menuSecondBnt;

    void Start()
    {
        ShowChildPanel();

        dicMenu = new Dictionary<int, List<Button>>();
        dicMenu.Add(0, map.btns);
        dicMenu.Add(1, equip.btns);
        dicMenu.Add(2, talisman.btns);
        dicMenu.Add(3, picture.btns);
        dicMenu.Add(4, achievement.btns);
        dicMenu.Add(5, options.btns);

        
        achievement.Init();
        picture.monsterPicture.Init();

        dicMenuEquip = new Dictionary<int, List<Button>>();
        dicMenuEquip.Add(0, equip.fragment.btns);
        dicMenuPic = new Dictionary<int, List<Button>>();
        dicMenuPic.Add(0, picture.monsterPicture.btns);
        DisplayChildPanel(mainButtonIndex);

        if (mainButtons.Length > 0)
        {
            mainButtons[mainButtonIndex].Select();
        }
        
    }


    void Update()
    {
        //�������������'1'
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (childPanelExpanded)//��һ���˵�
            {
                EnterSecond();
                childPanelExpanded = false;
                isSecond = true;
            }
            else//�Ƕ����˵�
            {
                if (mainButtonIndex == 1 || mainButtonIndex == 3)
                {
                    EnterThrid();
                    isSecond = false;
                    isThird = true;
                }
            }
        }

        //�л�����������'2'
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
        {

            if (childPanelExpanded)//��һ���˵�
            {
                FirstButton();
            }
            else if (isSecond)//�Ƕ����˵�
            {
                SecondButton();
            }
            else
            {
                ThirdButton();
            }

        }

        //���أ���������'3'�����ϼ����
        if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (isSecond)
            {
                SecondButtonIndex = 0;
                mainButtons[mainButtonIndex].Select();
                if(mainButtonIndex == 4)
                {
                    achievement.MoveDown(SecondButtonIndex);
                }
                isSecond = false;
                childPanelExpanded = true;
            }
            else if (isThird)
            {
                ReSecond();
                isSecond = true;
                isThird = false;
            }
        }
    }


    //��ʾĳ���Ӳ˵����
    void DisplayChildPanel(int index)
    {
        for (int i = 0; i < SecondPanel.Count; i++)
        {
            if (i == index)
            {
                if (!SecondPanel[i].activeSelf)
                {
                    SecondPanel[i].SetActive(true);
                }
            }
            else
            {
                if (SecondPanel[i].activeSelf)
                    SecondPanel[i].SetActive(false);
            }
        }
    }

    //��ʾ�ر����в˵����
    void ShowChildPanel()
    {
        for (int i = 0; i < SecondPanel.Count; i++)
        {
            if (!SecondPanel[i].activeSelf)
                SecondPanel[i].SetActive(true);
        }
    }

    //��������˵�
    public void EnterSecond()
    {
        dicMenu[mainButtonIndex][0].Select();
    }
    //���������˵�
    public void EnterThrid()
    {
        if(mainButtonIndex == 1)
        {
            dicMenuEquip[SecondButtonIndex][0].Select();
        }
        else if(mainButtonIndex == 3)
        {
            dicMenuPic[SecondButtonIndex][0].Select();
        }
    }

    //һ���˵���ť�л�
    public void FirstButton()
    {
        mainButtonIndex++;
        if (mainButtonIndex > mainButtons.Length - 1)
        {
            mainButtonIndex = 0;
        }
        
        mainButtons[mainButtonIndex].Select();
        DisplayChildPanel(mainButtonIndex);
    }

    //�����˵���ť�л�
    public void SecondButton()
    {
        SecondButtonIndex++;
        SecondButtonIndex = SecondButtonIndex > dicMenu[mainButtonIndex].Count - 1 ? 0 : SecondButtonIndex;
        dicMenu[mainButtonIndex][SecondButtonIndex].Select();
        if(mainButtonIndex ==4)
        {
            achievement.MoveDown(SecondButtonIndex);
        }
    }

    //�����˵���ť�л�
    public void ThirdButton()
    {
        ThirdButtonIndex++;
        
        if (mainButtonIndex == 1)
        {
            ThirdButtonIndex = ThirdButtonIndex > dicMenuEquip[SecondButtonIndex].Count - 1 ? 0 : ThirdButtonIndex;
            dicMenuEquip[SecondButtonIndex][ThirdButtonIndex].Select();
        }
        else
        {
            ThirdButtonIndex = ThirdButtonIndex > dicMenuPic[SecondButtonIndex].Count - 1 ? 0 : ThirdButtonIndex;
            dicMenuPic[SecondButtonIndex][ThirdButtonIndex].Select();
            picture.monsterPicture.MoveDown(ThirdButtonIndex);
        }
    }

    public void ReSecond()
    {
        dicMenu[mainButtonIndex][SecondButtonIndex].Select();
        ThirdButtonIndex = 0;
        if (mainButtonIndex == 3)
        {
            picture.monsterPicture.MoveDown(ThirdButtonIndex);
        }
    }

}

