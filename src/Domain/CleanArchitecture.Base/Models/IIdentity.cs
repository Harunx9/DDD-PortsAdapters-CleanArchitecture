namespace CleanArchitecture.Base.Models
{
    public interface IIdentity<TIdentity>
    {
        TIdentity Id { get; }
    }
}