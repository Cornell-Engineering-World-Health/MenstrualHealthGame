using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BackendRequest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetRequest("http://localhost:3000/api/quiz/"));



        //StartCoroutine(PostRequest("http://localhost:3000/api/quiz/"));
    }

    IEnumerator PostRequest(string uri, WWWForm data)
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
                Debug.Log("Post complete");
            }

        }
    }

    IEnumerator GetRequest(string uri)
    { 
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                string data = webRequest.downloadHandler.text;
                Debug.Log(pages[page] + ":\nReceived: " + data);
            }
        }
    }

    /*
    Hashtable ParseRequest(string data)
    {
        string[] items = data.Split('}');
        foreach(string item in items)
        {
            string key = item.Substring(item.IndexOf(':') + 2);
            key = key.Substring(0, key.IndexOf('"'));

            //float score = item
        }
    }

    private class Data
    {
        string question, answer;
        float score;

        Data(string quest, string ans, float score)
        {
            question = quest;
            answer = ans;
            this.score = score;
        }
    }
    */
        
}
//[{"_id":"5dd80809205214432d80ed78","points":10,"answer":"25","question":"What is 5 * 5?","__v":0},{"_id":"5dd8081f205214432d80ed79","points":0,"answer":"55","question":"What is 11 * 5?","__v":0}]