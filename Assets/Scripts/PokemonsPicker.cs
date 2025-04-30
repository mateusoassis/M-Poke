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
    [SerializeField] private string url = "https://pokeapi.co/api/v2/pokemon/";
    [Header("Player")]
    [SerializeField] private int playerPokemonIndex;
    [SerializeField] private string playerUrl;
    public PokemonInfoCustom pokeInfoCustomPlayer;
    [SerializeField] private Image playerImage;

    [Header("Enemy")]
    [SerializeField] private int enemyPokemonIndex;
    [SerializeField] private string enemyUrl;
    public PokemonInfoCustom pokeInfoCustomEnemy;
    [SerializeField] private Image enemyImage;

    void Awake()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        pokeInfoCustomPlayer.pokeMoves = new List<MoveData>{null, null, null, null};
        playerPokemonIndex = Random.Range(1,601);
        playerUrl = url + playerPokemonIndex;

        pokeInfoCustomEnemy.pokeMoves = new List<MoveData>{null, null, null, null};
        enemyPokemonIndex = Random.Range(1,601);
        enemyUrl = url + enemyPokemonIndex;

        StartCoroutine(GetPokeDataPlayer(playerUrl, pokeInfoCustomPlayer, playerImage));
        StartCoroutine(GetPokeDataEnemy(enemyUrl, pokeInfoCustomEnemy, enemyImage));
    }

    void Start()
    {
        
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
            StartCoroutine(UpdateSprite(pokeInfoCustom.back_url, image));

            for (int i = 0; i < 4; i++)
            {
                if(pokemonInfo.moves.Length > 4)
                {
                    if (i+1 <= pokemonInfo.moves.Length)
                    {
                        MoveData moveData = new MoveData();
                        moveData.name = pokemonInfo.moves[i].move.name;
                        StartCoroutine(GetMoveDataAndSave(moveData, pokemonInfo.moves[i].move.url, pokeInfoCustom.pokeMoves, i));
                    }
                    else if(pokemonInfo.moves.Length <= 3)
                    {
                        MoveData moveData = new MoveData();
                        moveData.name = "--";
                        moveData.pp = 0;
                        moveData.type = " ";
                        pokeInfoCustom.pokeMoves[i] = moveData;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Erro: " + request.error);
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
            StartCoroutine(UpdateSprite(pokeInfoCustom.front_url, image));

            for (int i = 0; i < 4; i++)
            {
                if(pokemonInfo.moves.Length > 4)
                {
                    if (i+1 <= pokemonInfo.moves.Length)
                    {
                        MoveData moveData = new MoveData();
                        moveData.name = pokemonInfo.moves[i].move.name;
                        StartCoroutine(GetMoveDataAndSave(moveData, pokemonInfo.moves[i].move.url, pokeInfoCustom.pokeMoves, i));
                    }
                    else if(pokemonInfo.moves.Length <= 3)
                    {
                        MoveData moveData = new MoveData();
                        moveData.name = "--";
                        moveData.pp = 0;
                        moveData.type = " ";
                        pokeInfoCustom.pokeMoves[i] = moveData;
                    }
                }
            }
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }
    
    IEnumerator UpdateSprite(string url, Image targetImage)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            targetImage.sprite = sprite;
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }
    IEnumerator GetMoveDataAndSave(MoveData getMoveData, string url, List<MoveData> saveMoveData, int index)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            MoveStats moveStats = JsonUtility.FromJson<MoveStats>(request.downloadHandler.text);
            getMoveData.pp = moveStats.pp;
            getMoveData.type = moveStats.type.name;
            saveMoveData[index] = getMoveData;
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }

    [System.Serializable]
    public class PokeData
    {
        public string name;
        public Sprites sprites;
        public MoveWrapper[] moves;
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
    }

    [System.Serializable]
    public class MoveData
    {
        public string name;
        public string type;
        public int pp;
    }
}
