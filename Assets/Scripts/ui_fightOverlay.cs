using UnityEngine;
using TMPro;
using System.Collections;

public class ui_fightOverlay : MonoBehaviour
{
    [SerializeField] private GameObject[] setas;
    [SerializeField] private GameObject mainBattleOverlayObject;
    [SerializeField] private int index;
    [SerializeField] private TextMeshProUGUI ppTexts;
    [SerializeField] private TextMeshProUGUI typeTexts;
    public TextMeshProUGUI[] moveTexts;
    [SerializeField] private PokemonsPicker pokeData;

    void OnEnable()
    {
        UpdateArrowsAndMoves();
        UpdateMovesNames();
    }

    void Update()
    {
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
                UpdateArrowsAndMoves();
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
                UpdateArrowsAndMoves();
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
                UpdateArrowsAndMoves();
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
                UpdateArrowsAndMoves();
            }
            else if(Input.GetKeyDown(KeyCode.Escape))
            {
                ShowMainBattleOverlay();
            }
        }
    }

    public void PullingMovesInfo(int i)
    {
        if(pokeData.pokeInfoCustomPlayer.pokeMoves[i].pp == 0)
        {
            ppTexts.text = " ";
        }
        else
        {
            ppTexts.text = pokeData.pokeInfoCustomPlayer.pokeMoves[i].pp + "/" + pokeData.pokeInfoCustomPlayer.pokeMoves[i].pp;
        }
        typeTexts.text = pokeData.pokeInfoCustomPlayer.pokeMoves[i].type.ToUpper();
    }

    public void UpdateArrowsAndMoves()
    {
        for (int i = 0; i < setas.Length; i++)
        {
            if(i == index)
            {
                setas[i].SetActive(true);
                PullingMovesInfo(index);
            }
            else
            {
                setas[i].SetActive(false);
            }
        }
    }

    public void UpdateMovesNames()
    {
        for (int i = 0; i < setas.Length; i++)
        {
            moveTexts[i].text = pokeData.pokeInfoCustomPlayer.pokeMoves[i].name.ToUpper();
        }
    }

    public void ShowMainBattleOverlay()
    {
        index = 0;
        mainBattleOverlayObject.SetActive(true);
        UpdateArrowsAndMoves();
        this.gameObject.SetActive(false);
    }

    public void SetIndex(int i)
    {
        index = i;
        UpdateArrowsAndMoves();
    }
}
