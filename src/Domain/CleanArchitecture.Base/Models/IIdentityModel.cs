namespace CleanArchitecture.Base.Models
{
    public interface IIdentityModel<TIdentity>
    {
        TIdentity Id { get; }
    }
}