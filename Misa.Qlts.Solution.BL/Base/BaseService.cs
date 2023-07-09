using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Misa.Qlts.Solution.BL.DepartmentService.DepartmentDtos;
using Misa.Qlts.Solution.BL.FixedAssetService.FixedAssetServiceDtos;
using Misa.Qlts.Solution.Common.Exceptions;
using Misa.Qlts.Solution.DL.Base;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.Base
{
    public abstract class BaseService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IBaseService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
    {
        #region Properties
        protected readonly IBaseRepository<TEntity> _baseRepository;
        protected readonly IMapper _mapper;
        #endregion

        #region Constructors
        public BaseService(
            IBaseRepository<TEntity> baseRepository,
            IMapper mapper
        )
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hàm gọi tới repo, thêm một bản ghi
        /// </summary>
        /// <param name="TEntityCreateDto">Giá trị bản ghi</param>
        /// <returns></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<int> AddAsync(TEntityCreateDto entityCreateDto)
        {
            var entity = _mapper.Map<TEntity>(entityCreateDto);

            // validate dữ liệu trước khi truyền xuống cho DL.



            int res = await _baseRepository.AddAsync(entity);

            if (res == 0)
            {
                throw new InternalException();
            }

            return res;

        }


        /// <summary>
        /// Hàm gọi tới repo, xóa bản ghi
        /// </summary>
        /// <param name="Ids">Danh sách id của các cần xóa</param>
        /// <returns></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<int> DeleteAsync(object Ids)
        {
            int res = await _baseRepository.DeleteAsync(Ids);

            if (res == 0)
            {
                throw new InternalException();
            }

            return res;
        }


        /// <summary>
        /// Hàm gọi tới repo, lấy một bản ghi
        /// </summary>
        /// <param name="Id">Id của bản ghi</param>
        /// <returns>TEntityDto</returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<TEntityDto> GetAsync(Guid Id)
        {
            var entityDto = await _baseRepository.GetAsync(Id);

            if (entityDto == null)
            {
                throw new NotFoundException();
            }

            var entity = _mapper.Map<TEntityDto>(entityDto);

            return entity;
        }

        /// <summary>
        /// Hàm gọi tới repo, lấy tất cả bản ghi.
        /// </summary>
        /// <returns>IEnumerable<TEntityDto></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<IEnumerable<TEntityDto>> GetListAsync()
        {
            var entityDto = await _baseRepository.GetListAsync();

            if (entityDto == null)
            {
                throw new NotFoundException();
            }

            var entity = _mapper.Map<IEnumerable<TEntityDto>>(entityDto);

            return entity;
        }

        /// <summary>
        /// Hàm gọi tới repo, chỉnh sửa một bản ghi
        /// </summary>
        /// <param name="Id">Id cần chỉnh sửa</param>
        /// <param name="entityUpdateDto">Thông tin chỉnh sửa</param>
        /// <returns></returns>
        /// Created by: ntvu (19/05/2023)
        public virtual async Task<int> UpdateAsync(Guid Id, TEntityUpdateDto entityUpdateDto)
        {
            var entity = _mapper.Map<TEntity>(entityUpdateDto);

            // validate dữ liệu trước khi truyền vào cho DL
            int res = await _baseRepository.UpdateAsync(Id, entity);

            if (res == 0)
            {
                throw new InternalException();
            }
            return res;
        } 
        #endregion
    }
}
