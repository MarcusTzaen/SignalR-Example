using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Web;

namespace WebApplication1
{
    public class PhotoHub : Hub
    {
        public void Init(string path)
        {
            // Call the broadcastMessage method to update clients.
            //Clients.All.broadcastMessage(name, message);
            PathPool.Add(GetBinary(Directory.GetFiles(path).FirstOrDefault()));
            var timer = new Timer(3000);
            timer.Elapsed += (s, e) =>
            {
                GlobalHost.ConnectionManager.GetHubContext<PhotoHub>().Clients.All.SetImg(PathPool.Lst.FirstOrDefault());
            };
            timer.Enabled = true;
        }


        private string GetBinary(string fileName)
        {
            using (var ms = new MemoryStream())
            {
                var bytes = File.ReadAllBytes(fileName);
                return "data:image/gif;base64," + Convert.ToBase64String(bytes);
            }
        }
    }

    public static class PathPool
    {
        private static List<string> _lst;
        public static List<string> Lst
        {
            get { return _lst; }
        }
        public static void Add(string path)
        {
            if (_lst == null)
            {
                _lst = new List<string>();
            }
            _lst.Add(path);
        }
    }
}