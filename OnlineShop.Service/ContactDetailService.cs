using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IContactDetailService
    {
        ContactDetail GetDefaultContactDetail();
    }
    public class ContactDetailService : IContactDetailService
    {
        private IUnitOfWork _unitOfWork;
        private IContactDetailRepository _contactDetailRepository;

        public ContactDetailService(IContactDetailRepository contactDetailRepository , IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _contactDetailRepository = contactDetailRepository;
        }

        public ContactDetail GetDefaultContactDetail()
        {
            return _contactDetailRepository.GetSingleByCondition(x => x.Status);
        }
    }
}
