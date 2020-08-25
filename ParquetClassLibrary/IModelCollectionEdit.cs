namespace ParquetClassLibrary
{
    /// <summary>
    /// Facilitates updating elements of a <see cref="ModelCollection"/> from design tools
    /// while maintaining a read-only facade during play.
    /// </summary>
    /// <typeparam name="TModel">The type collected, typically a concrete decendent of <see cref="Model"/>.</typeparam>
    public interface IModelCollectionEdit<TModel>
        where TModel : Model
    {
        /// <summary>
        /// Adds the given <typeparamref name="TModel"/> to the collection.
        /// </summary>
        /// <param name="inModel">A valid, defined <typeparamref name="TModel"/> contianed in this collection.</param>
        public void Add(TModel inModel);

        /// <summary>
        /// Empties the entire collection.
        /// </summary>
        public void Clear();

        /// <summary>
        /// Replaces a contained <typeparamref name="TModel"/> with the given <typeparamref name="TModel"/> whose
        /// <see cref="ModelID"/> is identicle to that of the model being replaced.
        /// </summary>
        /// <param name="inModel">A valid, defined <typeparamref name="TModel"/> contianed in this collection.</param>
        public void Replace(TModel inModel);
    }
}
