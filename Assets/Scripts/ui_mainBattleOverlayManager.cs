using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class ui_mainBattleOverlayManager : MonoBehaviour
{
    [SerializeField] private Fade fadeIn;
    [SerializeField] private GameObject fadeOutObject;
    [SerializeField] private GameObject[] setas;
    [SerializeField] private GameObject fightOverlayObject;
    [SerializeField] private int index;
    [SerializeField] private PokemonsPicker pokeData;
    [SerializeField] private TextMeshProUGUI flavorBattleText;

    [Header("Player")]
    [SerializeField] private TextMeshProUGUI playerPokemonName;
    [SerializeField] private TextMeshProUGUI playerPokemonLevel;
    [SerializeField] private TextMeshProUGUI playerPokemonHp;

    [Header("Enemy")]
    [SerializeField] private TextMeshProUGUI enemyPokemonName;
    [SerializeField] private TextMeshProUGUI enemyPokemonLevel;

    void Awake()
    {
        StartCoroutine(WaitForBattleReady());
    }
    
    IEnumerator WaitForBattleReady()
    {
        while(!PokemonBattleReady())
        {
            yield return null;
        }
        UpdateArrows();
        UpdateStats();
        StartCoroutine(fadeIn.FadeNow()); //fade in
        fadeOutObject.SetActive(false);
    }

    public void UpdateStats()
    {
        playerPokemonName.text = pokeData.pokeInfoCustomPlayer.pokeName.ToUpper();
        playerPokemonLevel.text = "Lv" + pokeData.pokeInfoCustomPlayer.level.ToString();
        playerPokemonHp.text = pokeData.pokeInfoCustomPlayer.hp.ToString() + "/" + pokeData.pokeInfoCustomPlayer.hp.ToString();
        flavorBattleText.text = "What will \n" + pokeData.pokeInfoCustomPlayer.pokeName.ToUpper() + " do?";

        enemyPokemonName.text = pokeData.pokeInfoCustomEnemy.pokeName.ToUpper();
        enemyPokemonLevel.text = "Lv" + pokeData.pokeInfoCustomEnemy.level.ToString();
    }

    public bool PokemonBattleReady()
    {
        return pokeData.playerImageDone && pokeData.playerMoveDone && pokeData.enemyImageDone;
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
