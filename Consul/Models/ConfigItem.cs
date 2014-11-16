using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Consul.Models
{
    /// <summary>
    /// A configuration (key/value) item.  More information here: https://www.consul.io/docs/agent/http.html#kv
    /// </summary>
    [DataContract]
    public class ConfigItem
    {
        /// <summary>
        /// Internal index value that represents when the entry was created
        /// </summary>
        [DataMember]
        public int CreateIndex { get; set; }

        /// <summary>
        /// Internal index that modified this key. This index 
        /// corresponds to the X-Consul-Index header value that is returned.
        /// A blocking query can be used to wait for a value to change. 
        /// If recurse is used, the X-Consul-Index corresponds to the 
        /// latest ModifyIndex and so a blocking query waits until 
        /// any of the listed keys are updated.
        /// </summary>
        [DataMember]
        public int ModifyIndex { get; set; }

        /// <summary>
        /// The last index of a successful lock acquisition. 
        /// If the lock is held, the Session key provides 
        /// the session that owns the lock.
        /// </summary>
        [DataMember]
        public int LockIndex { get; set; }
        
        /// <summary>
        /// The full path of the entry
        /// </summary>
        [DataMember]
        public string Key { get; set; }

        /// <summary>
        /// An opaque unsigned integer that can be attached to each entry. 
        /// The use of this is left totally to the user.
        /// </summary>
        [DataMember]
        public int Flags { get; set; }

        /// <summary>
        /// The config value for the key
        /// </summary>
        public string Value 
        {
            get 
            {
                string retval = string.Empty;
                if(!string.IsNullOrEmpty(this.Base64Value))
                {
                    byte[] base64EncodedBytes = Convert.FromBase64String(this.Base64Value);
                    retval = Encoding.UTF8.GetString(base64EncodedBytes);
                }
                
                return retval;
            }
            set
            {
                string retval = string.Empty;
                if(!string.IsNullOrEmpty(value))
                {
                    var plainTextBytes = Encoding.UTF8.GetBytes(value);
                    retval = Convert.ToBase64String(plainTextBytes);
                }

                this.Base64Value = retval;
            }
        }
        
        /// <summary>
        /// Provides the session that owns the lock (if a lock exists)
        /// </summary>
        [DataMember]
        public string Session { get; set; }

        /// <summary>
        /// The raw base64 encoded value from Consul
        /// </summary>
        [DataMember(Name="Value")]
        public string Base64Value { get; set; }
    }
}
