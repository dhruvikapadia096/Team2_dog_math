using UnityEngine.Networking;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public static class UserProfile
{


   public static string baseAzureFunctionUrl = "https://team2-database.azurewebsites.net/api/user/";
   public static string azureFunctionAuthenticationParams = "code=FvmKGPh5nj89X4LTHWQRR3oZaTuyPf08NTpPyWv2FbhaAzFu6MkS4g==&clientId=default";


  public static IEnumerator GetUsers(string deviceID, Action<User[]> onSuccess, Action<string> onError)
   {
       string queryParams = $"{azureFunctionAuthenticationParams}&deviceId={deviceID}";
       string url = $"{baseAzureFunctionUrl}?{queryParams}";
       Debug.Log("Getting List of Users" + url);


       using (UnityWebRequest www = UnityWebRequest.Get(url))
       {
           yield return www.SendWebRequest();


           if (www.result != UnityWebRequest.Result.Success)
           {
               string errorMessage = "Failed to fetch user data: " + www.error;
               Debug.LogError(errorMessage);
               onError?.Invoke(errorMessage);
               yield break;
           }


           string responseBody = www.downloadHandler.text;
           Debug.Log("Received JSON data: " + responseBody);
           try
           {
               ApiResponse response = JsonUtility.FromJson<ApiResponse>(responseBody);


               // Parse the 'Content' property as an ApiResponseContent object
               ApiResponseContent content = JsonUtility.FromJson<ApiResponseContent>(response.Content);


               UserContent userContent = new UserContent();
               userContent.users = content.users;
               List<User> userList = new List<User>();


               if (content != null && content.users != null)
               {
                   foreach (User user in userContent.users)
                   {
                       Debug.Log("User ID: " + user.UserID);
                       // Log other user properties as needed
                       userList.Add(user);
                   }


                   // Check if the users array is not null and has at least one element
                   if (content.users != null && content.users.Length > 0)
                   {
                       onSuccess?.Invoke(userList.ToArray());
                   }
               }
               else
               {
                   // Handle case where users array is null or empty
                   string errorMessage = "No users found in the response.";
                   Debug.Log(errorMessage);
                   onError?.Invoke(errorMessage);
               }
           }
           catch (Exception ex)
           {
               string errorMessage = "Error parsing JSON data: " + ex.Message;
               Debug.LogError(errorMessage);
               onError?.Invoke(errorMessage);
           }


       }
   }




  public static IEnumerator GetorCreateUser(string deviceId, string name, string profilePicture, System.Action<string> onSuccess, System.Action<string> onError)
{
   string queryParams = $"{azureFunctionAuthenticationParams}&deviceId={deviceId}&name={name}&profilePicture={profilePicture}";
   string url = $"{baseAzureFunctionUrl}?{queryParams}";


   Debug.Log($"Creating user @ {url}");


   using (UnityWebRequest www = UnityWebRequest.Put(url, new byte[0])) // Use byte[0] as the request body for PUT requests
   {
       yield return www.SendWebRequest();


       if (www.result != UnityWebRequest.Result.Success)
       {
           string errorMessage = "Failed to get user: " + www.error;
           Debug.LogError(errorMessage);
           onError?.Invoke("Error");
       }
       else
       {
           string responseBody = www.downloadHandler.text;
           Debug.Log("Received JSON data: " + responseBody);
           try
           {
               // Assuming apiResponse is the JSON string received from the API
               ApiResponse response = JsonUtility.FromJson<ApiResponse>(responseBody);
               Debug.Log("Received JSON data: " + response.Content);
               onSuccess?.Invoke(response.Content);
           }
           catch (System.Exception ex)
           {
               string errorMessage = "Error parsing JSON data: " + ex.Message;
               Debug.LogError(errorMessage);
               onError?.Invoke("Error");
           }
       }
   }
}


public static IEnumerator UpdateName(string userId, string name, System.Action<bool> onSuccess, System.Action<bool> onError)
{


   string endpoint = $"{userId}/updateName";
   string queryParams = $"?{azureFunctionAuthenticationParams}&name={name}";
   string url = $"{baseAzureFunctionUrl}{endpoint}{queryParams}";


   Debug.Log($"Updating user @ {url}");


   using (UnityWebRequest www = UnityWebRequest.Put(url, new byte[0])) // Use byte[0] as the request body for PUT requests
   {
       yield return www.SendWebRequest();


       if (www.result != UnityWebRequest.Result.Success)
       {
           string errorMessage = "Failed to update name: " + www.error;
           Debug.LogError(errorMessage);
           onError?.Invoke(false);
       }
       else
       {
           string responseBody = www.downloadHandler.text;
           Debug.Log("Received JSON data: " + responseBody);
           try
           {
               // Assuming apiResponse is the JSON string received from the API
               ApiResponse response = JsonUtility.FromJson<ApiResponse>(responseBody);
               onSuccess?.Invoke(true);
           }
           catch (System.Exception ex)
           {
               string errorMessage = "Error parsing JSON data: " + ex.Message;
               Debug.LogError(errorMessage);
               onError?.Invoke(false);
           }
       }
   }
}


}


