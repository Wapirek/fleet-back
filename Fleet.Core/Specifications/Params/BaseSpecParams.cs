namespace Fleet.Core.Specifications.Params
{
    public class BaseSpecParams
    {
        #region Protected Members

        protected const int MaxPageSize = 30;
        protected int _pageSize = 10;

        #endregion
        
        #region Public Properties
        
        public int PageIndex { get; set; } = 1;
        
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }        
        
        #endregion

    }
}