namespace Misa.Qlts.Solution.Common.CommonEntites
{
    /// <summary>
    /// Lớp phân trang,
    /// gồm số trang và số bản ghi mỗi trang.
    /// </summary>
    /// Created by: ntvu (15/05/2023)
    public class LimitPage
    {
        public LimitPage()
        {
            
        }
        #region Properties
        public int pageNumber { get; set; }         // số trang
        public int pageSize { get; set; }           // số bản ghi mỗi trang 
        #endregion
    }
}
