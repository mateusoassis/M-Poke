using UnityEngine;

public class ui_mainBattleOverlayManager : MonoBehaviour
{
    [SerializeField] private GameObject[] setas;
    [SerializeField] private GameObject fightOverlayObject;
    [SerializeField] private int index;

    void Start()
    {
        UpdateArrows();
    }

    void Update()
    {
        // 0 1
        // 2 3
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(index == 0)
            {
                index = 2;
            }
            else if(index == 1)
            {
                index = 3;
            }
            else if(index == 2)
            {
                index = 0;
            }
            else if(index == 3)
            {
                index = 1;
            }
            UpdateArrows();
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(index == 0)
            {
                index = 2;
            }
            else if(index == 1)
            {
                index = 3;
            }
            else if(index == 2)
            {
                index = 0;
            }
            else if(index == 3)
            {
                index = 1;
            }
            UpdateArrows();
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(index == 0)
            {
                index = 1;
            }
            else if(index == 1)
            {
                index = 0;
            }
            else if(index == 2)
            {
                index = 3;
            }
            else if(index == 3)
            {
                index = 2;
            }
            UpdateArrows();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(index == 0)
            {
                index = 1;
            }
            else if(index == 1)
            {
                index = 0;
            }
            else if(index == 2)
            {
                index = 3;
            }
            else if(index == 3)
            {
                index = 2;
            }
            UpdateArrows();
        }
        else if(Input.GetKeyDown(KeyCode.Return) && index == 0)
        {
            ShowFightOverlay();
        }
    }

    public void UpdateArrows()
    {
        for (int i = 0; i < setas.Length; i++)
        {
            if(i == index)
            {
                setas[i].SetActive(true);
            }
            else
            {
                setas[i].SetActive(false);
            }
        }
    }

    public void ShowFightOverlay()
    {
        fightOverlayObject.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void SetIndex(int i)
    {
        index = i;
        UpdateArrows();
    }
}
