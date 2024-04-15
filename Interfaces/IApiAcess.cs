using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamerCheck.Interfaces
{
    public interface IApiAcess
    {
        public string streamPlataform { get; set; }
        public string onlineStreamerFile { get; set; }
        public Task<HashSet<string>> GetStreamersOnline();

    }
}