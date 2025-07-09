using DiabetProject.BLL.Data.Dto;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetProject.BLL.Data.Hubs
{
    public class DiagnosisHub : Hub
    {
        public async Task BroadcastChartUpdate(List<DiagnosisDto> result)
        {
            await Clients.All.SendAsync("ReceiveChartUpdate", result);
        }
    }
}
