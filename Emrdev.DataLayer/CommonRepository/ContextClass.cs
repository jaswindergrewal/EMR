using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ClassContainMultipleModel.DAL;

namespace Emrdev.DataLayer
{
    public class ObjectEntity
    {
        public Emrdev.DataLayer.LMC_020505Entities _ObjectEntity = null;
        public Emrdev.DataLayer.LMC_020505Entities _ObjectEntityPart = null;
        public Emrdev.DataLayer.LMC_020505Entities _ObjectEntity2Part = null;
        public LMC_020505Entities ObjectEntity1
        {
            get
            {
                if (_ObjectEntity == null)
                {
                    _ObjectEntity = new LMC_020505Entities();
                }
                return _ObjectEntity;
            }
        }

        public LMC_020505Entities ObjectEntityPart1
        {
            get
            {
                if (_ObjectEntityPart == null)
                {
                    _ObjectEntityPart = new LMC_020505Entities();
                }
                return _ObjectEntityPart;
            }
        }

        public LMC_020505Entities ObjectEntityPart2
        {
            get
            {
                if (_ObjectEntity2Part == null)
                {
                    _ObjectEntity2Part = new LMC_020505Entities();
                }
                return _ObjectEntity2Part;
            }
        }
    }
}
