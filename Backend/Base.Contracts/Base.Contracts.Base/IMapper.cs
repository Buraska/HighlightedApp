namespace Base.Contracts.Base;

public interface IMapper<TOut, TIn>
{
    public TOut? Map(TIn? entity);

    public TIn? Map(TOut? entity);
}