namespace NCourseWork.Domain.Client
{
    using NCourseWork.Domain.Status;
    using System;

    public class Client
    {
        public Client()
        {

        }

        public Client(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; } = default;
        public ClientInfo Info { get; set; } = null!;
        public Status Status { get; set; } = null!;
        public int CountOfOrders { get; set; }
    }
}
