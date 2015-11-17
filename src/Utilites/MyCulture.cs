using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Utilites
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class MyCulture : IMyCulture
    {
        public void SetCulture(string culture)
        {
#if DNX451
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
#elif DNXCORE50
            CultureInfo.CurrentCulture = new CultureInfo(culture);
#endif
        }
    }
}
