using NCourseWork.Domain.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Client.Get
{
    public class ClientFullInfo
    {
        public Guid Id { get; set; }
        public ClientInfo ClientInfo { get; set; } = null!;
        public string StatusName { get; set; } = null!;
        public Guid StatusId { get; set; }
        public double DiscountPercentage { get; set; }
    }
}
