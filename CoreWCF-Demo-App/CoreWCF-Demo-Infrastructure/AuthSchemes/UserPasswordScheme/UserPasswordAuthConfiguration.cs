namespace CoreWCF_Demo_Infrastructure.AuthSchemes.UserPasswordScheme
{
    public record UserPasswordAuthConfiguration
    {
        public List<UserAuthValue> Users { get; init; }
    }

    public record UserAuthValue
    {
        public required string Username { get; set; }
        public required string Password { get; init; }
    }
}
