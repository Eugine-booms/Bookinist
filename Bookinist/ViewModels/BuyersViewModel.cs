using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

using System;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        private IRepository<Buyer> _bauerRepository;

        public BuyersViewModel()
        {
           
                if (!App.IsDesignTime)
                    throw new InvalidOperationException("Использование конструктора предназначенного для дизайнера VS");
            
        }

        public BuyersViewModel(IRepository<Buyer> bauerRepository)
        {
            _bauerRepository = bauerRepository;
        }
    }
}
