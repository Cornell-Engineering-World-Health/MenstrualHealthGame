using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System;

public class BackendRequest : MonoBehaviour
{
    // Use this for initialization

    Options options;

    void Start()
    {   
        string raw_data = "{\"user_id\":\"42\",\"question_id\":\"324\",\"correct\":false,\"attempt_num\":2}";

        Data data = JsonUtility.FromJson<Progress>(raw_data); // declares a Progress object

        request("progress", this.POST, data); 

        request("progress", this.GET); 
    }

    /** request to backend at endpoint */
    void request(string endpoint, Action<string, string, string> method) // request for GET methods with no data
    {
        options = getOptions("./Assets/Client/key.json");
        this.StartCoroutine(this.GetKey(options, endpoint, method));
    }

    /** request to backend at endpoint with data as an object */
    void request(string endpoint, Action<string, string, string> method, Data data) // request for POST methods with Data object
    {
        options = getOptions("./Assets/Client/key.json");
        string req = JsonUtility.ToJson(data);
        this.StartCoroutine(this.GetKey(options, endpoint, method, req));
    }

    /** request to backend at endpoint with data as a string */
    void request(string endpoint, Action<string, string, string> method, string data) // request for POST methods with Data string
    {
        options = getOptions("./Assets/Client/key.json");
        this.StartCoroutine(this.GetKey(options, endpoint, method, data));
    }

    /** convert json file into Options object */
    private Options getOptions(string path)
    {
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<Options>(json);
    }

    /** verify json string is not null, then convert to Token object and return stringified token */
    private string authenticate(string json)
    {
        if (json == null)
        {
            Debug.Log("Authentication Failed");
            return "";
        }

        Token token = JsonUtility.FromJson<Token>(json);
        return token.token_type + " " + token.access_token;
    }

    /** acquire key and run method request */
    private IEnumerator GetKey(Options opt, string endpoint, Action<string, string, string> method = null, string req = null)
    {
        string URL = opt.url;
        string data = JsonUtility.ToJson(opt.body);

        using (UnityWebRequest webRequest = UnityWebRequest.Put(URL, data))
        {
            webRequest.method = "POST";
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            yield return webRequest.SendWebRequest();

            string token = "";
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log("ERROR: " + webRequest.error);
            }
            else
            {
                string text = webRequest.downloadHandler.text; //string representation of data
                token = authenticate(text); //convert text to token
            }

            if (method != null)
            {
                method(endpoint, token, req); //call relevant HTTP request
            }
        }

    }

    /** HTTP Request GET */
    void GET(string endpoint, string auth, string data)
    {
        StartCoroutine(GetRequest("https://menstralhealthgameserver.herokuapp.com/api/" + endpoint, auth));
    }

    /** HTTP Request POST */
    void POST(string endpoint, string auth, string data)
    {
        StartCoroutine(PostRequest("https://menstralhealthgameserver.herokuapp.com/api/" + endpoint, auth, data));
    }

    /** GET REQUEST */
    IEnumerator GetRequest(string uri, string authorization) 
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("Authorization", authorization);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
            }
            else
            {
                string text = webRequest.downloadHandler.text; //string representation of data
                Debug.Log("Received: " + text);
            }
        }
    }

    /** POST REQUEST */
    IEnumerator PostRequest(string uri, string authorization, string data) 
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Put(uri, data))
        {
            webRequest.method = "POST";
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");
            webRequest.SetRequestHeader("Authorization", authorization);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log("ERROR: " + webRequest.error);
            }
            else
            {
                string text = webRequest.downloadHandler.text; //string representation of data
                Debug.Log("Received: " + text);
            }
        }
    }
}

    
