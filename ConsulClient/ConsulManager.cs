using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Consul.Models;
using ServiceStack.Text;

namespace ConsulClient
{
    public class ConsulManager
    {
        /// <summary>
        /// The base uri for API requests
        /// </summary>
        private Uri _baseUri = null;

        /// <summary>
        /// Creates a consul client
        /// </summary>
        /// <param name="serverUri">The uri of the consul server to connect to.  
        /// This includes the entire http formatted url.  
        /// Example: http://localhost.com:8500 </param>
        public ConsulManager(string serverUri)
        {
            _baseUri = new Uri(serverUri);
        }

        /// <summary>
        /// Gets a config item for a given key, or null if one can't be found
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns></returns>
        public ConfigItem GetConfigItem(string configKey)
        {
            ConfigItem retval = null;

            //  Call the ApiGet method
            var result = ApiGet<List<ConfigItem>>(string.Format("v1/kv/{0}", configKey));
            
            //  If the result isn't null, get the first item returned
            if(result != null)
                retval = result.FirstOrDefault();

            return retval;
        }

        /// <summary>
        /// Generic 'get' API call.  Expects to be able to serialize 
        /// the results to the specified type
        /// </summary>
        /// <typeparam name="T">The specified results type</typeparam>
        /// <param name="apiAction">The API action.  Example: /v1/kv/keyname</param>
        /// <param name="query">The querystring to pass (or blank, if none).  Example: ?test=value</param>
        /// <returns></returns>
        private T ApiGet<T>(string apiAction, string query = "")
        {
            //  Initialize the results to return
            T results = default(T);

            //  Next, construct the full url based on the passed apiAction:
            string fullUrl = new UriBuilder(_baseUri.Scheme, _baseUri.Host, _baseUri.Port, apiAction, query).ToString();

            try
            {
                //  Call the url and get the results back
                //  (we do this in 2 phases for easier debugging)
                var resultString = fullUrl.GetJsonFromUrl();
                results = resultString.Trim().FromJson<T>();
            }
            catch(WebException ex)
            {
                //  If it's a 404 exception, eat it.
                //  If it's not a 404 exception, throw it
                if(!ex.IsNotFound())
                    throw;
            }

            //  Return the results
            return results;
        }
    }
}
