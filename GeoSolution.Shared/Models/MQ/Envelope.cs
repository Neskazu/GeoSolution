using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSolution.Shared.Models.MQ
{
    public class Envelope<T>
    {
        public string Type { get; set; }

        public T Payload { get; set; }
    }
}
