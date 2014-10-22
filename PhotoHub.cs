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
            //it's not recommended .
            PathPool.Add(GetBinary(Directory.GetFiles(path).FirstOrDefault()), Context.ConnectionId);
            var timer = new Timer(3000);
            timer.Elapsed += (s, e) =>
            {
                GlobalHost.ConnectionManager.GetHubContext<PhotoHub>().Clients.All.SetImg(PathPool.Lst.FirstOrDefault());
            };
            timer.Enabled = true;
        }

        
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            PathPool.Lst.Remove(PathPool.Lst.FirstOrDefault(o => o.ConnectionId.Equals(Context.ConnectionId)));
            return base.OnDisconnected(stopCalled);
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
        private static List<Photo> _lst;
        public static List<Photo> Lst
        {
            get { return _lst; }
        }
        public static void Add(string path,string connectionId)
        {
            if (_lst == null)
            {
                _lst = new List<Photo>();
            }
            _lst.Add(new Photo()
            {
                Path = path,
                ConnectionId = connectionId,
            });
        }
    }

    public class Photo
    {
        public string Path { get; set; }
        public string ConnectionId { get; set; }
    }
}