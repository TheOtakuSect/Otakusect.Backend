using AutoMapper;
using OtakuSect.Data;
using OtakuSect.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtakuSect.BussinessLayer.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
        }
    }
}
