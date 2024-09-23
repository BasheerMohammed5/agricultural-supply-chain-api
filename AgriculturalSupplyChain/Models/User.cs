namespace AgriculturalSupplyChain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Role to define user type (e.g., Admin, Farmer, etc.)

        // Method to update specific fields
        public void UpdateUser(string userName = null, string email = null, string role = null)
        {
            if (!string.IsNullOrEmpty(userName))
                UserName = userName;

            if (!string.IsNullOrEmpty(email))
                Email = email;

            if (!string.IsNullOrEmpty(role))
                Role = role;
        }
    }
}
