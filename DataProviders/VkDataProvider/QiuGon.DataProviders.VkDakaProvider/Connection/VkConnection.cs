using VkNet;

namespace QiuGon.DataProviders.VkDakaProvider.Connection
{
    public class VkConnection
    {
        public VkApi VkApi { get; }

        public VkConnection(VkApi vkApi)
        {
            VkApi = vkApi;
        }
    }
}