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
    }

    [System.Serializable]
    public class pokeShow
    {
        public string pokeName;
//        public Array
    }

    void Start()
    {
        StartCoroutine(GetPokeData(url));
    }
    
    IEnumerator GetPokeData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if(request.result == UnityWebRequest.Result.Success)
        {
            //Debug.Log(request.downloadHandler.text);
            PokeData pokemonInfo = JsonUtility.FromJson<PokeData>(request.downloadHandler.text);
            Debug.Log(pokemonInfo.name);
            listaPokemonComMoves.Add(pokemonInfo.name);
            if(pokemonInfo.moves.Length > 4)
            {
                for(int i = 0; i < 4; i++)
                {
                    listaPokemonComMoves.Add(pokemonInfo.moves[i].move.name);
                }
            }
            else if(pokemonInfo.moves.Length <= 3)
            {
                for(int i = 0; i < 4; i++)
                {
                    if (i+1 <= pokemonInfo.moves.Length)
                    {
                        listaPokemonComMoves.Add(pokemonInfo.moves[i].move.name);
                    }
                    else
                    {
                        listaPokemonComMoves.Add("--");
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
