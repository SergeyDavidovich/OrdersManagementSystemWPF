namespace Infrastructure.Prism
{
    /// <summary>
    /// Реализуйте данный интерфейс в code-behind View, если навигация на него(View) должна порождать
    /// новый(вложенный) RegionManager
    /// </summary>
    public interface ICreateRegionManagerScope
    {
        /// <summary>
        /// Свойсвто, которое необходимо определить в реализующем интерфейс классе
        /// True - вложенный RegionManager будет создан 
        /// False - вложенный RegionManager не будет создан
        /// </summary>
        bool CreateRegionManagerScope { get; }
    }
}
