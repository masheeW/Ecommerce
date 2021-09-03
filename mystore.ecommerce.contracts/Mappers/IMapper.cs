namespace mystore.ecommerce.contracts.mappers
{
    public interface IMapper<in TInput, out TOutput>
    {
        /// <summary>
        /// Maps the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        TOutput Map(TInput input);
    }
}