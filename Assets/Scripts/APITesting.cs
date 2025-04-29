using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class APITesting : MonoBehaviour
{
    [SerializeField] private string url = "https://pokeapi.co/api/v2/pokemon/squirtle";

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
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Erro: " + request.error);
        }
    }
}
