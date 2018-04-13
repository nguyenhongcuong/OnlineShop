using System.Collections.Generic;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface ICommonService
    {
        IEnumerable<Slide> GetSlides();
    }
    public class CommonService : ICommonService
    {
        private ISlideRepository _slideRepository;
        private IUnitOfWork _unitOfWork;

        public CommonService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            _slideRepository = slideRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x => x.Status);
        }
    }
}
