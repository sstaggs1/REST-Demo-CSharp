using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using REST_Demo_CSharp.bbdn.rest.helpers;

namespace REST_Demo_CSharp
{
    public class DatasourceService : IRestService<Datasource>, IDisposable
    {
        HttpClient client;


        public DatasourceService(Token token)
        {
            client = new HttpClient
            {
                MaxResponseContentBufferSize = 256000
            };
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
        }


        public async Task<Datasource> CreateObject(Datasource dataSource)
        {
            Datasource datasource = new Datasource();
            var uri = new Uri(Constants.HOSTNAME + Constants.DATASOURCE_PATH);

            try
            {
                var json = JsonConvert.SerializeObject(dataSource);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(uri, body);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    datasource = JsonConvert.DeserializeObject<Datasource>(content);
                    Debug.WriteLine(@"				Datasource successfully created.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return datasource;
        }


        public async Task<Datasource> ReadObject()
        {
            Datasource datasource = new Datasource();

            var uri = new Uri(Constants.HOSTNAME + Constants.DATASOURCE_PATH + "/externalId:" + Constants.DATASOURCE_ID);

            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    datasource = JsonConvert.DeserializeObject<Datasource>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return datasource;
        }

        public async Task<Datasource> UpdateObject(Datasource updateDataSource)
        {
            Datasource datasource = new Datasource();

            try
            {
                var json = JsonConvert.SerializeObject(updateDataSource);
                var body = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await HttpClientExtensions.PatchAsync(client, Constants.HOSTNAME + Constants.DATASOURCE_PATH + "/externalId:" + Constants.DATASOURCE_ID, body);


                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Datasource successfully updated.");
                    var content = await response.Content.ReadAsStringAsync();
                    datasource = JsonConvert.DeserializeObject<Datasource>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return (datasource);
        }

        public async Task<Datasource> DeleteObject()
        {
            Datasource datasource = new Datasource();
            var uri = new Uri(Constants.HOSTNAME + Constants.DATASOURCE_PATH + "/externalId:" + Constants.DATASOURCE_ID);

            try
            {
                var response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"				Datasource successfully deleted.");
                    var content = await response.Content.ReadAsStringAsync();
                    datasource = JsonConvert.DeserializeObject<Datasource>(content);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            return (datasource);
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
        ~DatasourceService()
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