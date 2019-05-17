using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using cn.sharesdk.unity3d;
using System;

public class Test : MonoBehaviour
{
    public Text text;
    public ShareSDK ssdk;
    public InputField imageInput;
    public InputField videoInput;
    void Awake()
    {
        text.text = Application.persistentDataPath;
        ssdk.authHandler = OnAuthResultHandler;
        ssdk.shareHandler = OnShareResultHandler;
        ssdk.showUserHandler = OnGetUserInfoResultHandler;
        ssdk.getFriendsHandler = OnGetFriendsResultHandler;
        ssdk.followFriendHandler = OnFollowFriendResultHandler;
    }

    public void SendImage()
    {
        ShareContent content = new ShareContent();
        String[] platfsList = { ((int)PlatformType.QQ).ToString(), ((int)PlatformType.Facebook).ToString(), ((int)PlatformType.TencentWeibo).ToString() };
        content.SetHidePlatforms(platfsList);

        content.SetText("this is a test string.");
        // content.SetImageUrl("http://ww3.sinaimg.cn/mw690/be159dedgw1evgxdt9h3fj218g0xctod.jpg");
        content.SetTitle("test title");
        var urls = imageInput.text;
        String[] imageArray = urls.Split(';');
        String testText = "";
        for (int i = 0; i < imageArray.Length; i++)
        {
            testText += $"{imageArray[i]}\n";
        }
        text.text = testText;
        content.SetImageArray(imageArray);
        content.SetShareType(ContentType.Image);
        content.SetEnableClientShare(true);
        ssdk.ShowPlatformList(null, content, 100, 100);
    }

    public void SendVideo()
    {
        ShareContent content = new ShareContent();
        var urls = videoInput.text;
        text.text = urls;
        content.SetFilePath(urls);
        content.SetShareType(ContentType.Video);
        ssdk.ShareContent(PlatformType.Twitter, content);
    }

    void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            if (result != null && result.Count > 0)
            {
                print("authorize success !" + "Platform :" + type + "result:" + MiniJSON.jsonEncode(result));
            }
            else
            {
                print("authorize success !" + "Platform :" + type);
            }
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

    void OnGetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get user info result :");
            print(MiniJSON.jsonEncode(result));
            print("AuthInfo:" + MiniJSON.jsonEncode(ssdk.GetAuthInfo(type)));
            print("Get userInfo success !Platform :" + type);
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            text.text = "fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"];
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            text.text = "fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"];
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            text.text = "cancel !";
            print("cancel !");
        }
    }

    void OnShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("share successfully - share result :");
            print(MiniJSON.jsonEncode(result));
            text.text = "share successfully - share result :" + MiniJSON.jsonEncode(result);
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            text.text = "fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"];

            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
            text.text = "fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"];

			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            text.text = "cancel !";

            print("cancel !");
        }
    }

    void OnGetFriendsResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get friend list result :");
            print(MiniJSON.jsonEncode(result));
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }

    void OnFollowFriendResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("Follow friend successfully !");
        }
        else if (state == ResponseState.Fail)
        {
#if UNITY_ANDROID
            print("fail! throwable stack = " + result["stack"] + "; error msg = " + result["msg"]);
#elif UNITY_IPHONE
			print ("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
#endif
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
        }
    }
}
