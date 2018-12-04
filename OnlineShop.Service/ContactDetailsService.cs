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
    public interface IContactDetailsService
    {
        ContactDetail GetContact();
    }

    public class ContactDetailsService : IContactDetailsService
    {
        IContactDetailRepository _contactDetailRepository;
        IUnitOfWork _unitOfWork;

        public ContactDetailsService(IContactDetailRepository contactDetailRepository, IUnitOfWork unitOfWork)
        {
            this._contactDetailRepository = contactDetailRepository;
            this._unitOfWork = unitOfWork;
        }

        public ContactDetail GetContact()
        {
            return _contactDetailRepository.GetSingleEntity(x => x.Status);
        }
    }
}
