using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using REST_Demo_CSharp.bbdn.rest.helpers;

namespace REST_Demo_CSharp.bbdn.rest.services
{
    public class MembershipService : IRestService<Membership>, IDisposable
    {
        HttpClient client;


        public MembershipService(Token token)
        {
            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }


        public async Task<Membership> CreateObject(Membership newMembership)
        {
            Membership membership = new Membership();
            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + Constants.COURSE_ID + "users/externalId:" + Constants.USER_ID);

            try
            {
                var json = JsonConvert.SerializeObject(membership);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    membership = JsonConvert.DeserializeObject<Membership>(content);
                    Debug.WriteLine(@"				Membership successfully created.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return membership;
        }


        public async Task<Membership> ReadObject()
        {
            Membership membership = new Membership();

            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + Constants.COURSE_ID + "users/externalId:" + Constants.USER_ID);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    membership = JsonConvert.DeserializeObject<Membership>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return membership;
        }


        public async Task<Membership> UpdateObject(Membership updateMembership)
        {
            Membership membership = new Membership();

            try
            {
                var json = JsonConvert.SerializeObject(updateMembership);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await HttpClientExtensions.PatchAsync(client, Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + Constants.COURSE_ID + "users/externalId:" + Constants.USER_ID, body);


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Membership successfully updated.");
                    var content = await response.Content.ReadAsStringAsync();
                    membership = JsonConvert.DeserializeObject<Membership>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return (membership);
        }

        public async Task<Membership> DeleteObject()
        {
            Membership membership = new Membership();
            var uri = new Uri(Constants.HOSTNAME + Constants.COURSE_PATH + "/externalId:" + Constants.COURSE_ID + "users/externalId:" + Constants.USER_ID);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Membership successfully deleted.");
                    var content = await response.Content.ReadAsStringAsync();
                    membership = JsonConvert.DeserializeObject<Membership>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return (membership);
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
        ~MembershipService()
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

