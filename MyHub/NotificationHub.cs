using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace E_proc.MyHub
{
    public class NotificationHub : Hub
    {
        public async Task askServer(string someTextFromClient)
        {
            string tempString;

            if (someTextFromClient == "hey")
            {
                tempString = "message was 'hey'";
            }
            else
            {
                tempString = "message was something else";
            }

            await Clients.All.SendAsync("Send", tempString);
        }


        public async Task joinInstituteNotificationCenter()
        {

           await Groups.AddToGroupAsync(Context.ConnectionId, "instituteNotificationCenter");

        }
        public async Task joinAskInfoChat(string askInfoId)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, "AskInfoChat"+askInfoId);

        }
        public async Task joinAskInfoNotificationCitizen()
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, "AskInfoNotificationCitizen");

        }
        public async Task joinCitizenNotificationCenter()
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, "citizenNotificationCenter");

        }

        public async Task joinAskInfoNotificationInstitute()
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, "askInfoNotificationInstitute");

        }
    }
}
