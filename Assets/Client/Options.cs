using System;

// key schema
[Serializable]
public class Options
{
	public string method;
	public string url;
    public Body body;
}

[Serializable]
public class Body
{
	public string client_id;
	public string client_secret;
	public string audience;
	public string grant_type;
}

// token schema
[Serializable]
public class Token
{
    public string access_token;
    public string expires_in;
    public string token_type;
}

// Data generic super class
[Serializable]
public class Data
{
}

// Progress schema
[Serializable]
public class Progress : Data
{
    public string user_id;
    public string question_id;
    public bool correct;
    public int attempt_num;
}
