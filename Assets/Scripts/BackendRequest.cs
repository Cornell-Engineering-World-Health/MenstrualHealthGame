using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BackendRequest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //GetRequestExample();

        //PostRequestExample();

        PutRequestExample();
    }

    void PutRequestExample()
    {
        WWWForm form = new WWWForm();
        string id = "5dd807a8205214432d80ed77";
        form.AddField("answer", "20");
        form.AddField("points", 0);
        StartCoroutine(PutRequest("http://localhost:3000/api/quiz/" + id, form));
    }

    void GetRequestExample()
    {
        StartCoroutine(GetRequest("http://localhost:3000/api/quiz/"));
    }

    void PostRequestExample()
    {
        WWWForm form = new WWWForm();
        form.AddField("question", "What is 5 * 5?");
        form.AddField("answer", "25");
        form.AddField("points", 1);
        StartCoroutine(PostRequest("http://localhost:3000/api/quiz/", form));
    }

    IEnumerator PostRequest(string uri, WWWForm data) //POST REQUEST
    {
        using (UnityWebRequest www = UnityWebRequest.Post(uri, data))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError) 
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("POST complete");
            }
        }
    }

    IEnumerator GetRequest(string uri) //GET REQUEST
    { 
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                string data = webRequest.downloadHandler.text; //string representation of data
                Debug.Log("Received: " + data);
            }
        }
    }

    IEnumerator PutRequest(string uri, WWWForm data) //PUT REQUEST
    { 
        using (UnityWebRequest www = UnityWebRequest.Put(uri, data.ToString()))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("PUT complete");
            }
        }
    }
}
