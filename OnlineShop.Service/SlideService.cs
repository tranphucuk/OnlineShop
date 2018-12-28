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
    public interface ISlideService
    {
        Slide Add(Slide slide);
        IEnumerable<Slide> GetAll();
        Slide GetSingle(int id);
        Slide GetSingle(Slide slide);
        Slide Delete(int id);
        void Update(Slide slide);

        void Save();
    }

    public class SlideService : ISlideService
    {
        ISlideRepository _slideRepository;
        IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slideRepository, IUnitOfWork unitOfWork)
        {
            this._slideRepository = slideRepository;
            this._unitOfWork = unitOfWork;
        }

        public Slide Add(Slide slide)
        {
            return _slideRepository.Add(slide);
        }

        public Slide Delete(int id)
        {
            return _slideRepository.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slideRepository.GetAll();
        }

        public Slide GetSingle(int id)
        {
            return _slideRepository.GetSingleEntity(id);
        }

        public Slide GetSingle(Slide slide)
        {
            return _slideRepository.GetSingleEntity(x => x.Name == slide.Name);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide slide)
        {
            _slideRepository.Update(slide);
        }
    }
}
