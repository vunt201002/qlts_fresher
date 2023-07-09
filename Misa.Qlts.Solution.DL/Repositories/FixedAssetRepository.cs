using ClosedXML.Excel;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using Misa.Qlts.Solution.Common.CommonEntites;
using Misa.Qlts.Solution.Common.CommonEntities;
using Misa.Qlts.Solution.DL.Base;
using Misa.Qlts.Solution.DL.Context;
using Misa.Qlts.Solution.DL.Contracts;
using Misa.Qlts.Solution.DL.Entities;
using System.Data;
using System.Text.Json.Serialization;

namespace Misa.Qlts.Solution.DL.Repositories
{
    public class FixedAssetRepository : BaseRepository<FixedAsset>, IFixedAssetRepository
    {
        #region Constructor
        public FixedAssetRepository(DapperContext context) : base(context)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy tất cả tài sản.
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <returns>IEnumerable<FixedAsset></returns>
        /// Created by: ntvu (12/05/2023)
        public async Task<IEnumerable<FixedAsset>> GetFixedAssetsAsync(LimitPage limit)
        {
            var procedureName = "sp_get_fixed_assets";

            using (var connection = _context.CreateConnection())
            {
                var fixedAssets = await connection.QueryAsync<FixedAsset>(
                    procedureName,
                    commandType: CommandType.StoredProcedure
                );

                return fixedAssets.ToList();
            }
        }

