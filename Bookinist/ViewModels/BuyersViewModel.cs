using Bookinist.DAL.Entities;
using Bookinist.Interfaces;

using MathCore.WPF.ViewModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace Bookinist.ViewModels
{
    class BuyersViewModel : ViewModel
    {
        private IRepository<Buyer> _bauerRepository;

        public BuyersViewModel(IRepository<Buyer> bauerRepository)
        {
            _bauerRepository = bauerRepository;
        }
    }
}
