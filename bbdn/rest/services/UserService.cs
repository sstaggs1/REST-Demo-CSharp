using System;
using System.Text;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REST_Demo_CSharp.bbdn.rest.helpers;

namespace REST_Demo_CSharp.bbdn.rest.services
{
    public class UserService : IRestService<User>, IDisposable
    {
        HttpClient client;


        public UserService(Token token)
        {
            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }

        public async Task<User> CreateObject(User newUser)
        {
            User user = new User();
            var uri = new Uri(Constants.HOSTNAME + Constants.USER_PATH);

            try
            {
                var json = JsonConvert.SerializeObject(newUser);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                    Debug.WriteLine(@"				User successfully created.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return user;
        }

        public async Task<User> ReadObject()
        {
            User user = new User();

            var uri = new Uri(Constants.HOSTNAME + Constants.USER_PATH + "externalId:" + Constants.USER_ID);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return user;
        }


        public async Task<User> UpdateObject(User updateUser)
        {
            User user = new User();

            try
            {
                var json = JsonConvert.SerializeObject(updateUser);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await HttpClientExtensions.PatchAsync(client, Constants.HOSTNAME + Constants.USER_PATH + "externalId:" + Constants.USER_ID, body);


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				User successfully updated.");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        user = JsonConvert.DeserializeObject<User>(content);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return user;
        }

        public async Task<User> DeleteObject()
        {
            User user = new User();
            var uri = new Uri(Constants.HOSTNAME + Constants.USER_PATH + "externalId:" + Constants.USER_ID);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				User successfully deleted.");
                    var content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return (user);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                client.Dispose();

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~UserService()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
