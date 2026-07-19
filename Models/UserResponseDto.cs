namespace Backend.Models
{
	public class UserResponseDto
	{
		public int Id { get; set; }
		public string Name { get; set; }   // maps from FullName
		public string Email { get; set; }
		public string Role { get; set; }
	}
}
