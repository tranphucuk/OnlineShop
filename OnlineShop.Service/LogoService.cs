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
    public interface ILogoService
    {
        IEnumerable<Logo> GetAllLogos();
        IEnumerable<Logo> GetMultiLogos();
        void UpdateLogo(Logo logo);
        Logo GetSingleLogo(int id);
        void Save();
    }

    public class LogoService : ILogoService
    {
        ILogoRepository _logoRepository;
        IUnitOfWork _unitOfWork;

        public LogoService(ILogoRepository logoRepository, IUnitOfWork unitOfWork)
        {
            this._logoRepository = logoRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Logo> GetAllLogos()
        {
            return _logoRepository.GetAll();
        }

        public IEnumerable<Logo> GetMultiLogos()
        {
            return _logoRepository.GetMulti(x => x.Status == true);
        }

        public Logo GetSingleLogo(int id)
        {
            return _logoRepository.GetSingleEntity(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void UpdateLogo(Logo logo)
        {
            _logoRepository.Update(logo);
        }
    }
}
