using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BackendRequest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost:3000/api/quiz/"));

    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            Debug.Log(pages[page]);

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
            }
        }
    }
}
