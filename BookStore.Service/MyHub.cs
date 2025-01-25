using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class MyHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message);
        }
    }
}
