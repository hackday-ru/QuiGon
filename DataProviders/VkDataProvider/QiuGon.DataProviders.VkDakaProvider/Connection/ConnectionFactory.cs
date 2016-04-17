using System;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;

namespace QiuGon.DataProviders.VkDakaProvider.Connection
{
    public class ConnectionFactory
    {
        private static ConnectionFactory _instance;
        public static ConnectionFactory Instance
        {
            get { return _instance ?? (_instance = new ConnectionFactory()); }
        }

        private ConnectionFactory()
        {
        }

        public async Task<VkConnection> CreateConnectionAsync()
        {
            try
            {
                var scope = Settings.All;
                
                var vkApi = new VkApi();
                await vkApi.AuthorizeAsync(new ApiAuthParams
                {
                    ApplicationId = VkAuthorizeParameters.AppId,
                    Login = VkAuthorizeParameters.Login,
                    Password = VkAuthorizeParameters.Password,
                    Settings = scope
                }).ConfigureAwait(false);
                vkApi.RequestsPerSecond = 15;
                return new VkConnection(vkApi);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}