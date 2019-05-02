using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string BlobStorageUrl { get; set; }

        public bool UseKeyVault { get; set; }
        public bool UseBlob { get; set; }

        public VaultSettings Vault { get; set; }
    }

    public class VaultSettings
    {
        public string Name { get; set; }
        public string ClientId { get; set; }
    }
}
