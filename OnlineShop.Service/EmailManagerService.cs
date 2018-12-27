using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface IEmailManagerService
    {
        EmailManager Add(EmailManager emailManager);
        IEnumerable<EmailManager> GetAll();
        IEnumerable<EmailManager> GetAll(string keyword, int page, int pageSize, out int totalRow);
        EmailManager Delete(int id);
        EmailManager GetSingleEmail(int id);
        void UpdateEmail(EmailManager email);
        void Save();
    }

    public class EmailManagerService : IEmailManagerService
    {
        IEmailManagerRepository _emailManagerRepository;
        IUnitOfWork _unitOfWork;

        public EmailManagerService(IEmailManagerRepository emailManagerRepository, IUnitOfWork unitOfWork)
        {
            this._emailManagerRepository = emailManagerRepository;
            this._unitOfWork = unitOfWork;
        }

        public EmailManager Add(EmailManager emailManager)
        {
            return _emailManagerRepository.Add(emailManager);
        }

        public IEnumerable<EmailManager> GetAll()
        {
            return _emailManagerRepository.GetAll();
        }

        public IEnumerable<EmailManager> GetAll(string keyword, int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public EmailManager Delete(int id)
        {
            return _emailManagerRepository.Delete(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public EmailManager GetSingleEmail(int id)
        {
            return _emailManagerRepository.GetSingleEntity(id);
        }

        public void UpdateEmail(EmailManager email)
        {
            _emailManagerRepository.Update(email);
        }
    }
}
