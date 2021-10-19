using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreMVCBlobStorage.Models
{
    public class StorageConfig
    {
        public string AccountName { get; set; }
        public string ImageContainer { get; set; }
        public string AccountKey { get; set; }
    }
}
