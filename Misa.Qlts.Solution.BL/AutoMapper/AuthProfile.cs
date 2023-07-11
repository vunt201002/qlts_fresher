using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Misa.Qlts.Solution.BL.AuthService.AuthDtos;
using Misa.Qlts.Solution.DL.Entities;

namespace Misa.Qlts.Solution.BL.AutoMapper
{
    /// <summary>
    /// lớp map người dùng
    /// </summary>
    /// created by: ntvu (10/07/2023)
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthDto>();
        }
    }
}
