using System;
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
using IDA.DTO;

namespace IDA.App.Services
{
    public class IDAAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        private const string CLOUD_PHOTOS_URL = "TBD";
        private const string CLOUD_DATA_URL = "TBD";
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:39578/IDAAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://192.168.1.14:39578/IDAAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:39578/IDAAPI"; //API url when using windoes on development
        private const string DEV_ANDROID_EMULATOR_PHOTOS_URL = "http://10.0.2.2:39578/Images/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_PHOTOS_URL = "http://192.168.1.14:39578/Images/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_PHOTOS_URL = "http://localhost:39578/Images/"; //API url when using windoes on development


        private const string DEV_ANDROID_EMULATOR_DATA_URL = "http://10.0.2.2:39578/data/"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_DATA_URL = "http://192.168.1.14:39578/data/"; //API url when using physucal device on android
        private const string DEV_WINDOWS_DATA_URL = "https://localhost:39578/data/"; //API url when using windoes on development  // אולי לא יעבוד!


        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private string baseDataUri;
        private static IDAAPIProxy proxy = null;

        # region CreateProxy
        public static IDAAPIProxy CreateProxy()
        {
            string baseUri;
            string basePhotosUri;
            string baseDataUri;
            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                        basePhotosUri = DEV_ANDROID_EMULATOR_PHOTOS_URL;
                        baseDataUri = DEV_ANDROID_EMULATOR_DATA_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                        basePhotosUri = DEV_ANDROID_PHYSICAL_PHOTOS_URL;
                        baseDataUri = DEV_ANDROID_PHYSICAL_DATA_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                    basePhotosUri = DEV_WINDOWS_PHOTOS_URL;
                    baseDataUri = DEV_WINDOWS_DATA_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
                basePhotosUri = CLOUD_PHOTOS_URL;
                baseDataUri = CLOUD_DATA_URL;
            }