        /// <summary>
        /// Thêm một tài sản.
        /// </summary>
        /// <param name="fixedAsset">Tài sản cần thêm</param>
        /// <returns></returns>
        /// Created by: ntvu (15/05/2023)
        public override async Task<int> AddAsync(FixedAsset fixedAsset)
        {
            var procedureName = "sp_add_fixed_asset";

            fixedAsset.fixed_asset_id = Guid.NewGuid();

            using (var connection = _context.CreateConnection())
            {
                int res = await connection.ExecuteAsync(
                    procedureName,
                    fixedAsset,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

        /// <summary>
        /// Chỉnh sửa tài sản.
        /// </summary>
        /// <param name="fixedAssetId">Id của tài sản cần chỉnh sửa</param>
        /// <param name="fixedAsset">Thông tin chỉnh sửa</param>
        /// <returns></returns>
        /// Created by: ntvu (15/05/2023)
        public override async Task<int> UpdateAsync(Guid fixedAssetId, FixedAsset fixedAsset)
        {
            var procedureName = "sp_update_fixed_asset";

            fixedAsset.fixed_asset_id = fixedAssetId;

            using (var connection = _context.CreateConnection())
            {
                int res = await connection.ExecuteAsync(
                    procedureName,
                    fixedAsset,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

        /// <summary>
        /// Lọc và phân trang tài sản.
        /// </summary>
        /// <param name="limit">Số trang và số bản ghi mỗi trang</param>
        /// <param name="filter">Điều kiện lọc</param>
        /// <returns>Các tài sản được lọc</returns>
        /// Created by: ntvu (15/05/2023)
        public async Task<IEnumerable<FixedAsset>> FilteringAndPagingAsync(LimitPage limit, FilterModel filter)
        {
            var procedureName = "sp_filtering_and_paging";

            using (var connection = _context.CreateConnection())
            {
                var fixedAssets = await connection.QueryAsync<FixedAsset>(
                    procedureName,
                    new
                    {
                        limit.pageNumber,
                        limit.pageSize,
                        filter.FilterName,
                        filter.FilterDepartment,
                        filter.FilterCategory
                    },
                    commandType: CommandType.StoredProcedure
                );

                return fixedAssets;
            }
        }

        /// <summary>
        /// Lấy id của tài sản.
        /// </summary>
        /// <param name="fixedAssetCode">mã tài sản</param>
        /// <returns>Guid</returns>
        /// Created by: ntvu (23/05/2023)
        public async Task<Guid> GetFixedAssetIdAsync(string fixedAssetCode)
        {
            var procedureName = "sp_get_fixed_asset_id";

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("fixed_asset_code", fixedAssetCode);

            using (var connection = _context.CreateConnection())
            {
                var fixedAssetId = await connection.QuerySingleOrDefaultAsync<Guid>(
                    procedureName,
                    dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return fixedAssetId;
            }
        }

        /// <summary>
        /// Hàm check trùng mã
        /// </summary>
        /// <param name="fixedAssetId">Id tài sản</param>
        /// <param name="fixedAssetCode">Mã tài sản cần check</param>
        /// <returns>int</returns>
        /// created by: ntvu (10/06/2023)
        public async Task<int> CheckDuplicateCodeAsync(Guid fixedAssetId, string fixedAssetCode)
        {
            var procedureName = "sp_check_duplicate_fixed_asset_code";

            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("fixed_asset_id", fixedAssetId);
            dynamicParams.Add("fixed_asset_code", fixedAssetCode);

            using (var connections = _context.CreateConnection())
            {
                int res = await connections.QuerySingleOrDefaultAsync<int>(
                    procedureName,
                    dynamicParams,
                    commandType: CommandType.StoredProcedure
                );

                return res;
            }
        }

        /// <summary>
        /// Hàm tạo mã mới
        /// </summary>
        /// <returns>string</returns>
        /// created by: ntvu (10/06/2023)
        public async Task<string> GenerateNewCodeAsync()
        {
            var procedureName = "sp_generate_new_code";

            using (var connections = _context.CreateConnection())
            {
                string newCode = await connections.QuerySingleOrDefaultAsync<string>(
                    procedureName,
                    commandType: CommandType.StoredProcedure
                );

                return newCode;
            }
        }

        /// <summary>
        /// Hàm lấy summary
        /// </summary>
        /// <returns>Task<Summary></returns>
        /// created by: ntvu (06/06/2023)
        public async Task<Summary> GetSummary()
        {
            var procedureName = "sp_summary";

            using (var connections = _context.CreateConnection())
            {
                var ResSummary = await connections.QuerySingleOrDefaultAsync<Summary>(
                    procedureName,
                    commandType: CommandType.StoredProcedure
                );

                return ResSummary;
            }
        }

        /// <summary>
        /// Hàm export
        /// </summary>
        /// <returns>Task<FileResult></returns>
        /// created by: ntvu (12/06/2023)
        public async Task<FileContentResult> ExportFixedAssetToExcel()
        {
            var fileName = "FixedAsset.xlsx";

            var fixedAssets = await GetFixedAssetsAsync(new LimitPage());

            var fileRes = GenerateExcel(fileName, fixedAssets);

            return fileRes;
        }

        /// <summary>
        /// Hàm tạo file excel để export
        /// </summary>
        /// <param name="fileName">Tên file</param>
        /// <param name="fixedAssets">Danh sách tài sản</param>
        /// <returns>FileStreamResult</returns>
        /// created by: ntvu (15/06/2023)
        private static FileContentResult GenerateExcel(string fileName, IEnumerable<FixedAsset> fixedAssets)
        {
            DataTable dataTable = new("fixed_asset");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("fixed_asset_code"),
                new DataColumn("fixed_asset_name"),
                new DataColumn("fixed_asset_category_name"),
                new DataColumn("department_name"),
                new DataColumn("quantity"),
                new DataColumn("cost"),
                new DataColumn("accumulated_depreciation"),
                new DataColumn("residual_value")
            });

            foreach (var fixedAsset in fixedAssets)
            {
                dataTable.Rows.Add(
                    fixedAsset.fixed_asset_code,
                    fixedAsset.fixed_asset_name,
                    fixedAsset.fixed_asset_category_name,
                    fixedAsset.department_name,
                    fixedAsset.quantity,
                    fixedAsset.cost,
                    fixedAsset.accumulated_depreciation,
                    fixedAsset.residual_value
                );
            }

            using (XLWorkbook wb = new())
            {
                wb.Worksheets.Add(dataTable, "fixed_asset");
                using (MemoryStream stream = new())
                {
                    wb.SaveAs(stream);

                    var content = stream.ToArray();

                    return new FileContentResult(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                    )
                    {
                        FileDownloadName = fileName
                    };
                }
            }
        }

        /// <summary>
        /// hàm import file excel, lấy dữ liệu trong
        /// file rồi map thành object, sau đó trả
        /// về để fe đổ ra ui. UI không thể lấy full
        /// path để truyền lên, cần sửa để import theo
        /// cách khác. => TODO
        /// </summary>
        /// <returns>Task<IEnumerable<FixedAssetImport>></returns>
        /// created by: ntvu (20/06/2023)
        public List<FixedAssetImport> ImportExcel<FixedAssetImport>(ImportModel importModel)
        {
            List<FixedAssetImport> fixedAssetImports = new();

            Type typeObject = typeof(FixedAssetImport);

            using(XLWorkbook workbook = new(importModel.ExcelFilePath))
            {
                var worksheet = workbook.Worksheets.FirstOrDefault(w => w.Name == importModel.SheetName);
                var properties = typeObject.GetProperties();

                var columns = worksheet.FirstRow().Cells().Select(
                    (v, i) => new
                    {
                        v.Value,
                        Index = i + 1
                    }
                );

                foreach(IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    FixedAssetImport obj = (FixedAssetImport)Activator.CreateInstance(typeObject);

                    foreach(var prop in properties)
                    {
                        int colIndex = columns.SingleOrDefault(
                            c => c.Value.ToString() == prop.Name.ToString()
                        ).Index;

                        var cellValue = row.Cell(colIndex).Value;
                        object val = null;

                        var type = prop.PropertyType;

                        // chuyển type của cellValue sang các
                        // kiểu dữ liệu tương ứng. Cần update
                        // chỗ này vì đang chuyển theo kiểu
                        // mò, sửa cho dynamic hơn. => TODO
                        if (type == typeof(string))
                        {
                            val = cellValue.ToString();
                        } else if (type == typeof(int))
                        {
                            int intValue;
                            if (int.TryParse(cellValue.ToString(), out intValue))
                            {
                                val = intValue;
                            }
                        } else if (type == typeof(decimal))
                        {
                            decimal decimalValue;
                            if (decimal.TryParse(cellValue.ToString(), out decimalValue))
                            {
                                val = decimalValue;
                            }
                        }

                        prop.SetValue(obj, val);

                    }

                    fixedAssetImports.Add(obj);
                }
            }

            return fixedAssetImports;
        }

        #endregion
    }
}
