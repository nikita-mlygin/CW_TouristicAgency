using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Client.Update
{
    public class UpdateClientCommand : IRequest
    {
        public Guid Id { get; set; }
        public string UFirstName { get; set; } = null!;
        public string ULastName { get; set; } = null!;
        public string UMiddleName { get; set; } = null!;
        public string UAddress { get; set; } = null!;
        public string UPhone { get; set; } = null!;
    }
}
