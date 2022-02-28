﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IDA.App.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

namespace IDA.App.Services
{
    public class IDAAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:39578/IDAAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:39578/IDAAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:39578/IDAAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:39578/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://192.168.1.14:39578/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "http://localhost:39578/Images/"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static IDAAPIProxy proxy = null;

        # region CreateProxy
        public static IDAAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
            }

            if (proxy == null)
                proxy = new IDAAPIProxy(baseUri, basePhotosUri);
            return proxy;
        }

        #endregion

        private IDAAPIProxy(string baseUri, string basePhotosUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
        }

        public string GetBasePhotoUri() { return this.basePhotosUri; }

        #region login
        //Login - if user name and password are correct User object is returned. otherwise a null will be returned
        public async Task<User> LoginAsync(string email, string pass)
        {
            Worker w = null;
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Login?email={email}&pass={pass}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    if (u.IsWorker)
                    {
                        w = JsonSerializer.Deserialize<Worker>(content, options);
                        return w;
                    }
                    else
                        return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region CustomerRegister
        //This method register a new user into the server database. A previous login is NOT required! The nick name and email must be uniqe!
        //it returns true is succeeded or false otherwise
        //questions are ignored upon registering a user and shoul dbe added seperatly.
        //if succeeded - the user is automatically logged in on the server


        public async Task<User> UserRegister(User c)
        { 
            try
            {
                //Set thge user to be user and not worker
                c.IsWorker = false;
                JsonSerializerOptions options = new JsonSerializerOptions
                {

                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<User>(c, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
               

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UserRegister", content);
                if (response.IsSuccessStatusCode)
                {


                    string str = await response.Content.ReadAsStringAsync();

                    User u = JsonSerializer.Deserialize<User>(str, options);
                    return u;
                }
                else
                {
                    return null;

                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
        #endregion

        #region WorkerRegister
        public async Task<Worker> WorkerRegister(Worker w)
        {
             try
            {
                ////set worker as worker in user part of the object
                //w.IsWorker = true;

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                  //  Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };

                string jsonObject = JsonSerializer.Serialize<Worker>(w, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/WorkerRegister", content);
                if (response.IsSuccessStatusCode)
                {

                    string str = await response.Content.ReadAsStringAsync();

                    Worker worker = JsonSerializer.Deserialize<Worker>(str, options);
                    return worker;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        #endregion

        #region GetServices
        public async Task<List<Service>> GetServices()
        {
            try
            {
               
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetServices");


                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<Service> u = JsonSerializer.Deserialize<List<Service>>(content, options);

                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }


        #endregion

        #region EmailExist
        public async Task<bool> EmailExistAsync(string email)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"{this.baseUri}/IsEmailExist?email={email}");
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return true;
            }

        }


        #endregion

        #region upload image
        ////Upload file to server (only images!)
        //public async Task<bool> UploadImage(Models.FileInfo fileInfo, string targetFileName)
        //{
        //    try
        //    {
        //        var multipartFormDataContent = new MultipartFormDataContent();
        //        var fileContent = new ByteArrayContent(File.ReadAllBytes(fileInfo.Name));
        //        multipartFormDataContent.Add(fileContent, "file", targetFileName);
        //        HttpResponseMessage response = await client.PostAsync($"{this.baseUri}/UploadImage", multipartFormDataContent);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            return true;
        //        }
        //        else
        //            return false;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return false;
        //    }
        //}

        #endregion

        #region worker availbilty

        //public async Task<bool> UpdateWorkerAvailbilty(Worker w)
        //{
        //    try
        //    {
        //        JsonSerializerOptions options = new JsonSerializerOptions
        //        {
        //            ReferenceHandler = ReferenceHandler.Preserve,
        //            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
        //            PropertyNameCaseInsensitive = true
        //        };

        //        string jsonObject = JsonSerializer.Serialize<bool>(w.AvailbleUntil, options);
        //        StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateWorkerAvailbilty", content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string jsonContent = await response.Content.ReadAsStringAsync();
        //            bool ret = JsonSerializer.Deserialize<bool>(jsonContent, options);

        //            return ret;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return false;
        //    }
        //}

        #endregion

        #region worker availbilty

        //public async Task<bool> AvailbleUntill(Worker w)
        //{

        //    try
        //    {
        //        JsonSerializerOptions options = new JsonSerializerOptions
        //        {
        //            ReferenceHandler = ReferenceHandler.Preserve,
        //            Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
        //            PropertyNameCaseInsensitive = true
        //        };

        //        string jsonObject = JsonSerializer.Serialize<bool>(w.IsAvailble, options);
        //        StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

        //        HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateWorkerAvailbilty", content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string jsonContent = await response.Content.ReadAsStringAsync();
        //            bool ret = JsonSerializer.Deserialize<bool>(jsonContent, options);

        //            return ret;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return false;
        //    }
        //}

        #endregion
    }




}
   
