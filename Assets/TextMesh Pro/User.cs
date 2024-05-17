using UnityEngine;


[System.Serializable]
public class User
{
    public string UserID;
    public string Name;
    public bool IsSoundEnabled;
    public bool IsMusicEnabled;
    public int Age;
    public string Password;
    public string DeviceID;
    public string noOfGame;
}




[System.Serializable]
public class UserContent
{
    public User[] users;
}



