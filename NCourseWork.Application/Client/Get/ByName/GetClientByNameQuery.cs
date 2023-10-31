using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Client.Get.ByName
{
    public class GetClientByNameQuery : IRequest<IEnumerable<ClientListItemInfo>>
    {
        public GetClientByNameQuery()
        {
        }

        public GetClientByNameQuery(string? firstName, string? lastName, string? middleName)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
    }
}
