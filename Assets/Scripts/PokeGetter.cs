using System.Net;
using System.Net.Cache;
using System.Security.AccessControl;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class PokeGetter : MonoBehaviour
{
    [SerializeField] private string url = "https://pokeapi.co/api/v2/pokemon/";
    public pokemonInfoCustom pokeInfoCustom;
    public int randomPokemonIndex;

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
    public class pokemonInfoCustom
    {
        public string pokeName;
        public List<MoveData> pokeMoves;
        public Sprites enemy_front;
        public Sprites user_back;
        public Image enemySprite;
        public Image playerSprite;
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

    void Awake()
    {
        pokeInfoCustom.pokeMoves = new List<MoveData>{null, null, null, null};
        randomPokemonIndex = Random.Range(1,600);
        url += randomPokemonIndex;
    }
    void Start()
    {
        StartCoroutine(GetPokeData(url));
    }
    IEnumerator GetMoveDataAndSave(MoveData getMoveData, string url, List<MoveData> saveMoveData, int index)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            // saveMoveData.Add(getMoveData);
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

    IEnumerator UpdateEnemySprite(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            pokeInfoCustom.enemySprite.sprite = sprite;
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }
    IEnumerator UpdatePlayerSprite(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(request);
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            pokeInfoCustom.playerSprite.sprite = sprite;
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }

    IEnumerator GetPokeData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log(request.downloadHandler.text);
            PokeData pokemonInfo = JsonUtility.FromJson<PokeData>(request.downloadHandler.text);
            // Debug.Log(pokemonInfo.name);
            pokeInfoCustom.pokeName = pokemonInfo.name;
            pokeInfoCustom.front_url = pokemonInfo.sprites.front_default;
            StartCoroutine(UpdateEnemySprite(pokeInfoCustom.front_url));
            pokeInfoCustom.back_url = pokemonInfo.sprites.back_default;
            StartCoroutine(UpdatePlayerSprite(pokeInfoCustom.back_url));
            // pokeInfoCustom.pokeMoves.Add(pokemonInfo.moves[i].move.name);
            if(pokemonInfo.moves.Length > 4)
            {
                for(int i = 0; i < 4; i++)
                {
                    MoveData moveData = new MoveData();
                    moveData.name = pokemonInfo.moves[i].move.name;
                    StartCoroutine(GetMoveDataAndSave(moveData, pokemonInfo.moves[i].move.url, pokeInfoCustom.pokeMoves, i));
                    // pokeInfoCustom.pokeMoves[i].name = pokemonInfo.moves[i].move.name;
                    // pokeInfoCustom.pokeMoves.Add(moveData);
                }
            }
            else if(pokemonInfo.moves.Length <= 3)
            {
                for(int i = 0; i < 4; i++)
                {
                    if (i+1 <= pokemonInfo.moves.Length)
                    {
                        MoveData moveData = new MoveData();
                        moveData.name = pokemonInfo.moves[i].move.name;
                        StartCoroutine(GetMoveDataAndSave(moveData,pokemonInfo.moves[i].move.url,pokeInfoCustom.pokeMoves, i));
                        // pokeInfoCustom.pokeMoves[i].name = pokemonInfo.moves[i].move.name;
                        // pokeInfoCustom.pokeMoves.Add(moveData);
                    }
                    else
                    {
                        MoveData moveData = new MoveData();
                        moveData.name = "--";
                        // pokeInfoCustom.pokeMoves[i].name = "--";
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
}
