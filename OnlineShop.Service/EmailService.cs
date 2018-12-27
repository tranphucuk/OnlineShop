using OnlineShop.Common.Exceptions;
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
    public interface IEmailService
    {
        Email Add(Email email);
        void Save();
        IEnumerable<Email> GetAll(string keyword, int page, int pageSize, out int totalRow);
        IEnumerable<Email> GetAll();
        Email GetSingleEmail(int id);
        Email DeleteEmail(int id);
    }

    public class EmailService : IEmailService
    {
        IEmailRepository _emailRepository;
        IUnitOfWork _unitOfWork;

        public EmailService(IEmailRepository emailRepository, IUnitOfWork unitOfWork)
        {
            this._emailRepository = emailRepository;
            this._unitOfWork = unitOfWork;
        }

        public Email Add(Email email)
        {
            var isExisted = _emailRepository.CheckContains(x => x.EmailAddress == email.EmailAddress);
            if (isExisted)
            {
                throw new NameDuplicatedException("This email is subscribed with us already, please change to another email.");
            }
            return _emailRepository.Add(email);
        }

        public Email DeleteEmail(int id)
        {
            return _emailRepository.Delete(id);
        }

        public IEnumerable<Email> GetAll(string keyword, int page, int pageSize, out int totalRow)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                var query = _emailRepository.GetMulti(x => x.Status == true && x.EmailAddress.Contains(keyword));
                query = query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
                totalRow = query.Count();
                return query;
            }
            var queryNoKeyword = _emailRepository.GetMulti(x => x.Status == true);
            totalRow = queryNoKeyword.Count();
            return queryNoKeyword.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Email> GetAll()
        {
            return _emailRepository.GetAll();
        }

        public Email GetSingleEmail(int id)
        {
            return _emailRepository.GetSingleEntity(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
