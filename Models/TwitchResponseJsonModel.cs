using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StreamerCheck.Models
{
    public class TwitchResponseJsonModel
    {
        public List<TwtichStreamInfoJsonModel>? data { get; set; }
        public TwitchPageJsonModel? pagination { get; set; }
    }
}