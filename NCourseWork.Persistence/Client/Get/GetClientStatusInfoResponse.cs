namespace NCourseWork.Persistence.Client.Get
{
    using NCourseWork.Domain.Status;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class GetClientStatusInfoResponse
    {
        public Status? Status { get; set; }
        public int CountOfOrders { get; set; }
    }
}
