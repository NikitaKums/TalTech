using System;
using ee.itcollege.nikita.Contracts.BLL.Base.Services;

namespace ee.itcollege.nikita.BLL.Base.Services
{
    public class BaseService : IBaseService
    {
        private readonly Guid _instanceId = Guid.NewGuid();
        public Guid InstanceId => _instanceId;
    }
}   
