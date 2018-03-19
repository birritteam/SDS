using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Threading.Tasks;

namespace SDS_SanadDistributedSystem.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        public static Dictionary<string, string> dicConns = new Dictionary<string, string>();
       

        public override Task OnConnected()
        {
            dicConns.Add(Context.ConnectionId, Context.User.Identity.Name);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            try
            {
                dicConns.Add(Context.ConnectionId, Context.User.Identity.Name);
            }
            catch { }
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            dicConns.Remove(Context.ConnectionId);
            return base.OnDisconnected(stopCalled);
        }

        public static void sendnotify(string username, string message)
        {
            try
            {
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                foreach (var item in dicConns)
                    if (item.Value == username)
                        notificationHub.Clients.Client(item.Key).notify(message);
                //notificationHub.Clients.User(username).notify(message);
            }
            catch
            {

            }

        }
    }
}