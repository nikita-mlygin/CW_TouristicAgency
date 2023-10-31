namespace NCourseWork.Persistence.Client.Get
{
    internal class GetClientByIdResponse
    {
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
