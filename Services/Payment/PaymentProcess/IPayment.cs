using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcess
{
    public interface IPayment
    {
        bool Processor();
    }
}
