namespace QiuGon.DataProviders.VkDakaProvider
{
    public class MessageStatistic
    {
        public long UserId { get; private set; }
        
        public string UserName { get; private set; }
        
        public long MessageCount { get; private set; }

        public MessageStatistic(long userId, string userName, long messageCount)
        {
            UserId = userId;
            UserName = userName;
            MessageCount = messageCount;
        }
    }
}