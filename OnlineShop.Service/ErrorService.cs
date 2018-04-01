using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Models;

namespace OnlineShop.Service
{
    public interface IErrorService
    {
        ErrorLog Create(ErrorLog errorLog);
        void Save();
    }
    public class ErrorService : IErrorService
    {
        private IErrorRepository _errorRepository;
        private IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _errorRepository = errorRepository;
        }

        public ErrorLog Create(ErrorLog errorLog)
        {
            return _errorRepository.Add(errorLog);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
