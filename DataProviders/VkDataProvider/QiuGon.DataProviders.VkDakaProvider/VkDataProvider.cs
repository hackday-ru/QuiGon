using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using QiuGon.DataProviders.VkDakaProvider.Connection;
using QuiGon.Infrastructure.DataProviders;
using QuiGon.Infrastructure.Entities;
using VkNet.Enums.Filters;
using VkNet.Exception;
using VkNet.Model.RequestParams;

namespace QiuGon.DataProviders.VkDakaProvider
{
    public class VkDataProvider : IDataProvider
    {
        public async Task<ReadOnlyCollection<long>> GetFriendsAsync()
        {
            var connection = await ConnectionFactory.Instance.CreateConnectionAsync();

            return connection.VkApi.Friends.GetOnline(new FriendsGetOnlineParams());
        }

        public async Task<IReadOnlyList<MessageStatistic>> GetMessageStatisticsAsync()
        {
            var connection = await ConnectionFactory.Instance.CreateConnectionAsync();


            var statistics = new List<MessageStatistic>();
            
            var users = connection.VkApi.Friends.Get(new FriendsGetParams
            {
                Fields = ProfileFields.All
            });

            foreach (var user in users)
            {
                var messagCount = await GetMessageCountAsync(connection, user.Id);
                statistics.Add(new MessageStatistic(user.Id,
                    String.Format("{0} {1}", user.LastName, user.FirstName), 
                    messagCount));
                Console.WriteLine("User {0}", user.Id);
            }

            return statistics;
        }

        private async Task<long> GetMessageCountAsync(VkConnection connection, long userId)
        {
            return await Task.Factory.StartNew(() =>
            {
                var canGet = true;
                long count = 0;
                long? lastMessageId = null;
                while (canGet)
                {

                    try
                    {
                        var messageObject = connection.VkApi.Messages.GetHistory(new MessagesGetHistoryParams
                        {
                            Count = 200,
                            UserId = userId,
                            Offset = count
                        });
                        var lastMessage = messageObject.Messages.Last();
                        if (lastMessage.Id == lastMessageId)
                        {
                            return count;
                        }
                        lastMessageId = lastMessage.Id;
                        count += messageObject.TotalCount;
                    }
                    catch (TooManyRequestsException)
                    {

                    }
                    catch (Exception)
                    {
                        canGet = false;
                    }
                }
                return count;
           
            });
        }

        public async Task<List<SubjectAction>> GetWallForUserAsync(string userId)
        {
            var connection = await ConnectionFactory.Instance.CreateConnectionAsync().ConfigureAwait(false);
            return await Task.Factory.StartNew(() =>
            {
                var wallPosts = connection.VkApi.Wall.Get(new WallGetParams
                {
                    Count = 100,
                    Domain = userId,
                    Extended = true
                });

                var subjectActions = new List<SubjectAction>();
                foreach (var wallPost in wallPosts.WallPosts)
                {
                    subjectActions.Add(
                        new SubjectAction(
                            -1, 
                            SubjectActionType.Post, 
                            new TextContent(wallPost.Text), 
                            -1, 
                            -1, 
                            null));
                }
                return subjectActions;
            }).ConfigureAwait(false);
        }
    }
}
