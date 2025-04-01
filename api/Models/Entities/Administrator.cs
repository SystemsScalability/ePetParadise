namespace api.Models.Entities
{
    public class Administrator
    {
        public int AdministratorID { get; set; }
        public string Name { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string AdminType { get; set; }= string.Empty;
        public string Password { get; set; }= string.Empty;
        public string RegisterDate { get; set; }= string.Empty;
        public int Nit { get; set; }
        public int PhoneNumber { get; set; }
    }
}