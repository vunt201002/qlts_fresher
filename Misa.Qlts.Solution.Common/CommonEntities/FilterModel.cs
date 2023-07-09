namespace Misa.Qlts.Solution.Common.CommonEntites
{
    /// <summary>
    /// Lớp điều kiện lọc
    /// Gồm tên tài sản, phòng ban và loại tài sản.
    /// </summary>
    /// Created by: ntvu (17/05/2023)
    public class FilterModel
    {
        #region Properties
        public string FilterName { get; set; }              // tên tài sản
        public string FilterDepartment { get; set; }        // tên phòng ban
        public string FilterCategory { get; set; }          // tên loại tài sản 
        #endregion
    }
}
