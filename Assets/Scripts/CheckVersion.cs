using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Android;
using UnityEngine.Networking;
struct GetdataOutput //структура для чтения json файла конфигурации
{
    public string version; //номер версии
    public string link; //ссылка до apk файла
}
public class CheckVersion : MonoBehaviour
{
    public string AppName; //Название APK файла (без расширения .apk), например LiteraryTriangle
    public string LinkToJson; //Ссылка до файла конфигурации json (вместе с расширением .json),
                              //например http://website/folder/version.json
    public string FtpAddress; //192.168.0.159
    public string FtpLogin; //fabrica
    public string FtpPassword; //fabrica@2024

    public string NameApkFile;
    public string DownloadPath;
    public float AppVersion;

    public Text txtLoading; //текст со статусом загрузки
    public bool IsNeedDownload;
    private void Start()
    {
        //StartCoroutine(GetRequest());
        txtLoading.gameObject.SetActive(false);
        CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        ci.NumberFormat.CurrencyDecimalSeparator = ".";

        AppVersion = float.Parse(Application.version, NumberStyles.Any, ci);

        DownloadPath = Application.persistentDataPath;
        DownloadPath = Path.Combine(DownloadPath, $"{AppName}.apk");

        txtLoading.text = AppVersion.ToString();
        txtLoading.text = DownloadPath;

        GetDirList();
    }
    public void GetDirList()
    {
        NetworkCredential networkCredential = new NetworkCredential(FtpLogin, FtpPassword);
        string p = $"ftp://{FtpAddress}/apk" + "/";
        FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(p);
        if (ftpWebRequest == null)
        {
            return;
        }
        ftpWebRequest.Credentials = networkCredential;
        ftpWebRequest.Proxy = null;
        ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
        ftpWebRequest.KeepAlive = false;
        ftpWebRequest.UseBinary = true;

        FtpWebResponse ftpWebResponse = ftpWebRequest.GetResponse() as FtpWebResponse;
        if (ftpWebResponse == null)
        {
            return;
        }
        Stream s = ftpWebResponse.GetResponseStream();
        if (s == null)
        {
            return;
        }
        List<string> res = new List<string>();
        using (StreamReader sr = new StreamReader(s))
        {
            string str = sr.ReadLine();
            while (str != null)
            {
                res.Add(str);
                if (str.Contains(AppName + "_"))
                {
                    NameApkFile = str;
                    string l = $"ftp://{FtpAddress}/apk" + "/" + NameApkFile;

                    CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat.CurrencyDecimalSeparator = ".";
                    float v = float.Parse(NameApkFile.Substring(NameApkFile.IndexOf('_') + 1, NameApkFile.IndexOf('.') - (NameApkFile.IndexOf('_') + 1)).Replace('-', '.'), NumberStyles.Any, ci);

                    Debug.Log(v);
                    if (v > AppVersion)
                    {
                        StartCoroutine(DownloadApk(l, v));
                        return;
                    }

                    break;
                }
                Debug.Log(str);
                str = sr.ReadLine();
            }
        }
        s.Close();
        ftpWebResponse.Close();
    }
    public IEnumerator DownloadApk(string l, float v)
    {
        IsNeedDownload = true;
        txtLoading.gameObject.SetActive(true);
        txtLoading.text = $"Скачивается обновление (v{v})";

        yield return new WaitForSeconds(1f);

        downloadWithFTP(l, DownloadPath, FtpLogin, FtpPassword);
    }
    private byte[] downloadWithFTP(string ftpUrl, string savePath = "", string userName = "", string password = "")
    {
        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(ftpUrl));
        //request.Proxy = null;

        request.UsePassive = true;
        request.UseBinary = true;
        request.KeepAlive = true;

        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
        {
            request.Credentials = new NetworkCredential(userName, password);
        }

        request.Method = WebRequestMethods.Ftp.DownloadFile;

        if (!string.IsNullOrEmpty(savePath))
        {
            downloadAndSave(request.GetResponse(), savePath);
            return null;
        }
        else
        {
            return downloadAsbyteArray(request.GetResponse());
        }
    }

    byte[] downloadAsbyteArray(WebResponse request)
    {
        using (Stream input = request.GetResponseStream())
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while (input.CanRead && (read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }

    void downloadAndSave(WebResponse request, string savePath)
    {
        Stream reader = request.GetResponseStream();

        FileStream fileStream = new FileStream(savePath, FileMode.Create);


        int bytesRead = 0;
        byte[] buffer = new byte[2048];

        while (true)
        {
            bytesRead = reader.Read(buffer, 0, buffer.Length);

            if (bytesRead == 0)
                break;

            fileStream.Write(buffer, 0, bytesRead);
        }

        //Резервное копирование APK в папку Download
        //File.Copy(Application.persistentDataPath + $"/{AppName}.apk", "/storage/emulated/0/Download" + $"/{AppName}.apk", true);
        //Установка APK
        InstallApp(Application.persistentDataPath + $"/{AppName}.apk");

        fileStream.Close();
    }
    public void CheckAppDownload() 
    {
        if (IsNeedDownload)
        {
            InstallApp(Application.persistentDataPath + $"/{AppName}.apk");
        }
    }
    /// <summary>
    /// Установка приложения из APK файла на устройство
    /// </summary>
    /// <param name="apkPath"></param>
    public void InstallApp(string apkPath) 
    {
        txtLoading.text = "APK устанавливается";

        //Get Activity then Context
        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext");

        //Get the package Name
        string packageName = unityContext.Call<string>("getPackageName");
        string authority = packageName + ".fileprovider";

        AndroidJavaClass intentObj = new AndroidJavaClass("android.content.Intent");
        string ACTION_VIEW = intentObj.GetStatic<string>("ACTION_VIEW");
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", ACTION_VIEW);


        int FLAG_ACTIVITY_NEW_TASK = intentObj.GetStatic<int>("FLAG_ACTIVITY_NEW_TASK");
        int FLAG_GRANT_READ_URI_PERMISSION = intentObj.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION");

        //File fileObj = new File(String pathname);
        AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", apkPath);
        //FileProvider object that will be used to call it static function
        AndroidJavaClass fileProvider = new AndroidJavaClass("androidx.core.content.FileProvider");
        //getUriForFile(Context context, String authority, File file)
        AndroidJavaObject uri = fileProvider.CallStatic<AndroidJavaObject>("getUriForFile", unityContext, authority, fileObj);

        intent.Call<AndroidJavaObject>("setDataAndType", uri, "application/vnd.android.package-archive");
        intent.Call<AndroidJavaObject>("addFlags", FLAG_ACTIVITY_NEW_TASK);
        intent.Call<AndroidJavaObject>("addFlags", FLAG_GRANT_READ_URI_PERMISSION);
        currentActivity.Call("startActivity", intent);
        txtLoading.text = "APK установлен";
    }
}
