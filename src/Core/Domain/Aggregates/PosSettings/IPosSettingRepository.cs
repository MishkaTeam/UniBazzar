using BuildingBlocks.Domain.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates.PosSettings
{
    public interface IPosSettingRepository:IRepositoryBase<PosSetting>
    {
    }
}
