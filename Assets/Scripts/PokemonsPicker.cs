using System.Net;
using System.Net.Cache;
using System.Security.AccessControl;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class PokemonsPicker : MonoBehaviour
{
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private Fade fadeOut;
    [SerializeField] private ui_fightOverlay fightOverlay;
    [SerializeField] private string url = "https://pokeapi.co/api/v2/pokemon/";
    [SerializeField] private Sprite greenHpBar;
    [SerializeField] private Sprite yellowHpBar;
    [SerializeField] private Sprite redHpBar;
    [Header("Player")]
    [SerializeField] private int playerPokemonIndex;
    [SerializeField] private string playerUrl;
    public PokemonInfoCustom pokeInfoCustomPlayer;
    [SerializeField] private Image playerImage;
    public bool playerMoveDone = false;
    public bool playerImageDone = false;
    [SerializeField] private Image playerHpImage;

    [Header("Enemy")]
    [SerializeField] private int enemyPokemonIndex;
    [SerializeField] private string enemyUrl;
    public PokemonInfoCustom pokeInfoCustomEnemy;
    [SerializeField] private Image enemyImage;
    public bool enemyMoveDone = false;
    public bool enemyImageDone = false;
    [SerializeField] private Image enemyHpImage;

    void Awake()
    {
        InitialSetup();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(fadeOut.FadeNow());
        }
    }

    private void ErrorPanelShow()
    {
        errorPanel.SetActive(true);
    }

    private void InitialSetup()
    {
        pokeInfoCustomPlayer.pokeMoves = new List<MoveData>{null, null, null, null};
        playerPokemonIndex = Random.Range(1,601);
        playerUrl = url + playerPokemonIndex;
        pokeInfoCustomPlayer.level = Random.Range(10, 91);
        pokeInfoCustomPlayer.hp = 0;
        pokeInfoCustomPlayer.currentHp = 0;

        pokeInfoCustomEnemy.pokeMoves = new List<MoveData>{null, null, null, null};
        enemyPokemonIndex = Random.Range(1,601);
        enemyUrl = url + enemyPokemonIndex;
        pokeInfoCustomEnemy.level = Random.Range(10, 91);
        pokeInfoCustomEnemy.hp = 0;
        pokeInfoCustomEnemy.currentHp = 0;

        StartCoroutine(GetPokeDataPlayer(playerUrl, pokeInfoCustomPlayer, playerImage));
        StartCoroutine(GetPokeDataEnemy(enemyUrl, pokeInfoCustomEnemy, enemyImage));
    }

    IEnumerator GetPokeDataPlayer(string url, PokemonInfoCustom pokeInfoCustom, Image image)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            PokeData pokemonInfo = JsonUtility.FromJson<PokeData>(request.downloadHandler.text);

            pokeInfoCustom.pokeName = pokemonInfo.name;
            pokeInfoCustom.back_url = pokemonInfo.sprites.back_default;
            StartCoroutine(UpdateSpritePlayer(pokeInfoCustom.back_url, image));

            foreach (var stat_line in pokemonInfo.stats)
            {
                if(stat_line.stat.name == "hp")
                {
                    pokeInfoCustomPlayer.hp = (int)((2 * stat_line.base_stat * pokeInfoCustom.level)/100) + pokeInfoCustom.level + 10;
                    pokeInfoCustomPlayer.currentHp = Random.Range(1,pokeInfoCustomPlayer.hp+1);
                    float newFillAmount = (float)pokeInfoCustomPlayer.currentHp / pokeInfoCustomPlayer.hp;
                    playerHpImage.fillAmount = newFillAmount;
                    if(newFillAmount > 0.5f)
                    {
                        playerHpImage.sprite = greenHpBar;
                    }
                    else if(newFillAmount > 0.25f)
                    {
                        playerHpImage.sprite = yellowHpBar;
                    }
                    else
                    {
                        playerHpImage.sprite = redHpBar;
                    }
                    break;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                if(i < pokemonInfo.moves.Length)
                {
                    MoveData moveData = new MoveData();
                    moveData.name = pokemonInfo.moves[i].move.name;
                    StartCoroutine(GetMoveDataAndSavePlayer(moveData, pokemonInfo.moves[i].move.url, pokeInfoCustom.pokeMoves, i));
                }
                else
                {
                    MoveData moveData = new MoveData();
                    moveData.name = "--";
                    moveData.pp = 0;
                    moveData.type = " ";
                    pokeInfoCustom.pokeMoves[i] = moveData;
                }
            }
        }
        else
        {
            Debug.Log("Erro: " + request.error);
            ErrorPanelShow();
        }
    }
    

    IEnumerator GetPokeDataEnemy(string url, PokemonInfoCustom pokeInfoCustom, Image image)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            PokeData pokemonInfo = JsonUtility.FromJson<PokeData>(request.downloadHandler.text);

            pokeInfoCustom.pokeName = pokemonInfo.name;
            pokeInfoCustom.front_url = pokemonInfo.sprites.front_default;
            StartCoroutine(UpdateSpriteEnemy(pokeInfoCustom.front_url, image));

            foreach (var stat_line in pokemonInfo.stats)
            {
                if(stat_line.stat.name == "hp")
                {
                    pokeInfoCustomEnemy.hp = (int)((2 * stat_line.base_stat)/100) + pokeInfoCustom.level + 10;
                    pokeInfoCustomEnemy.currentHp = Random.Range(1,pokeInfoCustomEnemy.hp+1);
                    float newFillAmount = (float)pokeInfoCustomEnemy.currentHp / pokeInfoCustomEnemy.hp;
                    enemyHpImage.fillAmount = newFillAmount;
                    if(newFillAmount > 0.5f)
                    {
                        enemyHpImage.sprite = greenHpBar;
                    }
                    else if(newFillAmount > 0.25f)
                    {
                        enemyHpImage.sprite = yellowHpBar;
                    }
                    else
                    {
                        enemyHpImage.sprite = redHpBar;
                    }
                    break;
                }
            }

            // for (int i = 0; i < 4; i++)
            // {
            //     if(pokemonInfo.moves.Length > 4)
            //     {
            //         if (i+1 <= pokemonInfo.moves.Length)
            //         {
            //             MoveData moveData = new MoveData();
            //             moveData.name = pokemonInfo.moves[i].move.name;
            //             StartCoroutine(GetMoveDataAndSaveEnemy(moveData, pokemonInfo.moves[i].move.url, pokeInfoCustom.pokeMoves, i));
            //         }
            //         else if(pokemonInfo.moves.Length <= 3)
            //         {
            //             MoveData moveData = new MoveData();
            //             moveData.name = "--";
            //             moveData.pp = 0;
            //             moveData.type = " ";
            //             pokeInfoCustom.pokeMoves[i] = moveData;
            //         }
            //     }
            // }
        }
        else
        {
            Debug.Log("Erro: " + request.error);
            ErrorPanelShow();
        }
    }
    
    IEnumerator UpdateSpritePlayer(string url, Image targetImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            targetImage.sprite = sprite;
            playerImageDone = true;
        }
        else
        {
            Debug.Log("Erro: " + request.error);
            ErrorPanelShow();
        }
    }
    IEnumerator UpdateSpriteEnemy(string url, Image targetImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            targetImage.sprite = sprite;
            enemyImageDone = true;
        }
        else
        {
            Debug.Log("Erro: " + request.error);
            ErrorPanelShow();
        }
    }
    IEnumerator GetMoveDataAndSavePlayer(MoveData getMoveData, string url, List<MoveData> saveMoveData, int index)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            MoveStats moveStats = JsonUtility.FromJson<MoveStats>(request.downloadHandler.text);
            getMoveData.pp = moveStats.pp;
            getMoveData.type = moveStats.type.name;
            saveMoveData[index] = getMoveData;
            fightOverlay.moveTexts[index].text = saveMoveData[index].name.ToUpper();
            playerMoveDone = true;
        }
        else
        {
            Debug.Log("Erro: " + request.error);
            ErrorPanelShow();
        }
    }

    // IEnumerator GetMoveDataAndSaveEnemy(MoveData getMoveData, string url, List<MoveData> saveMoveData, int index)
    // {
    //     UnityWebRequest request = UnityWebRequest.Get(url);
    //     yield return request.SendWebRequest();

    //     if(request.result == UnityWebRequest.Result.Success)
    //     {
    //         MoveStats moveStats = JsonUtility.FromJson<MoveStats>(request.downloadHandler.text);
    //         getMoveData.pp = moveStats.pp;
    //         getMoveData.type = moveStats.type.name;
    //         saveMoveData[index] = getMoveData;
    //         enemyMoveDone = true;
    //     }
    //     else
    //     {
    //         Debug.Log("Erro: " + request.error);
    //     }
    // }

    [System.Serializable]
    public class PokeData
    {
        public string name;
        public Sprites sprites;
        public MoveWrapper[] moves;
        public StatInfo[] stats;
    }
    
    [System.Serializable]
    public class StatInfo
    {
        public int base_stat;
        public Stat stat;
    }

    [System.Serializable]
    public class Stat
    {
        public string name;
    }

    [System.Serializable]
    public class Sprites
    {
        public string back_default;
        public string front_default;
    }
    
    [System.Serializable]
    public class MoveWrapper
    {
        public Move move;
    }
    
    [System.Serializable]
    public class Move
    {
        public string name;
        public string url;
    }

    [System.Serializable]
    public class MoveStats
    {
        public int pp;
        public MoveType type;
    }

    [System.Serializable]
    public class MoveType
    {
        public string name;
    }

    [System.Serializable]
    public class PokemonInfoCustom
    {
        public string pokeName;
        public List<MoveData> pokeMoves;
        public Sprites enemy_front;
        public Sprites user_back;
        public string front_url;
        public string back_url;
        public int level;
        public int hp;
        public int currentHp;
    }

    [System.Serializable]
    public class MoveData
    {
        public string name;
        public string type;
        public int pp;
    }
}
