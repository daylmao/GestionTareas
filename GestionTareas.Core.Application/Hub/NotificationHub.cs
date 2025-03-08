using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionTareas.Core.Application.Hub
{
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task Notificate(string message)
        {
            await Clients.All.SendAsync("SendNotification",message);
        }
    }
}
