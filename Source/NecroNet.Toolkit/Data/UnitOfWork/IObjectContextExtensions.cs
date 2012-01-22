namespace NecroNet.Toolkit.Data
{
    public static class IObjectContextExtensions
    {
        /// <summary>
        /// Returns actual unwrapped object context.
        /// </summary>
        /// <typeparam name="TObjectContext">Type of object context to return</typeparam>
        /// <param name="context">The wrapped object context.</param>
        public static TObjectContext AsActual<TObjectContext>(this IObjectContext context)
        {
            return (TObjectContext) context;
        }
    }
}