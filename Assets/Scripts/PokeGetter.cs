using System.Net;
using System.Net.Cache;
using System;
using System.Security.AccessControl;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PokeGetter : MonoBehaviour
{
    [SerializeField] private string url = "https://pokeapi.co/api/v2/pokemon/ditto";
    public List<string> listaPokemonComMoves = new List<string>();
    public pokemonInfoCustom pokeInfoCustom;

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
                
            // }
            // listaPokemonComMoves.Add(pokemonInfo.name);
            // if(pokemonInfo.moves.Length > 4)
            // {
            //     for(int i = 0; i < 4; i++)
            //     {
            //         listaPokemonComMoves.Add(pokemonInfo.moves[i].move.name);
            //     }
            // }
            // else if(pokemonInfo.moves.Length <= 3)
            // {
            //     for(int i = 0; i < 4; i++)
            //     {
            //         if (i+1 <= pokemonInfo.moves.Length)
            //         {
            //             listaPokemonComMoves.Add(pokemonInfo.moves[i].move.name);
            //         }
            //         else
            //         {
            //             listaPokemonComMoves.Add("--");
            //         }
            //     }
                
            // }
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }
}
