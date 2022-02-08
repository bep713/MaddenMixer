using System;
using System.Collections.Generic;
using System.Text;

namespace EASoundbankTools.Parser.SBR
{
    public class StoreTypeStrategyFactory
    {
        public static IStoreTypeStrategy Create(byte StoreType)
        {
            switch(StoreType)
            {
                default:
                case 0:
                    return new StoreType0Strategy();
                case 1:
                    return new StoreType1Strategy();
                case 2:
                    return new StoreType2Strategy();
                case 3:
                    return new StoreType3Strategy();
                case 4:
                    return new StoreType4Strategy();
            }
        }
    }
}
