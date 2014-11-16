using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Consul.Models;
using System.Linq;

namespace Consul
{
    /// <summary>
    /// Consul client operations
    /// </summary>
    public class ConsulClient
    {
        /// <summary>
        /// The base uri for API requests
        /// </summary>
        private Uri _baseUri = null;

        /// <summary>
        /// Creates a consul client
        /// </summary>
        /// <param name="serverUri">The uri of the consul server to connect to.  This includes the entire http formatted url.  Example: http://localhost.com:8500 </param>
        public ConsulClient(string serverUri)
        {
            _baseUri = new Uri(serverUri);
        }

        /// <summary>
        /// Gets a config item for a given key, or null if one can't be found
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public async Task<ConfigItem> GetConfigItem(string configKey)
        {
            return (await ApiGet<List<ConfigItem>>(string.Format("/v1/kv/{0}", configKey))).FirstOrDefault();
        }

        #region Generic API helpers

        /// <summary>
        /// Generic 'get' API call.  Expects to be able to serialize 
        /// the results to the specified type
        /// </summary>
        /// <typeparam name="T">The specified results type</typeparam>
        /// <param name="apiAction">The API action.  Example: helper/account-details</param>
        /// <returns></returns>
        private async Task<T> ApiGet<T>(string apiAction)
        {
            //  Initialize the results to return
            T results = default(T);

            //  Create a new client
            using(var client = new HttpClient())
            {
                //  Set the options for the client
                client.BaseAddress = new Uri(string.Format("{0}://{1}/", _baseUri.Scheme, _baseUri.Authority));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //  Call the REST action
                HttpResponseMessage response = await client.GetAsync(apiAction);

                //  Check the response
                if(response.IsSuccessStatusCode)
                {
                    //  If we were successful, deserialize 
                    //  to the expected return type
                    results = await response.Content.ReadAsAsync<T>();
                }
            }

            //  Return the results
            return results;
        } 

        #endregion
    }
}