            if (proxy == null)
                proxy = new IDAAPIProxy(baseUri, basePhotosUri, baseDataUri);
            return proxy;
        }

        private IDAAPIProxy(string baseUri, string basePhotosUri, string baseDataUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
            this.basePhotosUri = basePhotosUri;
            this.baseDataUri = baseDataUri;
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

        #region get worker
        public async Task<Worker> GetWorkerAsync(int? workerId)
        {
            try
            {
                if (workerId == null)
                    return null;
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetWorker?workerId={workerId}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    Worker w = JsonSerializer.Deserialize<Worker>(content, options);
                    return w;
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
                    //TO DO: loop  through  user job offers and get chosen worker for each one!
                    foreach(JobOffer job in u.JobOffers)
                    {
                        if (job.ChosenWorkerId != null)
                            job.ChosenWorker = await this.GetWorkerAsync(job.ChosenWorkerId);
                    }
                    if (u.IsWorker)
                    {
                        w = JsonSerializer.Deserialize<Worker>(content, options);
                        foreach (JobOffer job in w.WorkerJobOffers)
                        {
                            if (job.ChosenWorkerId != null)
                                job.ChosenWorker = await this.GetWorkerAsync(job.ChosenWorkerId);
                        }
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

        #region User Register
       

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

        #region Worker Register
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

        #region Get Services
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

        #region Is Email Exist
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

        #region workers reviews

        
        public async Task<List<JobOffer>> GetWorkerReviews()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetAllWR");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<JobOffer> lst = JsonSerializer.Deserialize<List<JobOffer>>(content, options);
                    return lst;
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

        #region worker availbilty

        public async Task<bool> UpdateWorkerAvailbilty(Worker w)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };

                string jsonObject = JsonSerializer.Serialize<DateTime>(w.AvailbleUntil, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateWorkerAvailbilty", content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool ret = JsonSerializer.Deserialize<bool>(jsonContent, options);

                    return ret;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion

        #region User Update

        public async Task<User> UserUpdate(User c)
        {
            try
            {
                 
                c.IsWorker = false;
                JsonSerializerOptions options = new JsonSerializerOptions
                {

                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                string jsonObject = JsonSerializer.Serialize<User>(c, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");


                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/UpdateUser", content);
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

        #region Worker Update
        public async Task<bool> WorkerUpdate(Worker w)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string jsonObject = JsonSerializer.Serialize<Worker>(w, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/WorkerUpdate", content);
                if (response.IsSuccessStatusCode)
                {

                    string str = await response.Content.ReadAsStringAsync();

                    bool success = JsonSerializer.Deserialize<bool>(str, options);
                    return success;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
        #endregion

        #region GetCitiesNameList
        private List<string> GetCitiesNameList(List<City> cities)
        {
            List<string> citiesName = new List<string>();

            foreach (City city in cities)
            {
                citiesName.Add(city.english_name);
            }
            citiesName.Remove(citiesName[0]);

            return citiesName;
        }
        #endregion

        #region GetCitiesAsync
        public async Task<List<string>> GetCitiesAsync()
        {
            ///royts/israel-cities/master/israel-cities.json
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/cities.json");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<City> cities = JsonSerializer.Deserialize<List<City>>(content, options);

                    return GetCitiesNameList(cities);
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

        #region GetStreetsNameList
        private List<string> GetStreetsNameList(List<Street> streets/*, string city*/)
        {
            List<string> streetsName = new List<string>();

            foreach (Street street in streets)
            {
                streetsName.Add(street.street_name);
            }

            return streetsName;
        }
        #endregion

        #region GetStreetsAsync
        public async Task<List<string>> GetStreetsAsync(/*string city*/)
        {
            //?resource_id=d4901968-dad3-4845-a9b0-a57d027f11ab&limit=1500
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/streets.json?666");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();

                    List<Street> streets = JsonSerializer.Deserialize<List<Street>>(content, options);
                    return GetStreetsNameList(streets/*, city*/);
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

        #region GetStreetListAsync
        public async Task<List<Street>> GetStreetListAsync()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/streets.json?666");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();

                    List<Street> streets = JsonSerializer.Deserialize<List<Street>>(content, options);
                    return streets;
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

        #region GetStreetsNameByCity
        private List<string> GetStreetsNameByCity(List<Street> streets, string city)
        {
            List<string> streetsName = new List<string>();

            foreach (Street street in streets)
            {
                if (street.city_name == city)
                    streetsName.Add(street.street_name);
            }

            return streetsName;
        }
        #endregion

        #region GetStreetsByCityAsync
        public async Task<List<string>> GetStreetsByCityAsync(string city)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseDataUri}/streets.json?666");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();

                    List<Street> streets = JsonSerializer.Deserialize<List<Street>>(content, options);
                    return GetStreetsNameByCity(streets, city);
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

        #region GetServicesNameList
        private List<string> GetServicesNameList(List<Service> services)
        {
            List<string> serviceName = new List<string>();

            foreach (Service s in services)
            {
                serviceName.Add(s.Name);
            }

            return serviceName;
        }
        #endregion

        #region get workers list

        public async Task<List<Worker>> GetAvailableWorkers()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetAvailableWorkrs");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, //avoid reference loops!
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    List<Worker> lst = JsonSerializer.Deserialize<List<Worker>>(content, options);
                    return lst;
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

        #region job offer
        public async Task<JobOffer> JobOffer(JobOffer j)
        {
            try
            {

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    PropertyNameCaseInsensitive = true
                };

                string jsonObject = JsonSerializer.Serialize<JobOffer>(j, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/JobOffer", content);
                if (response.IsSuccessStatusCode)
                {

                    string str = await response.Content.ReadAsStringAsync();

                    JobOffer jobOffer = JsonSerializer.Deserialize<JobOffer>(str, options);
                    return jobOffer;
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

        #region send email

        public async Task<bool> SendMail(Worker w)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };

                string jsonObject = JsonSerializer.Serialize<Worker>(w, options);
                StringContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/SendMail", content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    bool ret = JsonSerializer.Deserialize<bool>(jsonContent, options);

                    return ret;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        #endregion

        #region get user job offer
        public async Task<List<JobOffer>> GetUserJobOffer(int id)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };

              

                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/GetUserJobOffer?id={id}");
                if (response.IsSuccessStatusCode)
                {
                    string jsonContent = await response.Content.ReadAsStringAsync();
                    var ret = JsonSerializer.Deserialize<List<JobOffer>>(jsonContent, options);

                    return ret;
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
    }


}
   
