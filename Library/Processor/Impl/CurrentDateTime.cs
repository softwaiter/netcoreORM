﻿using System;

namespace CodeM.Common.Orm.Processor.Impl
{
    public class CurrentDateTime: IExecute
    {
        public object Execute(Model model, string prop, dynamic obj)
        {
            return DateTime.Now;
        }
    }
}
